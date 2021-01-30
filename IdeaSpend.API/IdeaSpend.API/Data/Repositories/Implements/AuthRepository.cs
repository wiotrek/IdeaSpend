using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IdeaSpend.API
{
    public class AuthRepository : IAuthRepository
    {
        #region Private Members
        
        private readonly IdeaSpendContext _context;
        
        #endregion
        
        #region Constructors

        public AuthRepository(IdeaSpendContext context)
        {
            _context = context;
        }
        
        #endregion
        
        #region Implemented Methods
        
        /// <summary>
        /// Authenticate user to allow overview self profile
        /// </summary>
        /// <param name="username">The username to login</param>
        /// <param name="password">The password to matching with encrypt password</param>
        /// <returns></returns>
        public async Task<UserEntity> Login( string username, string password )
        {
            // Get user by username
            var user = await _context.Users.FirstOrDefaultAsync ( x => x.Username == username );

            // Make sure user exist
            if( user == null )
                return null;

            return !VerifyPasswordHash ( password, user.PasswordHash, user.PasswordSalt ) ? null : user;
        }

        /// <summary>
        /// Create new user to web application
        /// </summary>
        /// <param name="user">User information to create</param>
        /// <param name="password">The password to login</param>
        /// <returns></returns>
        public async Task<bool> Register( UserEntity user, string password )
        {
            CreatePasswordHashSalt ( password, out var passwordHash, out var passwordSalt );

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync ( user );
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsUserExist( string username )
        {
            return await _context.Users.AnyAsync ( x => x.Username == username );
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