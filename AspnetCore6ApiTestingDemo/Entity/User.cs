namespace AspnetCore6ApiTestingDemo.Entity;

public class User : EntityBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool IsPassive { get; set; }
}