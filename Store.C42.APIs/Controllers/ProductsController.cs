using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.C42.APIs.Errors;
using Store.C42.Core.Dtos.Products;
using Store.C42.Core.Helper;
using Store.C42.Core.Services.Contract;
using Store.C42.Core.Specifications.Products;

namespace Store.C42.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }





        [ProducesResponseType(typeof(PaginationResponse<ProductDto>), StatusCodes.Status200OK)]
        [HttpGet]   // GET  BaseUrl/api/Products
        //sort: name, pricAsc, priceDesc
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery] ProductSpecParams productSpecParams)  // endpoint
        {
            var result = await _productService.GetAllProductsAsync(productSpecParams);  

            return Ok(result);     // 200
        }



        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("brands")]   // GET  BaseUrl/api/Products/brands
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands() 
        {
            var result = await _productService.GetAllTypesAsync();
            return Ok(result);
        }



        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("types")]   // GET  BaseUrl/api/Products/types
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();
            return Ok(result);
        }



        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]        // GET  BaseUrl/api/Products
        public async Task<IActionResult> GetProductbyId(int? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(400));
            var result = await _productService.GetProductByIdAsync(id.Value);
            if(result is null) return NotFound(new ApiErrorResponse(404));
            return Ok(result);
        }
    }
}
