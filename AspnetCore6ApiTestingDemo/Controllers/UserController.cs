using AspnetCore6ApiTestingDemo.Entity;
using AspnetCore6ApiTestingDemo.Infra;
using AspnetCore6ApiTestingDemo.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCore6ApiTestingDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;
    private readonly IRepository<User> _repository;

    public UserController(ILogger<UserController> logger, IRepository<User> repository, IMapper mapper)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<List<UserDto>> Get()
    {
        var list = await _repository.ListAllAsync();

        return _mapper.Map<List<UserDto>>(list);
    }
}