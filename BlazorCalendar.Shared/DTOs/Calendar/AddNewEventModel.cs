using System.ComponentModel.DataAnnotations;

namespace BlazorCalendar.Shared.DTOs.Calendar;

public class AddOrEditEventModel(string userId) : IValidatableObject
{
    public string UserId { get; } = userId;
    public string? EventId { get; set; }
    public int Day { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Minutes { get; set; }
    public int Hours { get; set; }
    public List<string> Errors { get; private set; } = [];
    public IEnumerable<DayEvent>? DayEvents { get; set; }
    public DateTime Timestamp => new(Year, Month, Day, Hours, Minutes, 0);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new HashSet<ValidationResult>();

        if (string.IsNullOrEmpty(Title)) results.Add(new ValidationResult("Please enter an event title."));

        if (string.IsNullOrEmpty(Description)) results.Add(new ValidationResult("Please enter an event description."));

        if (DayEvents is not null)
        {
            var utcDate = Timestamp.ToUniversalTime();
            var eventExists = DayEvents.Any(
                e => e.Timestamp.Hour == utcDate.Hour &&
                     e.Timestamp.Minute == utcDate.Minute &&
                     (EventId is null || e.Id != EventId));

            if (eventExists) results.Add(new ValidationResult("This time has already been scheduled for other event."));
        }

        Errors = results.Select(r => r.ErrorMessage!).ToList();
        return results;
    }

    public void SetTime(int hours, int minutes)
    {
        Hours = hours;
        Minutes = minutes;
    }

    public void Reset()
    {
        EventId = null;
        Description = null;
        Title = null;
        SetTime(0, 0);
        Errors.RemoveAll(_ => true);
        DayEvents = null;
    }
}