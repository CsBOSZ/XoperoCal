using xopCal.Model;

namespace xopCal.Services;

public interface IAuthService
{
    void RegisterUser(UserDto dto);
    string GetJwt(LoginDto dto);
}