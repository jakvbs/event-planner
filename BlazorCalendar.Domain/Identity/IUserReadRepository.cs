using BlazorCalendar.Domain.User;

namespace BlazorCalendar.Domain.Identity;

public interface IUserReadRepository
{
    Task<UserInfo?> GetUserByEmail(string email);
}