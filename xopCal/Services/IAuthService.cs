using xopCal.Model;

namespace xopCal.Services;

public interface IAuthService
{
    void RegisterUser(UserDtoIn dtoIn);
    string GetJwt(LoginDto dto);
}