using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Drawing;
using WebShop.Data;
using WebShop.Data.Entities.Identity;
using WebShop.Helpers;
using WebShop.Models;
using WebShop.Models.Users;

namespace WebShop.Controllers
{
    public class UserController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly AppEFContext _appEFContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserController(AppEFContext appEFContext, IWebHostEnvironment hostingEnvironment, IConfiguration configuration, IMapper mapper)
        {
            _appEFContext = appEFContext;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _mapper = mapper;
        }

        
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var result = await _appEFContext.UserRoles.Select(x => _mapper.Map<UserViewModel>(x)).ToListAsync();

            return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromForm] UserEditViewModel model)
        {
            var user = await _appEFContext.Users.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (user == null)
                return NotFound();

            String fileName = string.Empty;
            if (model.Image != null)
            {
                var imageOld = user.Image;

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
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Image = string.IsNullOrEmpty(fileName) ? user.Image : fileName;

            var role = await _appEFContext.UserRoles.SingleOrDefaultAsync(x => x.User.Id == model.Id);
            if (role != null)
            {
                var roleEx = await _appEFContext.Roles.SingleOrDefaultAsync(x => x.Id == model.Role);
                if (roleEx != null)
                {
                    _appEFContext.UserRoles.Remove(role);
                    var newUserRole = new UserRoleEntity
                    {
                        UserId = model.Id,
                        RoleId = model.Role
                    };
                    _appEFContext.UserRoles.Add(newUserRole);
                }
            }
           

            await _appEFContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _appEFContext.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            try { 
            var dirSave = Path.Combine(_hostingEnvironment.ContentRootPath, "Data", "Uploads");
            string[] sizes = ((string)_configuration.GetValue<string>("ImageSizes")).Split(" ");
            foreach (var s in sizes)
            {
                var imgDelete = Path.Combine(dirSave, s + "_" + user.Image);
                if (System.IO.File.Exists(imgDelete))
                {
                    System.IO.File.Delete(imgDelete);
                }
            }
            }catch (Exception ex) { }
            var role = await _appEFContext.UserRoles.SingleOrDefaultAsync(x => x.User.Id == id);
            if (role != null)
             _appEFContext.UserRoles.Remove(role); 
            
                _appEFContext.Users.Remove(user);
            await _appEFContext.SaveChangesAsync();
            return Ok();
        }
    }
}
