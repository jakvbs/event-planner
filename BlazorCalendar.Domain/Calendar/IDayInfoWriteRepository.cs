namespace BlazorCalendar.Domain.Calendar;

public interface IDayInfoWriteRepository
{
    Task<DayInfo> AddDayEvent(string userId, DateTime timeStamp, string title, string description);
    Task UpdateDayEvent(string eventId, DateTime timeStamp, string title, string description);
    Task DeleteDayEvent(string eventId);
}