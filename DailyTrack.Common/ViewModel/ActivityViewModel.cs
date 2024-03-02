using System;
using System.Collections.Generic;
using static DailyTrack.Domain.Activity;

namespace DailyTrack.Common.ViewModel
{
    public class ActivityViewModel
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public string StartedAtString { get { return StartedAt?.ToShortTimeString() ?? string.Empty; } }
        public TimeSpan Duration
        {
            get
            {
                TimeSpan duration = TimeSpan.Zero;
                bool isRunning = false;
                DateTime startTime = DateTime.MinValue;

                foreach (var log in Logs)
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
        public string DurationString { get { return Duration.ToString(@"hh\:mm\:ss"); } }
        public ActivitiyStatusViewModel Status { get; set; }
        public ActivitiyTypeViewModel Type { get; set; }
        public int TypeInt { get { return Type.ToInt(); } set { Type = (ActivitiyTypeViewModel)value; } }

        public IReadOnlyList<TaskLog> Logs { get; set; }
    }
}
