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
            var result = _mapper.Map<List<ProductGetHomePageViewModel>>(await _appEFContext.Products.Include(p => p.Images).ToListAsync());
            return Ok(result);
        }
        [HttpGet("homePageList")]
        public async Task<IActionResult> HomePageList()
        {

            var result = _mapper.Map<List<ProductGetHomePageViewModel>>(await _appEFContext.Products.Include(p => p.Images)
                .Where(p => p.HomePageSelection)
                .OrderBy(p => p.HomePagePriority).ToListAsync());

            return Ok(result);
        }
        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {

            var result = _mapper.Map<List<ProductGetHomePageViewModel>>(await _appEFContext.Products.Include(p => p.Images).ToListAsync()).FirstOrDefault(p=>p.Id == id);

            return Ok(result);
        }
        [HttpPost("getRelatedProducts")]
        public async Task<IActionResult> GetRelatedProducts(RelatedProductViewModel settings)
        {

            var result = _mapper.Map<List<ProductGetHomePageViewModel>>
                (await _appEFContext.Products.Include(p => p.Images).Where(p=>p.CategoryId==settings.CategoryId && p.Id != settings.Id).Take(4).ToListAsync());

            return Ok(result);
        }


        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] ProductViewModel model)
        {
            if (model.Name != null)
            {
                var product = _mapper.Map<ProductEntity>(model);

                _appEFContext.Add(product);
                _appEFContext.SaveChanges();

                foreach (var idImg in model.ImagesID)
                {
                    var image = await _appEFContext.ProductImages.SingleOrDefaultAsync(x => x.Id == idImg);
                    image.ProductId = product.Id;
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
