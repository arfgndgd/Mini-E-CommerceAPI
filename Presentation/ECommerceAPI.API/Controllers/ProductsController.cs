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
using ECommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using ECommerceAPI.Application.Features.Commands.Product.RemoveProduct;
using ECommerceAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using ECommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
using ECommerceAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using ECommerceAPI.Application.Features.Commands.ProductImageFile.RemovProductImage;
using Microsoft.AspNetCore.Authorization;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
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
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
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
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            #region old code
            // paramater ProductUpdateVM
            //Product product = await _productReadRepository.GetByIdAsync(model.Id);
            //product.Stock = model.Stock;
            //product.Price = model.Price;
            //product.Name = model.Name;
            //await _productWriteRepository.SaveAsync();
            #endregion

            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            #region old code
            //await _productWriteRepository.RemoveAsync(id);
            //await _productWriteRepository.SaveAsync();
            #endregion

            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
            return Ok();
        }

        [HttpPost("[action]")] //....../api/controller/action
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            #region old code
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

            //List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", Request.Form.Files);

            //Product product = await _productReadRepository.GetByIdAsync(id);
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

            //await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new ProductImageFile
            //{
            //    FileName = r.fileName,
            //    Path = r.pathOrContainerName,
            //    Storage = _storageService.StorageName,
            //    Products = new List<Product>() { product }
            //}).ToList());

            //await _productImageFileWriteRepository.SaveAsync();
            //return Ok();

            #endregion

            uploadProductImageCommandRequest.Files = Request.Form.Files;
            UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            #region old code
            //Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
            //    .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            //return Ok(product.ProductImageFiles.Select(p => new
            //{
            //    Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
            //    p.FileName,
            //    p.Id
            //}));
            #endregion

            List<GetProductImagesQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest);
            return Ok(response);
        }

        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] RemoveProductImageCommandRequest removeProductImageCommandRequest, string imageId)
        {
            #region old code
            //Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
            //    .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            //ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));
            //product.ProductImageFiles.Remove(productImageFile);
            //await _productWriteRepository.SaveAsync();
            //return Ok();
            #endregion

            removeProductImageCommandRequest.ImageId = imageId;
            RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);
            return Ok();
        }
    }
}
 