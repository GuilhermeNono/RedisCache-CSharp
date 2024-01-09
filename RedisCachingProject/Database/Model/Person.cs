using Dapper.Contrib.Extensions;

namespace RedisCachingProject.Database.Model;

[Table("Person")]
public class Person
{
    [Key] 
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string LastName { get; set; } = string.Empty;
}