using DailyTrack.Common.ViewModel;
using DailyTrack.Domain;

namespace DailyTrack.ViewModel
{
    public static class TaskStatusViewModelExtensions
    {
        public static ActivitiyStatusViewModel ToTaskStatusViewModel(this Domain.ActivityStatus status)
        {
            switch (status)
            {
                case Domain.ActivityStatus.NotStarted:
                    return ActivitiyStatusViewModel.NotStarted;
                case Domain.ActivityStatus.InProgress:
                    return ActivitiyStatusViewModel.InProgress;
                case Domain.ActivityStatus.Stopped:
                    return ActivitiyStatusViewModel.Stopped;
                default:
                    return ActivitiyStatusViewModel.NotStarted;
            }
        }

        public static Domain.ActivityStatus ToDomainTaskStatus(this ActivitiyStatusViewModel status)
        {
            switch (status)
            {
                case ActivitiyStatusViewModel.NotStarted:
                    return Domain.ActivityStatus.NotStarted;
                case ActivitiyStatusViewModel.InProgress:
                    return Domain.ActivityStatus.InProgress;
                case ActivitiyStatusViewModel.Stopped:
                    return Domain.ActivityStatus.Stopped;
                default:
                    return Domain.ActivityStatus.NotStarted;
            }
        }
    }
}
