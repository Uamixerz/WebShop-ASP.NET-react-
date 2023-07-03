using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using WebShop.Data;
using WebShop.Data.Entities.Product;
using WebShop.Helpers;
using WebShop.Models;
using WebShop.Models.Items;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly AppEFContext _appEFContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public ProductController(AppEFContext appEFContext, IWebHostEnvironment hostingEnvironment, IConfiguration configuration, IMapper mapper)
        {
            _appEFContext = appEFContext;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {



            var res = await _appEFContext.Products.ToListAsync();
            //var result = await _appEFContext.Products.Select(x => _mapper.Map<ProductGetViewModel>(x)).ToListAsync();
            var result = new List<ProductGetViewModel>();
            foreach(var product in res)
            {
                result.Add(new ProductGetViewModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    categoryId = (int)product.CategoryId,
                    CategoryName = _appEFContext.Categories.Where(x => x.Id == product.CategoryId).First().Name,
                    Images = _appEFContext.ProductImages.ToList().Where(x => x.ProductId == product.Id).Select(x => new ProductImageItemViewModel { Id = x.Id, Name = x.UrlImage }).ToList(),
                });
            }


            return Ok(result);
        }



        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] ProductViewModel model)
        {
            if (model.Name != null)
            {
                var product = new ProductEntity { Name = model.Name, 
                    Description = model.Description, 
                    Price = model.Price, 
                    DateCreated = DateTime.UtcNow, 
                    CategoryId = model.categoryId };
                _appEFContext.Add(product);
                _appEFContext.SaveChanges();
                foreach (var idImg in model.ImagesID)
                {
                    var image = await _appEFContext.ProductImages.SingleOrDefaultAsync(x => x.Id == idImg);
                    image.ProductId = product.Id;
                    if (product.Images == null)
                        product.Images = new List<ProductImagesEntity>();
                    product.Images.Add(image);
                }
                
                _appEFContext.SaveChanges();
                return Ok(product);
            }
            return BadRequest(404);
        }

            [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] ProductImageUploadViewModel model)
        {
            String fileName = string.Empty;
            if (model.Image != null)
            {
                var imageOld = model.Image;

                var uploadsFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "Data", "Uploads");
                fileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var ms = new MemoryStream())
                {
                    await model.Image.CopyToAsync(ms);
                    var bmp = new Bitmap(Image.FromStream(ms));
                    string[] sizes = ((string)_configuration.GetValue<string>("ImageSizes")).Split(" ");
                    foreach (var size in sizes)
                    {
                        var s = Convert.ToInt32(size);
                        var saveImage = ImageWorker.CompressImage(bmp, s, s, false, false);
                        saveImage.Save(Path.Combine(uploadsFolder, size + "_" + fileName));
                        //видаляю старі фото
                        var imgDelete = Path.Combine(uploadsFolder, s + "_" + imageOld);
                        if (System.IO.File.Exists(imgDelete))
                        {
                            System.IO.File.Delete(imgDelete);
                        }
                    }
                }
                var entity = new ProductImagesEntity();
                entity.UrlImage = fileName;
                entity.DateCreated = DateTime.UtcNow;
                _appEFContext.ProductImages.Add(entity);
                _appEFContext.SaveChanges();
                return Ok(_mapper.Map<ProductImageItemViewModel>(entity));
            }

            return BadRequest();
        }
        [HttpDelete("RemoveImage/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var image = await _appEFContext.ProductImages.SingleOrDefaultAsync(x => x.Id == id);
            if (image == null)
                return NotFound();

            var dirSave = Path.Combine(_hostingEnvironment.ContentRootPath, "Data", "Uploads");
            string[] sizes = ((string)_configuration.GetValue<string>("ImageSizes")).Split(" ");
            foreach (var s in sizes)
            {
                var imgDelete = Path.Combine(dirSave, s + "_" + image.UrlImage);
                if (System.IO.File.Exists(imgDelete))
                {
                    System.IO.File.Delete(imgDelete);
                }
            }
            _appEFContext.ProductImages.Remove(image);
            await _appEFContext.SaveChangesAsync();
            return Ok();
        }
    }
}
