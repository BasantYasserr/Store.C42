using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.C42.Core.Dtos.Products;
using Store.C42.Core.Helper;
using Store.C42.Core.Services.Contract;
using Store.C42.Core.Specifications.Products;

namespace Store.C42.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]   // GET  BaseUrl/api/Products
        //sort: name, pricAsc, priceDesc
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecParams productSpecParams)  // endpoint
        {
            var result = await _productService.GetAllProductsAsync(productSpecParams);  

            return Ok(result);     // 200
        }


        [HttpGet("brands")]   // GET  BaseUrl/api/Products/brands
        public async Task<IActionResult> GetAllBrands() 
        {
            var result = await _productService.GetAllTypesAsync();
            return Ok(result);
        }


        [HttpGet("types")]   // GET  BaseUrl/api/Products/types
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();
            return Ok(result);
        }


        [HttpGet("{id}")]        // GET  BaseUrl/api/Products
        public async Task<IActionResult> GetProductbyId(int? id)
        {
            if (id is null) return BadRequest("Invalid Id !!");
            var result = await _productService.GetProductByIdAsync(id.Value);
            if(result is null) return NotFound($"The Product With Id: {id} Is Not Found At DB");
            return Ok(result);
        }
    }
}
