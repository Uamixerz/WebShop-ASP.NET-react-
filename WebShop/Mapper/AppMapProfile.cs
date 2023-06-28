using AutoMapper;
using WebShop.Data.Entities;
using WebShop.Models;

namespace WebShop.Mapper
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            CreateMap<CategoryEntity, CategoryGetViewModel>();
           // .ForMember(x => x.ParentId, opt => opt.MapFrom(x => x.Parent.Id));
            CreateMap<CategoryCreateViewModel, CategoryEntity>()
                .ForMember(x => x.ParentId, opt => opt.MapFrom(x=>x.ParentId==0?null : x.ParentId))
                .ForMember(x => x.Image, opt => opt.Ignore());
        }
        
    }
}
