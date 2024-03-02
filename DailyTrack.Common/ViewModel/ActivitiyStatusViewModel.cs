using DailyTrack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyTrack.Common.ViewModel
{
    public enum ActivitiyStatusViewModel
    {
        NotStarted,
        InProgress,
        Stopped
    }

    public static class TaskStatusExtensions
    {
        public static string ToFriendlyString(this ActivitiyStatusViewModel status)
        {
            switch (status)
            {
                case ActivitiyStatusViewModel.NotStarted:
                    return "Not Started";
                case ActivitiyStatusViewModel.InProgress:
                    return "In Progress";
                case ActivitiyStatusViewModel.Stopped:
                    return "Stopped";
                default:
                    return "Unknown";
            }
        }
    }
}
