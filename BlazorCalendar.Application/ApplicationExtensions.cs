using BlazorCalendar.Application.DTOs;
using BlazorCalendar.Application.Features.Calendar.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorCalendar.Application;

public static class ApplicationExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<CalendarResponseMapper>();

        services.Configure<CalendarSettings>(cfg =>
        {
            var calendarSettings = new CalendarSettings();
            configuration.GetSection("CalendarSettings").Bind(calendarSettings);
            cfg.MinYear = calendarSettings.MinYear;
        });

        services.AddMediatR(config => { config.RegisterServicesFromAssembly(typeof(AppMarker).Assembly); });
    }
}