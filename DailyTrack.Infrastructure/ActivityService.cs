using DailyTrack.ApplicationCore;
using DailyTrack.Domain;
using LiteDB;

namespace DailyTrack.Infrastructure;

public class ActivityService : IActivityService
{
    private LiteDatabase _database;

    public ActivityService(string connectionString)
    {
        _database = new LiteDatabase(connectionString);
    }

    public async Task AddActivity(Activity activity)
    {
        var activities = _database.GetCollection<ActivitiyDB>("activities");
        var task = new ActivitiyDB
        {
            Name = activity.Name,
            Description = activity.Description,
            CreatedAt = activity.CreatedAt,
            Type = activity.Type,
            Logs = activity.Logs.ToList()
        };
        activities.Insert(task);
    }

    public async Task UpdateActivity(Activity activity)
    {
        var activities = _database.GetCollection<ActivitiyDB>("activities");
        var task = activities.FindById(activity.Id);

        if (task == null)
        {
            throw new InvalidOperationException("Task not found");
        }

        task.Name = activity.Name;
        task.Description = activity.Description;
        task.Type = activity.Type;
        task.Logs = activity.Logs.ToList();

        activities.Update(task);
    }

    public async Task<List<Activity>> GetActivitiesForDate(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            throw new InvalidOperationException("Start date cannot be greater than end date");
        }
        if (startDate == endDate)
        {
            endDate = endDate.AddDays(1);
        }
        var activities = _database.GetCollection<ActivitiyDB>("activities");
        var query = Query.And(
            Query.GTE("CreatedAt", startDate.Date),
            Query.LT("CreatedAt", endDate.Date)
        );
        var tasks =  activities.Find(query).ToList();

        return tasks.Select(t => new Activity(t.Id, t.Name, t.Description, t.CreatedAt, t.Type, t.Logs)).ToList();
    }
    
    public Task<Activity> GetActivityById(Guid id)
    {
        var activities = _database.GetCollection<ActivitiyDB>("activities");
        var task = activities.FindById(id);
        if (task == null)
        {
            return Task.FromResult<Activity>(null);
        }

        return Task.FromResult(new Activity(task.Id, task.Name, task.Description, task.CreatedAt, task.Type, task.Logs));
    }

    public Task DeleteActivity(Guid id)
    {
        var activities = _database.GetCollection<ActivitiyDB>("activities");
        activities.Delete(id);
        return Task.CompletedTask;
    }
}
