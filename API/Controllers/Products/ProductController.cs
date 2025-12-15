using Application.Interfaces.Shop;
using Domain.Dtos.Shop;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: /api/Product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllExternalProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetExternalProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
        // POST: /api/Product
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FakeStoreProductDto dto)
        {
            if (dto == null) return BadRequest("Product data is required.");

            var result = await _productService.CreateExternalProductAsync(dto);
            if (!result.IsSucceed) return BadRequest(result.Message);

            return Ok(result.Message);
        }

        // PUT: /api/Product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FakeStoreProductDto dto)
        {
            if (dto == null) return BadRequest("Product data is required.");

            var result = await _productService.UpdateExternalProductAsync(id, dto);
            if (!result.IsSucceed) return BadRequest(result.Message);

            return Ok(result.Message);
        }

        // DELETE: /api/Product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteExternalProductAsync(id);
            if (!result.IsSucceed) return BadRequest(result.Message);

            return Ok(result.Message);
        }


    }
}