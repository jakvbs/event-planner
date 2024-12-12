using BlazorCalendar.Shared.DTOs.Calendar;

namespace BlazorCalendar.Shared.UseCases.Calendar;

public interface IDayEventProcessor
{
    Task<ProcessDayEventResult> ProcessAsync(AddOrEditEventModel eventModel);
}