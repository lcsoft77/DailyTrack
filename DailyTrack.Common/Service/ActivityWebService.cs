using DailyTrack.Common.Interface;
using DailyTrack.Common.ViewModel;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DailyTrack.Common.Service
{
    public class ActivityWebService : IActivityWebService
    {
        private readonly HttpClient _httpClient;

        public ActivityWebService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddActivity(ActivitiyNewViewModel activity)
        {

            await _httpClient.PostAsJsonAsync("/api/activity", activity);
        }

        public async Task<IReadOnlyList<ActivityViewModel>> GetActivitiesForDate(DateTime startDate, DateTime endDate)
        {           
            return await _httpClient.GetFromJsonAsync<List<ActivityViewModel>>($"/api/activities?startDate={startDate.ToString("yyyy-MM-dd")}&endDate={endDate.ToString("yyyy-MM-dd")}");
        }

        public Task<ActivityViewModel> GetActivityById(Guid id)
        {
            return _httpClient.GetFromJsonAsync<ActivityViewModel>($"/api/activity/{id}");
        }

        public Task UpdateActivity(ActivityViewModel activityViewModel)
        {
            return _httpClient.PutAsJsonAsync($"/api/activity/{activityViewModel.id}", activityViewModel);
        }

        public Task DeleteActivity(Guid id)
        {
            return _httpClient.DeleteAsync($"/api/activity/{id}");
        }

        public Task StartActivity(Guid id)
        {
            return _httpClient.GetAsync($"/api/start/{id}");
        }

        public Task StopActivity(Guid id)
        {
            return _httpClient.GetAsync($"/api/stop/{id}");
        }
    }
}
