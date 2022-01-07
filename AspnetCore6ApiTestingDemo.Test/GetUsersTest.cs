using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using AspnetCore6ApiTestingDemo.Infra;
using AspnetCore6ApiTestingDemo.Entity;
using Microsoft.Extensions.DependencyInjection;
using AspnetCore6ApiTestingDemo.Model;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace AspnetCore6ApiTestingDemo.Test;

public class GetUsersTest : TestBase
{
    private IMapper mapper;

    public GetUsersTest() : base(new TestWebApplicationFactory<Program>())
    {
    }

    [SetUp]
    protected void Init()
    {
        mapper = ServiceProvider.GetService<IMapper>();
    }

    [Test]
    public async Task Get_User_Should_return_valid_data()
    {
        //setup
        var user = await CreateUserAsync();
        var expectedUser = mapper.Map<UserDto>(user);

        //act
        var url = $"api/user";
        var response = await Client.GetAsync(url);

        var list = await GetDtoFromResponse<List<UserDto>>(response);

        var actualUser = list.FirstOrDefault();

        expectedUser.Should().BeEquivalentTo(actualUser);
    }

    protected async Task<User> CreateUserAsync()
    {
        var repository = ServiceProvider.GetService<IRepository<User>>();
        var user = new User()
        {
            Name = "John",
            Surname = "Doe",
        };

        await repository.AddAsync(user);
        await repository.SaveAsync();
        return user;
    }

    protected async Task<T> GetDtoFromResponse<T>(HttpResponseMessage response)
    {
        var responseBody = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(responseBody, JsonSerializerOptions);
    }
}