using DailyTrack.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyTrack.Common.Interface
{
    public interface IActivityWebService
    {
        Task AddActivity(ActivitiyNewViewModel activity);
        Task UpdateActivity(ActivityViewModel activityViewModel);
        Task<IReadOnlyList<ActivityViewModel>> GetActivitiesForDate(DateTime startDate, DateTime endDate);
        Task<ActivityViewModel> GetActivityById(Guid id);
        Task DeleteActivity(Guid id);
        Task StartActivity(Guid id);
        Task StopActivity(Guid id);
    }
}
