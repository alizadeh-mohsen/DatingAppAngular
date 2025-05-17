using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
        UserDto CreateToken(DatingAppUser user);
    }
}
