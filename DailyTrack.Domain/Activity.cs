using CSharpFunctionalExtensions;

namespace DailyTrack.Domain;

public class Activity : Entity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? StartedAt
    {
        get
        {
            var lastLog = Logs.LastOrDefault();
            if (lastLog == null)
                return null;

            if (lastLog.Status == TaskStatusEvent.Started)
            {
                return lastLog.Timestamp;
            }
            
            return null; // Task has not started yet
        }
    }
    public TimeSpan Duration
    {
        get
        {
            TimeSpan duration = TimeSpan.Zero;
            bool isRunning = false;
            DateTime startTime = DateTime.MinValue;

            foreach (var log in _logs)
            {
                if (log.Status == TaskStatusEvent.Started)
                {
                    isRunning = true;
                    startTime = log.Timestamp;
                }
                else if (log.Status == TaskStatusEvent.Stopped && isRunning)
                {
                    duration += log.Timestamp - startTime;
                    isRunning = false;
                }
            }

            // If the task is still running, calculate the duration until now
            if (isRunning)
            {
                duration += DateTime.Now - startTime;
            }

            return duration;
        }
    }

    public ActivityStatus Status
    {
        get
        {
            if (_logs.Count == 0)
                return ActivityStatus.NotStarted;

            if (_logs.Last().Status == TaskStatusEvent.Started)
                return ActivityStatus.InProgress;

            return ActivityStatus.Stopped;
        }
    }
    public ActivitiyType Type { get; set; }

    private List<TaskLog> _logs = new List<TaskLog>();
    public IReadOnlyList<TaskLog> Logs => _logs;

    public Activity(Guid id, string name, string description, DateTime createdAt, ActivitiyType type, IReadOnlyList<TaskLog> logs)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        Type = type;
        _logs = logs.ToList();
    }

    private Activity(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CreatedAt = DateTime.Now;
        Type = ActivitiyType.Other;
    }

    public static Result<Activity> Create(string name, string description, DateTime createdAt, ActivitiyType type)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Activity>("Name cannot be empty");

        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Activity>("Description cannot be empty");


        return Result.Success(new Activity(Guid.NewGuid(), name, description, createdAt, type, new List<TaskLog>()));

       // return Result.Success(new Activity(name, description));
    }

    public void Start()
    {
        if (StartedAt.HasValue)
            throw new InvalidOperationException("Activity already started");

        _logs.Add(new TaskLog { Timestamp = DateTime.Now, Status = TaskStatusEvent.Started });
    }

    public void Stop()
    {
        if (!StartedAt.HasValue)
            throw new InvalidOperationException("Activity not started");

        _logs.Add(new TaskLog { Timestamp = DateTime.Now, Status = TaskStatusEvent.Stopped });
    }

    public class TaskLog
    {
        public DateTime Timestamp { get; set; }
        public TaskStatusEvent Status { get; set; }
    }

    public enum TaskStatusEvent
    {
        Started,
        Stopped
    }
}
