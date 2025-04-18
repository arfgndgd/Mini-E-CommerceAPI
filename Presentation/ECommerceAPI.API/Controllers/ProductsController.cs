using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Application;
using ECommerceAPI.Application.ViewModels.Products;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Storage;
using Microsoft.EntityFrameworkCore;
using MediatR;
using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly IFileWriteRepository _fileWriteRepository;
        readonly IFileReadRepository _fileReadRepository;
        readonly IProductImageFileReadRepository _productImageFileReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        readonly IStorageService _storageService;
        readonly IConfiguration _configuration;



        readonly IMediator _mediator;



        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IStorageService storageService, IConfiguration configuration, IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _storageService = storageService;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            #region training
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


            //Order order = await _orderReadRepository.GetByIdAsync("f18caa89-3671-4a74-95e8-a764e2ce72a3");
            //order.Address = "İstanbul";
            //await _orderWriteRepository.SaveAsync();
            #endregion

            #region old code

            // paramater: [FromQuery]Pagination pagination
            //var totalCount = _productReadRepository.GetAll(false).Count();
            //var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            //{
            //    p.Id,
            //    p.Name,
            //    p.Stock,
            //    p.Price,
            //    p.CreatedDate,
            //    p.UpdatedDate
            //});

            //return Ok(new
            //{
            //    totalCount,
            //    products
            //});
            #endregion

            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            #region old code
            //paramater: ProductCreateVM model
            //await _productWriteRepository.AddAsync(new()
            //{
            //    Name = model.Name,
            //    Price = model.Price,
            //    Stock = model.Stock
            //});
            //await _productWriteRepository.SaveAsync();
            #endregion

            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ProductUpdateVM model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Name = model.Name;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")] //....../api/controller/action
        public async Task<IActionResult> Upload(string id)
        {

            ////var datas = await _storageService.UploadAsync("resource/files", Request.Form.Files); //local
            //var datas = await _storageService.UploadAsync("files", Request.Form.Files); //azure
            ////var datas = await _fileService.UploadAsync("resource/files", Request.Form.Files); //cloud'ta resource kullanılmaz
            //await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.pathOrContainerName,
            //    Storage = _storageService.StorageName
            //}).ToList());
            //await _productImageFileWriteRepository.SaveAsync();

            ////await _invoiceFileWriteRepository.AddRangeAsync(datas.Select(d => new InvoiceFile()
            ////{
            ////    FileName = d.fileName,
            ////    Path = d.path,
            ////    Price = new Random().Next()
            ////}).ToList());
            ////await _invoiceFileWriteRepository.SaveAsync();

            ////await _fileWriteRepository.AddRangeAsync(datas.Select(d => new Domain.Entities.File()
            ////{
            ////    FileName = d.fileName,
            ////    Path = d.path
            ////}).ToList());
            ////await _fileWriteRepository.SaveAsync();

            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", Request.Form.Files);

            Product product = await _productReadRepository.GetByIdAsync(id);
            ////bu da kullanılabilir
            //foreach (var r in result)
            //{
            //    product.ProductImageFiles.Add(new()
            //    {
            //        FileName = r.fileName,
            //        Path = r.pathOrContainerName,
            //        Storage = _storageService.StorageName,
            //        Products = new List<Product>() { product }
            //    });
            //}

            await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages(string id)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            return Ok(product.ProductImageFiles.Select(p => new
            {
                Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                p.FileName,
                p.Id
            }));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id, string imageId)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));
            product.ProductImageFiles.Remove(productImageFile);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
 