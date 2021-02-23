using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IdeaSpend.API
{
    public class AuthRepository : BaseRepository, IAuthRepository
    {
        #region Constructors

        public AuthRepository(IdeaSpendContext context) : base(context) { }
        
        #endregion
        
        #region Implemented Methods
        
        public async Task<UserEntity> FindUserByUsername( string username )
        {
            return await _dataContext.Users.FirstOrDefaultAsync ( x => x.Username == username );
        }

        /// <summary>
        /// Saving new user to db
        /// </summary>
        public async Task<bool> Register( UserEntity user )
        {
            await _dataContext.Users.AddAsync ( user );
            return await _dataContext.SaveChangesAsync() > 0;
        }

        #endregion
    }
}