namespace BlazorCalendar.Shared.DTOs.Calendar;

public class CalendarResponseModel
{
    public IList<DayEvent> DayEvents { get; init; } = [];
    public IList<MonthModel> Months { get; init; } = [];
    public IList<int> Years { get; init; } = [];
}