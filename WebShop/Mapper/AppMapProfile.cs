using AutoMapper;
using WebShop.Data.Entities;
using WebShop.Data.Entities.Identity;
using WebShop.Models;
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
            CreateMap<UserRoleEntity, UserViewModel>()
                .ForMember(x => x.Role, opt => opt.MapFrom(x => x.Role.Name))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.User.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.User.LastName))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.User.Email))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.User.Id))
                .ForMember(x => x.Image, opt => opt.MapFrom(x => x.User.Image));
        }
        
    }
}
