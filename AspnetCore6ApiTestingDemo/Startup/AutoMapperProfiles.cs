using AspnetCore6ApiTestingDemo.Entity;
using AspnetCore6ApiTestingDemo.Model;
using AutoMapper;

namespace AspnetCore6ApiTestingDemo.Startup;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<User, UserDto>()
            .ForMember(a => a.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(a => a.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(a => a.Surname, opt => opt.MapFrom(src => src.Surname));
    }
}