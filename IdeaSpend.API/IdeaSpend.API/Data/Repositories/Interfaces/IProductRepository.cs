using System.Threading.Tasks;

namespace IdeaSpend.API
{
    public interface IProductRepository
    {
        Task<bool> AddProductAsync(ProductDto productDto, int userId);
    }
}
