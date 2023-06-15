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
        public CategoriesController(AppEFContext appEFContext, IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _appEFContext = appEFContext;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;

        }
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            //var result = await _appEFContext.Categories.ToListAsync();
            var result = new List<CategoryGetViewModel>();
            foreach(var category in _appEFContext.Categories)
            {
                result.Add(new CategoryGetViewModel
                {
                    Name = category.Name,
                    Description = category.Description,
                    Image = category.Image,
                    ParentId = category.ParentId,
                    Id = category.Id,
                    Priority = category.Priority
                });
            }
            
            return Ok(result);
        }
        [HttpGet("list{id}")]
        public async Task<IActionResult> List(int id)
        {
            //var result = await _appEFContext.Categories.ToListAsync();
            var result = new List<CategoryGetViewModel>();
            foreach (var category in _appEFContext.Categories.Where(i=> i.ParentId == id).ToList())
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
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CategoryCreateViewModel model)
        {
            if (model.Image != null && model.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "Data", "Uploads");

                var fileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);

                //using (var fileStream = new FileStream(filePath, FileMode.Create))
                //{
                //    model.Image.CopyTo(fileStream);
                //}
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

                CategoryEntity category = new CategoryEntity
                {
                    DateCreated = DateTime.UtcNow,
                    Name = model.Name,
                    Description = model.Description,
                    Image = fileName,
                    Priority = model.Priority,
                    ParentId = model.ParentId == 0 ? null : model.ParentId,
                };
                await _appEFContext.AddAsync(category);
                await _appEFContext.SaveChangesAsync();
                return Ok(category);
            }
            else
                return BadRequest("No file uploaded");
        }

    }
}
