using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdeaSpend.API
{
    // TODO: Create Response class to keep success flag and message depend on status
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Private Members
        
        private readonly AuthService _authService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        
        #endregion

        #region Constructor
        
        public AuthController( AuthService authService,
                               IConfiguration config,
                               IMapper mapper)
        {
            _authService = authService;
            _config = config;
            _mapper = mapper;
        }
        
        #endregion

        #region Request Methods
        
        [HttpPost("register")]
        public async Task<IActionResult> Register( [FromBody] RegisterDto registerDto )
        {
            // Create user to save
            var user = new UserEntity();

            
            // Check repeat password with password user want
            if( registerDto.Password != registerDto.RepeatPassword )
                return BadRequest ( "Wpisane hasła do siebie nie pasują" );
            
            
            // TODO: Transfer data via automapper class
            // Fill user information from view form
            user.Username = registerDto.Username;
            user.FirstName = registerDto.FirstName;
            user.LastName = registerDto.LastName;
            user.Email = registerDto.Email;
            user.Created = DateTime.Now;

            
            // Save user to database
            var isCreated = await _authService.AddUser ( user, registerDto.Password );

            
            // Get know user something was wrong
            if( isCreated == false )
                return BadRequest ( $"Utworzenie użytkownika {registerDto.Username} nie powiodło się" );

            
            return StatusCode ( 201 );
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync( [FromBody] LoginDto loginDto )
        {
            // Get user from database via auth
            var user = await _authService.LoginAsync( loginDto.Username, loginDto.Password );
            
            
            if( user == null )
                return Unauthorized();
            
            
            #region Create Token
            
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(10),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            #endregion

            var userToReturn = _mapper.Map<LoginUserDto> ( user );
            userToReturn.Token = tokenHandler.WriteToken ( token );
            
            return Ok(userToReturn);
        }
        
        #endregion
    }
}