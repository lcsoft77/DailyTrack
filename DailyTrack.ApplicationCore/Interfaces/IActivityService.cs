using DailyTrack.Domain;

namespace DailyTrack.ApplicationCore;

public interface IActivityService
{
    Task AddActivity(Activity activity);
    Task DeleteActivity(Guid id);
    Task<List<Activity>> GetActivitiesForDate(DateTime startDate, DateTime endDate);
    Task<Activity> GetActivityById(Guid id);
    Task UpdateActivity(Activity activity);
}
