using AutoMapper;
using WebShop.Data.Entities;
using WebShop.Data.Entities.Identity;
using WebShop.Data.Entities.Product;
using WebShop.Models;
using WebShop.Models.Items;
using WebShop.Models.Users;

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
            CreateMap<UserEntity, UserViewModel>().ForMember(x=>x.Role, opt => opt.MapFrom(x => x.UserRoles.FirstOrDefault().Role));
               
            CreateMap<ProductImagesEntity, ProductImageItemViewModel>().ForMember(x => x.Name, opt => opt.MapFrom(x=>x.UrlImage));
            //CreateMap<ProductEntity, ProductGetViewModel>().ForMember(x=> x.images, opt => opt.MapFrom(x=> x.Images));
            
            
        }
        
    }
}
