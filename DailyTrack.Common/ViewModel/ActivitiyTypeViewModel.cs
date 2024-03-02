using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyTrack.Common.ViewModel
{
    public enum ActivitiyTypeViewModel
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
        public static string ToFriendlyString(this ActivitiyTypeViewModel type)
        {
            switch (type)
            {
                case ActivitiyTypeViewModel.Development:
                    return "Development";
                case ActivitiyTypeViewModel.Meeting:
                    return "Meeting";
                case ActivitiyTypeViewModel.Analysis:
                    return "Analysis";
                case ActivitiyTypeViewModel.RemoteSupport:
                    return "Remote Support";
                case ActivitiyTypeViewModel.Ticket:
                    return "Ticket";
                case ActivitiyTypeViewModel.VmAppCreation:
                    return "Vm/App Creation";
                case ActivitiyTypeViewModel.Other:
                    return "Other";
                default:
                    return "Unknown";
            }
        }        

        public static int ToInt(this ActivitiyTypeViewModel type)
        {
            return (int)type;
        }
    }
}
