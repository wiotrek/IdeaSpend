using System.Threading.Tasks;

namespace IdeaSpend.API
{
    /// <summary>
    /// Authorization methods to allow overview web content
    /// </summary>
    public interface IAuthRepository
    {
        Task<UserEntity> Login( string username, string password );
        Task<bool> Register( UserEntity user, string password );
        
        /// <summary>
        /// Check if user exist
        /// </summary>
        /// <param name="username">The unique username</param>
        /// <returns></returns>
        Task<bool> IsUserExist( string username );
    }
}