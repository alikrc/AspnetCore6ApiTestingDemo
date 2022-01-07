using System.Text.Json;

namespace AspnetCore6ApiTestingDemo.Model;

public class UserDto : DtoBase
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Surname { get; set; }
}