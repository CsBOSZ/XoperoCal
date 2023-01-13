using AutoMapper;
using xopCal.Entity;

namespace xopCal.Model.Mappers;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<EventCal, EventCalDtoOut>()
            .ForMember(e => e.OwnerName,
                c => c.MapFrom(s =>$"{s.Owner.Name}" ))
            .ForMember(e => e.OwnerEmail,
                c => c.MapFrom(s =>$"{s.Owner.Email}"));

        CreateMap<User, UserDtoOut>();

        CreateMap<EventCal, EventCalDtoOut2>();
    }
}