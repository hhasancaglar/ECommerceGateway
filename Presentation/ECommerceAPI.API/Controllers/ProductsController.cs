using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            await _productWriteRepository.AddRangeAsync(new()
            {
                new() { Id = Guid.NewGuid(), Name = "Product 1", Price= 110, InsertDate = DateTime.Now, Stock = 10, IsActive = true, IsDeleted = false },
                new() { Id = Guid.NewGuid(), Name = "Product 2", Price= 120, InsertDate = DateTime.Now, Stock = 20, IsActive = true, IsDeleted = false },
                new() { Id = Guid.NewGuid(), Name = "Product 3", Price= 130, InsertDate = DateTime.Now, Stock = 30, IsActive = true, IsDeleted = false },
                new() { Id = Guid.NewGuid(), Name = "Product 4", Price= 140, InsertDate = DateTime.Now, Stock = 40, IsActive = true, IsDeleted = false },
            });

            var count = await _productWriteRepository.SaveAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);

            return Ok(product);
        }
    }
}
