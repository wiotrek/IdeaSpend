using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IdeaSpend.API
{
    public class AuthService
    {
        #region Private Members

        private readonly IAuthRepository _authRepository;

        #endregion

        
        #region Constructor

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        #endregion

        
        #region Public Methods

        /// <summary>
        /// Authenticate user to allow overview self profile
        /// </summary>
        /// <param name="username">Username as login user</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserEntity> LoginAsync( string username, string password )
        {
            var user = await _authRepository.FindUserByUsername ( username );

            if( user == null )
                return null;
            
            return !VerifyPasswordHash ( password, user.PasswordHash, user.PasswordSalt ) ? null : user;
        }
        
        public async Task<bool> AddUser( UserEntity user, string password )
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            if( !hasNumber.IsMatch ( password ) || !hasUpperChar.IsMatch ( password ) ||
                !hasMinimum8Chars.IsMatch ( password ) )
                return false;

            CreatePasswordHashSalt ( password, out var passwordHash, out var passwordSalt );

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return await _authRepository.Register( user );
        }

        #endregion

        
        #region Private Methods
        
        /// <summary>
        /// Encrypt password inputed by user
        /// </summary>
        /// <param name="password">The password to encrypt</param>
        /// <param name="passwordHash">Create hash of the <see cref="password"/></param>
        /// <param name="passwordSalt">Create salt of the <see cref="password"/></param>
        private static void CreatePasswordHashSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        /// <summary>
        /// Check inputed password with encrypt password
        /// </summary>
        /// <param name="password">The password to verify</param>
        /// <param name="passwordHash">The part of <see cref="password"/> hash</param>
        /// <param name="passwordSalt">The part of <see cref="password"/> salt</param>
        /// <returns>True if inputed password is matching with encrypt salt-hash parts, false otherwise</returns>
        private static bool VerifyPasswordHash(string password, IReadOnlyList<byte> passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            
            return !computeHash.Where ( ( hash, i ) => hash != passwordHash[i] ).Any();
        }
        
        #endregion
    }
}