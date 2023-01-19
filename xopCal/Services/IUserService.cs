using xopCal.Entity;
using xopCal.Model;

namespace xopCal.Services;

public interface IUserService
{
    UserDtoOut? GetUserById(int? id,bool events);
    UserDtoOut? GetUserByEmail(string email,bool events);
    List<UserDtoOut> GetAllUserByName(string name,bool events);
    
    List<UserDtoOut> GetAll(bool events);
}