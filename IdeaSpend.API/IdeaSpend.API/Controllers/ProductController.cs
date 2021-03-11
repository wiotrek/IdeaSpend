using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;

namespace IdeaSpend.API
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        #region Private Members

        private readonly ProductService _productService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public ProductController(ProductService productService,
                                 IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        #endregion

        /// <summary>
        /// NOTE: {userId} word inside HttpPost parameter must be the same as userId word inside AddProduct argument
        /// </summary>
        [HttpPost("add/{userId}")]
        public async Task<IActionResult> AddProduct([FromBody] IEnumerable<ProductDto> allProductsDto, int userId)
        {
            if (!await _productService.GetListThenCreateProductAsync(allProductsDto, userId))
                return BadRequest("Nie udało się zapisać produktu");

            return StatusCode(201);
        }

        [HttpGet("get/{userId}")]
        public IActionResult GetProducts(int userId)
        {
            var userProducts = _productService.ReadProducts(userId);
            var productsToReturn = _mapper.Map<IEnumerable<ProductDto>>(userProducts);

            return Ok(productsToReturn);
        }

        [HttpGet("get/{userId}/property:{property}")]
        public IActionResult GetProductsByProperty( int userId, string property )
        {
            var userFilterProducts = _productService.ReadProductsByNameOrSeller ( userId, property );
            var filterProductsToReturn = _mapper.Map<IEnumerable<ProductDto>> ( userFilterProducts );

            return Ok ( filterProductsToReturn );
        }
        
        [HttpDelete("del/{userId}/product:{productId}")]
        public IActionResult DeleteCatalog(int userId, int productId)
        {
            if (!_productService.DeleteProduct(userId, productId))
                return BadRequest("product hasn't been deleted");

            return StatusCode(201);
        }
    }
}
