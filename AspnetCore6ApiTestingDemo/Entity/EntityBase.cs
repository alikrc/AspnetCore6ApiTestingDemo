using System.Text.Json;

namespace AspnetCore6ApiTestingDemo.Entity;

public abstract class EntityBase
{
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}