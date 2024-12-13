using BlazorCalendar.Shared.DTOs.Calendar;
using BlazorCalendar.Shared.UseCases.Calendar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCalendar.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class CalendarController : ControllerBase
{
    private readonly IGetCalendar _getCalendarUseCase;
    private readonly IAddDayEvent _addDayEventUseCase;
    private readonly IUpdateDayEvent _updateDayEventUseCase;
    private readonly IDeleteDayEvent _deleteDayEventUseCase;

    public CalendarController(
        IGetCalendar getCalendarUseCase,
        IAddDayEvent addDayEventUseCase,
        IUpdateDayEvent updateDayEventUseCase,
        IDeleteDayEvent deleteDayEventUseCase)
    {
        _getCalendarUseCase = getCalendarUseCase;
        _addDayEventUseCase = addDayEventUseCase;
        _updateDayEventUseCase = updateDayEventUseCase;
        _deleteDayEventUseCase = deleteDayEventUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetCalendar([FromQuery] int year, [FromQuery] int month, [FromQuery] string userId)
    {
        var result = await _getCalendarUseCase.GetAsync(year, month, userId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] EventData requestModel)
    {
        var timestamp = requestModel.Timestamp;
        var addNewEventModel = new AddOrEditEventModel(requestModel.UserId!)
        {
            Day = timestamp.Day,
            Month = timestamp.Month,
            Year = timestamp.Year,
            Title = requestModel.Title,
            Description = requestModel.Description,
            Hours = timestamp.Hour,
            Minutes = timestamp.Minute
        };

        var eventAdded = await _addDayEventUseCase.ProcessAsync(addNewEventModel);

        return eventAdded.IsSuccess 
            ? Ok(new NewEventCreatedResponse(eventAdded.EventId!))
            : BadRequest();
    }

    [HttpPut("{eventId}")]
    public async Task<IActionResult> UpdateEvent(string eventId, [FromBody] EventData requestModel)
    {
        var timestamp = requestModel.Timestamp;
        var updateEventModel = new AddOrEditEventModel(requestModel.UserId!)
        {
            EventId = eventId,
            Day = timestamp.Day,
            Month = timestamp.Month,
            Year = timestamp.Year,
            Title = requestModel.Title,
            Description = requestModel.Description,
            Hours = timestamp.Hour,
            Minutes = timestamp.Minute
        };

        var eventUpdated = await _updateDayEventUseCase.ProcessAsync(updateEventModel);

        return eventUpdated.IsSuccess ? NoContent() : BadRequest();
    }

    [HttpDelete("{eventId}")]
    public async Task<IActionResult> DeleteEvent(string eventId)
    {
        await _deleteDayEventUseCase.DeleteEvent(eventId);
        return Ok();
    }
} 