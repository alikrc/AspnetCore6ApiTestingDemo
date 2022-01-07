using System.Text.Json;

namespace AspnetCore6ApiTestingDemo.Model;

public class DtoBase
{
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}