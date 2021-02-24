using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;

namespace IdeaSpend.API
{
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        #region Private Members

        private readonly CatalogService _catalogService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public CatalogController(CatalogService catalogService, IMapper mapper)
        {
            _catalogService = catalogService;
            _mapper = mapper;
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

        [HttpGet("get/{userId}")]
        public IActionResult GetCatalos(int userId)
        {
            var userCatalogs = _catalogService.Catalogs(userId);
            var userCatalogsName = _mapper.Map<IEnumerable<CatalogDto>> ( userCatalogs );

            return Ok( userCatalogsName );
        }
        
    }
}
