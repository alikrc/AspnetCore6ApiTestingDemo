using AspnetCore6ApiTestingDemo.Entity;
using AspnetCore6ApiTestingDemo.Model;
using AutoMapper;

namespace AspnetCore6ApiTestingDemo.Startup;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<User, UserDto>();
    }
}