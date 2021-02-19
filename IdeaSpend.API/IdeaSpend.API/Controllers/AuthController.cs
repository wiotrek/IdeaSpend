using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdeaSpend.API
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Private Members
        
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        
        #endregion

        #region Constructor
        
        public AuthController( IAuthRepository authRepository, IConfiguration config )
        {
            _authRepository = authRepository;
            _config = config;
        }
        
        #endregion

        #region Request Methods
        
        [HttpPost("register")]
        public async Task<IActionResult> Register( [FromBody] RegisterDto registerDto )
        {
            // Create user to save
            var user = new UserEntity();

            // Make sure username is free
            if (await _authRepository.IsUserExist ( registerDto.Username ))
                return BadRequest($"Nazwa użytkownika {registerDto.Username} jest zajęta.");
            
            // Check repeat password with password user want
            if( registerDto.Password != registerDto.RepeatPassword )
                return BadRequest ( "Wpisane hasła do siebie nie pasują" );
            
            // Fill user information from view form
            user.Username = registerDto.Username;
            user.FirstName = registerDto.FirstName;
            user.LastName = registerDto.LastName;
            user.Email = registerDto.Email;
            user.Created = DateTime.Now;

            // Save user to database
            var createdUser = await _authRepository.Register ( user, registerDto.Password );

            // Get know user something was wrong
            if( createdUser == false )
                return BadRequest ( $"Utworzenie użytkownika {registerDto.Username} nie powiodło się" );

            return StatusCode ( 201 );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login( [FromBody] LoginDto loginDto )
        {
            // Get user from database via auth
            var user = await _authRepository.Login ( loginDto.Username, loginDto.Password );
            
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

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
        
        #endregion
    }
}