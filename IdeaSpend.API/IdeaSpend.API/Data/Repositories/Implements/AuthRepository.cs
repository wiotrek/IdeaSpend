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
        
        public async Task<UserEntity> FindUserByUsername( string username )
        {
            return await _context.Users.FirstOrDefaultAsync ( x => x.Username == username );
        }

        /// <summary>
        /// Saving new user to db
        /// </summary>
        public async Task<bool> Register( UserEntity user )
        {
            await _context.Users.AddAsync ( user );
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion
    }
}