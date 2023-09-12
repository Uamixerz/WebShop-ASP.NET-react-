using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using WebShop.Data;
using WebShop.Data.Entities.Characteristics;
using WebShop.Data.Entities.Product;
using WebShop.Models;
using WebShop.Models.Characteristics;
using WebShop.Models.Items;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacteristicsController : Controller
    {

        private readonly AppEFContext _appEFContext;
        private readonly IMapper _mapper;
        public CharacteristicsController(AppEFContext appEFContext, IMapper mapper)
        {
            _appEFContext = appEFContext;
            _mapper = mapper;

        }
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var result2 = await _appEFContext.Characteristics.Include(p => p.CharacteristicsCategory)
                .Select(x => new CharacteristicGetViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Categories = x.CharacteristicsCategory.Select(p => new CategoryForCharacteristic { Id = p.Category.Id, Name = p.Category.Name }).ToList()
                }).ToListAsync();


            return Ok(result2);
        }

        [HttpPost("AddCharacteristic")]
        public async Task<IActionResult> AddCharacteristic([FromForm] CharacteristicViewModel model)
        {
            try
            {
                if (model.Name != null)
                {
                    var charact = _mapper.Map<CharacteristicsEntity>(model);

                    _appEFContext.Add(charact);
                    _appEFContext.SaveChanges();

                    foreach (int categoryId in model.CategoriesId)
                    {
                        var categoryCharact = new CharacteristicsCategoryEntity
                        {
                            CategoryId = categoryId,
                            CharacteristicId = charact.Id
                        };

                        _appEFContext.CharacteristicsCategory.Add(categoryCharact);
                    }
                    _appEFContext.SaveChanges();

                    return Ok(_mapper.Map<CharacteristicViewModel>(charact));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest(404);
        }
        [HttpPut("EditCharacteristic")]
        public async Task<IActionResult> EditCharacteristic([FromForm] CharacteristicEditViewModel model)
        {
            var charact = await _appEFContext.Characteristics.FirstOrDefaultAsync(p => p.Id == model.IdCharacteristic);
            if (charact == null)
                return NotFound();
            try
            {
                charact.Name = model.Name;
                var categoriesToDelete = _appEFContext.CharacteristicsCategory
                    .Where(p => !model.CategoriesId.Contains(p.CategoryId) && p.CharacteristicId == charact.Id).ToList();
                _appEFContext.CharacteristicsCategory.RemoveRange(categoriesToDelete);
                foreach (int categoryId in model.CategoriesId)
                {
                    var categoryCharact = new CharacteristicsCategoryEntity
                    {
                        CategoryId = categoryId,
                        CharacteristicId = charact.Id
                    };
                    if(_appEFContext.CharacteristicsCategory.FirstOrDefault(p=>p.CharacteristicId == charact.Id && p.CategoryId == categoryId) == null)
                        _appEFContext.CharacteristicsCategory.Add(categoryCharact);
                }
                _appEFContext.SaveChanges();
                return Ok(_mapper.Map<CharacteristicViewModel>(charact));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var characteristic = await _appEFContext.Characteristics.SingleOrDefaultAsync(x => x.Id == id);
                if (characteristic == null)
                    return NotFound();

                var catCharacteristic = await _appEFContext.CharacteristicsCategory.Where(x => x.CharacteristicId == id).ToListAsync();
                _appEFContext.CharacteristicsCategory.RemoveRange(catCharacteristic);

                var prodCharacteristic = await _appEFContext.CharacteristicsProduct.Where(x => x.CharacteristicId == id).ToListAsync();
                _appEFContext.CharacteristicsProduct.RemoveRange(prodCharacteristic);
                await _appEFContext.SaveChangesAsync();
                _appEFContext.Characteristics.Remove(characteristic);
                await _appEFContext.SaveChangesAsync();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
    
}

