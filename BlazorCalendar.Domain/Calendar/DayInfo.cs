using BlazorCalendar.Domain.User;

namespace BlazorCalendar.Domain.Calendar;

public class DayInfo : EntityBase
{
    public DateTime Timestamp { get; init; }
    public string Title { get; init; } = default!;
    public string Description { get; init; } = default!;
    public required string UserId { get; init; }
    public UserInfo? User { get; set; }
}