using DailyTrack.Common.ViewModel;

namespace DailyTrack.ViewModel
{
    public static class TaskTypeViewModelExtensions
    {
        public static ActivitiyTypeViewModel ToTaskTypeViewModel(this Domain.ActivitiyType taskType)
        {
            switch (taskType)
            {
                case Domain.ActivitiyType.Development:
                    return ActivitiyTypeViewModel.Development;
                case Domain.ActivitiyType.Meeting:
                    return ActivitiyTypeViewModel.Meeting;
                case Domain.ActivitiyType.Analysis:
                    return ActivitiyTypeViewModel.Analysis;
                case Domain.ActivitiyType.RemoteSupport:
                    return ActivitiyTypeViewModel.RemoteSupport;
                case Domain.ActivitiyType.Ticket:
                    return ActivitiyTypeViewModel.Ticket;
                case Domain.ActivitiyType.VmAppCreation:
                    return ActivitiyTypeViewModel.VmAppCreation;
                case Domain.ActivitiyType.Other:
                    return ActivitiyTypeViewModel.Other;
                default:
                    return ActivitiyTypeViewModel.Other;
            }            
        }

        public static Domain.ActivitiyType ToDomainTaskType(this ActivitiyTypeViewModel taskType)
        {
            switch (taskType)
            {
                case ActivitiyTypeViewModel.Development:
                    return Domain.ActivitiyType.Development;
                case ActivitiyTypeViewModel.Meeting:
                    return Domain.ActivitiyType.Meeting;
                case ActivitiyTypeViewModel.Analysis:
                    return Domain.ActivitiyType.Analysis;
                case ActivitiyTypeViewModel.RemoteSupport:
                    return Domain.ActivitiyType.RemoteSupport;
                case ActivitiyTypeViewModel.Ticket:
                    return Domain.ActivitiyType.Ticket;
                case ActivitiyTypeViewModel.VmAppCreation:
                    return Domain.ActivitiyType.VmAppCreation;
                case ActivitiyTypeViewModel.Other:
                    return Domain.ActivitiyType.Other;
                default:
                    return Domain.ActivitiyType.Other;
            }
        }
    }
}
