using AutoMapper;
using WebShop.Data.Entities;
using WebShop.Data.Entities.Characteristics;
using WebShop.Data.Entities.Identity;
using WebShop.Data.Entities.Product;
using WebShop.Models;
using WebShop.Models.Characteristics;
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

            CreateMap<ProductImagesEntity, ProductImageItemViewModel>().ForMember(x => x.Name, opt => opt.MapFrom(x => x.UrlImage));

            //CreateMap<ProductEntity, ProductGetViewModel>().ForMember(x=> x.images, opt => opt.MapFrom(x=> x.Images));
            CreateMap<ProductEntity, ProductGetHomePageViewModel>()
           .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

            CreateMap<ProductViewModel, ProductEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ігнорувати Id, якщо він вже існує
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<CharacteristicGetViewModel, CharacteristicsEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<CharacteristicsEntity, CharacteristicViewModel>();
            CreateMap< CharacteristicViewModel, CharacteristicsEntity>();


        }
        
    }
}
