using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Data.Entities;
using WebShop.Data;
using WebShop.Models;
using Microsoft.AspNetCore.Identity;
using WebShop.Data.Entities.Identity;
using WebShop.Models.Auth;
using WebShop.Constants;


namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<UserEntity> _userManager;

        public AuthController(UserManager<UserEntity> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
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

                var userEntity = new UserEntity()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                    Image = fileName
                };
                var result = await _userManager.CreateAsync(userEntity, model.Password);

                if (result.Succeeded)
                {
                    result = _userManager.AddToRoleAsync(userEntity, Roles.User).Result;
                    return Ok(result);
                }
                {
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    return BadRequest("Ooops");
                }
            }
            else
                return BadRequest("No file uploaded");
        }
    }
}
