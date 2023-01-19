using AutoMapper;
using Microsoft.EntityFrameworkCore;
using xopCal.Entity;
using xopCal.Model;

namespace xopCal.Services;

public class UserService : IUserService
{
    private readonly EventDbContext _context;
    private readonly IMapper _mapper;
    private static Dictionary<int, string> _connect = new Dictionary<int, string>();

    public void AddConnectUser(int id, string connectId)
    {
        _connect.Add(id,connectId);
    }
    public void DeleteConnectUser(int id)
    {
        _connect.Remove(id);
    }
    public UserService(EventDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public UserDtoOut? GetUserById(int? id,bool events)
    {
        if (id is null)
        {
            return null;
        }

        if (events)
        {
            return _mapper.Map<UserDtoOut>(_context.Users.Include(u => u.EventCals).Include(u => u.SubscribeEventCals).FirstOrDefault(u => u.Id == id));

        }
        return _mapper.Map<UserDtoOut>(_context.Users.FirstOrDefault(u => u.Id == id));
    }
    
    public UserDtoOut? GetUserByEmail(string email,bool events)
    {
        if (events)
        {
            return _mapper.Map<UserDtoOut>(_context.Users.Include(u => u.EventCals).Include(u => u.SubscribeEventCals).FirstOrDefault(u => u.Email == email));

        }
        return _mapper.Map<UserDtoOut>(_context.Users.FirstOrDefault(u => u.Email == email));
    }

    public List<UserDtoOut> GetAllUserByName(string name,bool events)
    {

        if (events)
        {
            return _mapper.Map<List<UserDtoOut>>(_context.Users.Where(u => u.Name.Contains(name)).Include(u => u.SubscribeEventCals).Include(u => u.EventCals).ToList());

        }
        return _mapper.Map<List<UserDtoOut>>(_context.Users.Where(u => u.Name.Contains(name)).ToList());

    }

    public List<UserDtoOut> GetAll(bool events)
    {
        if (events)
        {
            return _mapper.Map<List<UserDtoOut>>(_context.Users.Include(u => u.EventCals).Include(u => u.SubscribeEventCals).ToList());

        }
        return _mapper.Map<List<UserDtoOut>>(_context.Users.ToList());
    }
}