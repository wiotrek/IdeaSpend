using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace IdeaSpend.API
{
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        #region Private Members

        private readonly ICatalogRepository _catalogRepository;

        #endregion

        #region Constructor

        public CatalogController(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        #endregion

        [HttpPost("add/{userId}")]
        public async Task<IActionResult> AddCatalog([FromBody] CatalogDto catalogDto, int userId)
        {
            // TODO: Validate properties of the catalogDto

            // If didn't save return message
            if (!await _catalogRepository.AddCatalogAsync(catalogDto, userId))
                return BadRequest("Nie udało się dodać katalogu");

            return StatusCode(201);
        }
    }
}
