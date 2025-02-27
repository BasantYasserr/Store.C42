using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.C42.APIs.Errors;
using Store.C42.Repository.Data.Contexts;

namespace Store.C42.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }



        [HttpGet("notfound")]   //GET:   /api/buggy/notfound
        public async Task<IActionResult> GetNotFound()
        {
            var brand = await _context.Brands.FindAsync(100);
            if(brand is null) return NotFound(new ApiErrorResponse(404));
            return Ok(brand);
        }
        
        [HttpGet("servererror")]   //GET:   /api/buggy/servererror
        public async Task<IActionResult> GetServerError()
        {
            var brand = await _context.Brands.FindAsync(100);
            var brandToString = brand.ToString();   //Will Throw Exception (NullReferenceException)
            return Ok(brand);
        }

        [HttpGet("badrequest")]   //GET:   /api/buggy/badrequest
        public async Task<IActionResult> GetBadRequest()
        {
            return BadRequest(new ApiErrorResponse(400));
        }
        
        [HttpGet("badrequest/{id}")]   //GET:   /api/buggy/badrequest/ahmed
        public async Task<IActionResult> GetBadRequest(int id)
        {
            return BadRequest();
        }
        
        [HttpGet("unauthorized")]   //GET:   /api/buggy/unauthorized
        public async Task<IActionResult> GetUnauthorizedError(int id)
        {
            return Unauthorized(new ApiErrorResponse(40));
        }
    } 
}