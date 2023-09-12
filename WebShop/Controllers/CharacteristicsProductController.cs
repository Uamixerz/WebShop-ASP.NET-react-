using Microsoft.AspNetCore.Mvc;
using WebShop.Data.Entities.Characteristics;
using WebShop.Data;
using WebShop.Models.Characteristics;
using AutoMapper;

namespace WebShop.Controllers
{
    public class CharacteristicsProductController : Controller
    {
        private readonly AppEFContext _appEFContext;
        private readonly IMapper _mapper;
        public CharacteristicsProductController(AppEFContext appEFContext, IMapper mapper)
        {
            _appEFContext = appEFContext;
            _mapper = mapper;

        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddCharacteristicProduct([FromForm] CharacteristicViewModel model)
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
            return BadRequest(404);
        }
    }
}
