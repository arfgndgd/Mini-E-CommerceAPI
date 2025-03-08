using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        readonly private IOrderWriteRepository _orderWriteRepository;
        readonly private IOrderReadRepository _orderReadRepository;
        readonly private ICustomerWriteRepository _customerWriteRepository;
        readonly private ICustomerReadRepository _customerReadRepository;


        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IOrderWriteRepository orderWriteRepository, ICustomerWriteRepository customerWriteRepository, ICustomerReadRepository customerReadRepository, IOrderReadRepository orderReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
            _customerReadRepository = customerReadRepository;
            _orderReadRepository = orderReadRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            //await _productWriteRepository.AddRangeAsync(new()
            //{
            //    new(){ Id = Guid.NewGuid(), Name = "Product 1", Price = 100, CreatedDate = DateTime.UtcNow, Stock = 10},
            //    new(){ Id = Guid.NewGuid(), Name = "Product 2", Price = 200, CreatedDate = DateTime.UtcNow, Stock = 20},
            //    new(){ Id = Guid.NewGuid(), Name = "Product 3", Price = 300, CreatedDate = DateTime.UtcNow, Stock = 40}
            //});
            //await _productWriteRepository.SaveAsync();

            //Product p = await _productReadRepository.GetByIdAsync("cb9339b5-bf43-4772-a88f-56b70813628d", false); //tracking mekanizmaası kapatıldı
            //p.Name = "Mehmet";
            //await _productWriteRepository.SaveAsync();

            //await _productWriteRepository.AddAsync(new()
            //{
            //    Name = "Product 2", Price = 200, CreatedDate = DateTime.UtcNow, Stock = 20
            //});



            //var customerId = Guid.NewGuid();
            //await _customerWriteRepository.AddAsync(new() { Id = customerId, Name ="AHmet"});
            //await _orderWriteRepository.AddAsync(new()
            //{
            //    Description = "vvvvvvvvvvvv",
            //    Address = "vvvvvvvvvvvv",
            //    CustomerId = customerId
            //});
            //await _orderWriteRepository.AddAsync(new()
            //{
            //    Description = "aaaaaaa",
            //    Address = "aaaaaaaaaaaaa",
            //    CustomerId = customerId
            //});

            //await _orderWriteRepository.SaveAsync();


            Order order = await _orderReadRepository.GetByIdAsync("f18caa89-3671-4a74-95e8-a764e2ce72a3");
            order.Address = "İstanbul";
            await _orderWriteRepository.SaveAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
