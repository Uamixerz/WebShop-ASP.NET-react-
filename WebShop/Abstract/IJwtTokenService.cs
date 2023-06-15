using WebShop.Data.Entities.Identity;

namespace WebShop.Abstract
{
    public interface IJwtTokenService
    {
        Task<string> CreateToken(UserEntity user);
    }
}
