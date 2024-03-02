using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyTrack.Domain
{
    public enum ActivitiyType
    {
        Development,
        Meeting,
        Analysis,
        RemoteSupport,
        Ticket,
        VmAppCreation,
        Other
    }

    public static class TaskTypeExtensions
    {
        public static string ToFriendlyString(this ActivitiyType type)
        {
            switch (type)
            {
                case ActivitiyType.Development:
                    return "Development";
                case ActivitiyType.Meeting:
                    return "Meeting";
                case ActivitiyType.Analysis:
                    return "Analysis";
                case ActivitiyType.RemoteSupport:
                    return "Remote Support";
                case ActivitiyType.Ticket:
                    return "Ticket";
                case ActivitiyType.VmAppCreation:
                    return "Vm/App Creation";
                case ActivitiyType.Other:
                    return "Other";
                default:
                    return "Unknown";
            }
        }
    }
}
