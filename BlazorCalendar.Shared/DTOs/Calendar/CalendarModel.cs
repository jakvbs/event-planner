namespace BlazorCalendar.Shared.DTOs.Calendar;

public record CalendarModel(CalendarResponseModel CurrentCalendar)
{
    public static readonly CalendarModel Empty = new(
        new CalendarResponseModel()
    );
}