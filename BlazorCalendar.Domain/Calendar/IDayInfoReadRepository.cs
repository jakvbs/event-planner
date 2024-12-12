namespace BlazorCalendar.Domain.Calendar;

public interface IDayInfoReadRepository
{
    Task<IEnumerable<DayInfo>> GetDayEvents(string userId, int month, int year);
}