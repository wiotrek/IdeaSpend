using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdeaSpend.API
{
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        #region Private Members

        private readonly CatalogService _catalogService;

        #endregion

        #region Constructor

        public CatalogController(CatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        #endregion

        [HttpPost("add/{userId}")]
        public async Task<IActionResult> AddCatalog([FromBody] CatalogDto catalogDto, int userId)
        {
            // If didn't save return message
            if (!await _catalogService.AddCatalog(catalogDto, userId))
                return BadRequest("Nie udało się dodać katalogu");

            return StatusCode(201);
        }
    }
}
