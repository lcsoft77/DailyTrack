namespace DailyTrack.ViewModel
{
    public class ActivityNewViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get;  set; }
        public DateTime? StartedAt { get;  set; }
        public TimeSpan Duration { get; set; }
    }
}
