using System.Threading.Tasks;

namespace IdeaSpend.API
{
    /// <summary>
    /// Authorization methods to allow overview web content
    /// </summary>
    public interface IAuthRepository : IBaseRepository
    {
        Task<UserEntity> FindUserByUsername( string username );
        Task<bool> Register( UserEntity user );
    }
}