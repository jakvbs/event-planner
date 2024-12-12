using BlazorCalendar.Application.DTOs;
using BlazorCalendar.Domain.Calendar;
using BlazorCalendar.Shared.DTOs.Calendar;
using MediatR;
using Microsoft.Extensions.Options;

namespace BlazorCalendar.Application.Features.Calendar.Queries;

public class CalendarRequestHandler(
    IOptions<CalendarSettings> calendarSettingOptions,
    IDayInfoReadRepository dayInfoRepository
) : IRequestHandler<CalendarRequest, CalendarResponse>
{
    private readonly CalendarSettings calendarSettings = calendarSettingOptions.Value;

    public async Task<CalendarResponse> Handle(CalendarRequest request, CancellationToken cancellationToken)
    {
        var events = (await dayInfoRepository.GetDayEvents(request.UserId, request.Month, request.Year))
            .Select(e => new DayInfoResponse(e.Id, e.Timestamp, e.Title, e.Description))
            .ToList();

        var currentYear = DateTime.UtcNow.Year;

        var years = new List<int>();
        for (var y = calendarSettings.MinYear; y <= currentYear + 2; y++) years.Add(y);

        var months = GetMonthsList();
        var response = new CalendarResponse(events, months, years);

        return response;
    }

    private IList<MonthModel> GetMonthsList()
    {
        return new List<MonthModel>
        {
            new(1, "January"),
            new(2, "Feburary"),
            new(3, "March"),
            new(4, "April"),
            new(5, "May"),
            new(6, "June"),
            new(7, "July"),
            new(8, "August"),
            new(9, "September"),
            new(10, "October"),
            new(11, "November"),
            new(12, "December")
        };
    }
}