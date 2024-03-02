using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyTrack.Domain
{
    public enum ActivityStatus
    {
        NotStarted,
        InProgress,
        Stopped
    }

    public static class TaskStatusExtensions
    {
        public static string ToFriendlyString(this ActivityStatus status)
        {
            switch (status)
            {
                case ActivityStatus.NotStarted:
                    return "Not Started";
                case ActivityStatus.InProgress:
                    return "In Progress";
                case ActivityStatus.Stopped:
                    return "Stopped";
                default:
                    return "Unknown";
            }
        }
    }
}
