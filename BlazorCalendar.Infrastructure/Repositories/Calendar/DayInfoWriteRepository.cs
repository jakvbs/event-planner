using BlazorCalendar.Domain.Calendar;
using MongoDB.Driver;

namespace BlazorCalendar.Infrastructure.Repositories.Calendar;

public class DayInfoWriteRepository(IMongoDatabase mongoDatabase) : IDayInfoWriteRepository
{
    private readonly IMongoCollection<DayInfo> _events = mongoDatabase.GetCollection<DayInfo>(nameof(DayInfo));

    public async Task<DayInfo> AddDayEvent(string userId, DateTime timeStamp, string title, string description)
    {
        var dayEvent = new DayInfo
        {
            Id = string.Empty,
            UserId = userId,
            Title = title,
            Description = description,
            Timestamp = timeStamp
        };

        await _events.InsertOneAsync(dayEvent);

        return dayEvent;
    }

    public async Task UpdateDayEvent(string eventId, DateTime timeStamp, string title, string description)
    {
        var filterDefinition = new FilterDefinitionBuilder<DayInfo>().Where(e => e.Id == eventId);
        var update = new UpdateDefinitionBuilder<DayInfo>()
            .Set(e => e.Description, description)
            .Set(e => e.Title, title)
            .Set(e => e.Timestamp, timeStamp);

        await _events.UpdateOneAsync(filterDefinition, update);
    }

    public async Task DeleteDayEvent(string eventId)
    {
        var filterDefinition = new FilterDefinitionBuilder<DayInfo>().Where(e => e.Id == eventId);

        await _events.DeleteOneAsync(filterDefinition);
    }
}