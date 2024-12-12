using BlazorCalendar.Domain.User;

namespace BlazorCalendar.Domain.Identity;

public interface IUserWriteRepository
{
    Task<UserInfo> CreateUser(string email, string firstName, string lastName, string password);
}