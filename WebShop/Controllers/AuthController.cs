using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Data.Entities;
using WebShop.Data;
using WebShop.Models;
using Microsoft.AspNetCore.Identity;
using WebShop.Data.Entities.Identity;
using WebShop.Models.Auth;
using WebShop.Constants;
using WebShop.Abstract;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(UserManager<UserEntity> userManager, IWebHostEnvironment hostingEnvironment, IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
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
                else
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
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                    return BadRequest("Не вірно вказані дані");
                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                    return BadRequest("Не вірно вказані дані");
                var token = await _jwtTokenService.CreateToken(user);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
