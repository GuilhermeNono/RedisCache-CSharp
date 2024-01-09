using RedisCachingProject.Database.Model;

namespace RedisCachingProject.DTO;

public class PersonResult
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public int Age { get; set; }
    public string LastName { get; set; }

    public static PersonResult ToResult(Person person)
    {
        return new()
        {
            Id = person.Id,
            LastName = person.LastName,
            Age = person.Age,
            FirstName = person.FirstName
        };
    }
}