using BlazorCalendar.Shared.DTOs.Calendar;

namespace BlazorCalendar.Application.Features.Calendar.Queries;

public class CalendarResponse(
    IList<DayInfoResponse> days,
    IList<MonthModel> months,
    IList<int> years)
{
    public IList<DayInfoResponse> Days { get; } = days;

    public IList<MonthModel> Months { get; } = months;
    public IList<int> Years { get; } = years;
}