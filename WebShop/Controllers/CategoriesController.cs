using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Data.Entities;
using WebShop.Models;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        private readonly AppEFContext _appEFContext;
        public CategoriesController(AppEFContext appEFContext, IWebHostEnvironment hostingEnvironment)
        {
            _appEFContext = appEFContext;
            _hostingEnvironment = hostingEnvironment;
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

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
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
