namespace BlazorCalendar.Domain.User;

public class UserInfo : EntityBase
{
    public UserName Name { get; init; } = new();
    public string PasswordHash { get; init; } = null!;
    public string? Email { get; init; }

    public string GetFullName()
    {
        return $"{Name.FirstName} {Name.LastName}";
    }
}