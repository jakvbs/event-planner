using System.ComponentModel.DataAnnotations;

namespace BlazorCalendar.Shared.DTOs.Calendar;

public class EventData
{
    [Required] public string UserId { get; init; } = default!;
    public string? Description { get; init; }
    public string? Title { get; init; }
    public DateTime Timestamp { get; init; }
}