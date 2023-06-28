using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using WebShop.Data;
using WebShop.Data.Entities;
using WebShop.Helpers;
using WebShop.Models;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        private readonly AppEFContext _appEFContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public CategoriesController(AppEFContext appEFContext, IWebHostEnvironment hostingEnvironment, IConfiguration configuration, IMapper mapper)
        {
            _appEFContext = appEFContext;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _mapper = mapper;

        }
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var result = await _appEFContext.Categories.Select(x => _mapper.Map<CategoryGetViewModel>(x)).ToListAsync();
            
            return Ok(result);
        }
        [HttpGet("list{id}")]
        public async Task<IActionResult> List(int id)
        {



            //var result = await _appEFContext.Categories.ToListAsync();
            var result = new List<CategoryGetViewModel>();
            foreach (var category in _appEFContext.Categories.Where(i => i.ParentId == id).ToList())
            {
                result.Add(new CategoryGetViewModel
                {
                    Name = category.Name,
                    Description = category.Description,
                    Image = category.Image,
                    ParentId = category.ParentId,
                    Id = category.Id
                });
            }

            return Ok(result);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _appEFContext.Categories
                .Where(x => x.Id == id)
                .Select(x => _mapper.Map<CategoryGetViewModel>(x))
                .ToListAsync();
            if (result.Count > 0)
            {
                return Ok(result[0]);
            }
            else
            { return NotFound(); }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CategoryCreateViewModel model)
        {
            if (model.Image != null && model.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "Data", "Uploads");

                var fileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);

               
                using (var ms = new MemoryStream())
                {
                   await model.Image.CopyToAsync(ms);
                    var bmp = new Bitmap(Image.FromStream(ms));
                    string []sizes = ((string)_configuration.GetValue<string>("ImageSizes")).Split(" ");
                    foreach (var size in sizes)
                    {
                        var s = Convert.ToInt32(size);
                        var saveImage = ImageWorker.CompressImage(bmp, s, s, false, false);
                        saveImage.Save(Path.Combine(uploadsFolder, size + "_" + fileName));
                    }
                }

                CategoryEntity category = _mapper.Map<CategoryEntity>(model);
               
                category.Image = fileName;
                await _appEFContext.AddAsync(category);
                await _appEFContext.SaveChangesAsync();
                return Ok(category);
            }
            else
                return BadRequest("No file uploaded");
        }
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromForm] CategoryEditViewModel model)
        {
            var category = await _appEFContext.Categories.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (category == null)
                return NotFound();
            
            String fileName = string.Empty;
            if (model.ImageUpload != null)
            {
                var imageOld = category.Image;

                var uploadsFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "Data", "Uploads");
                fileName = Guid.NewGuid().ToString() + "_" + model.ImageUpload.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var ms = new MemoryStream())
                {
                    await model.ImageUpload.CopyToAsync(ms);
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
            }
            category.Name = model.Name;
            category.Priority = model.Priority;
            category.Description = model.Description;
            category.ParentId = model.ParentId == 0 ? null : model.ParentId;
            category.Image = string.IsNullOrEmpty(fileName) ? category.Image : fileName;
            await _appEFContext.SaveChangesAsync();
            return Ok(category);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _appEFContext.Categories.SingleOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound();

            var dirSave = Path.Combine(_hostingEnvironment.ContentRootPath, "Data", "Uploads");
            string[] sizes = ((string)_configuration.GetValue<string>("ImageSizes")).Split(" ");
            foreach (var s in sizes)
            {
                var imgDelete = Path.Combine(dirSave, s + "_" + category.Image);
                if (System.IO.File.Exists(imgDelete))
                {
                    System.IO.File.Delete(imgDelete);
                }
            }
            _appEFContext.Categories.Remove(category);
            await _appEFContext.SaveChangesAsync();
            return Ok();
        }
    }
}
