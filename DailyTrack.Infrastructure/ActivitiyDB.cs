using CSharpFunctionalExtensions;
using DailyTrack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DailyTrack.Domain.Activity;

namespace DailyTrack.Infrastructure
{
    public class ActivitiyDB : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public Domain.ActivityStatus Status { get; set; }
        public ActivitiyType Type { get; set; }

        public List<TaskLog> Logs { get; set; } = new List<TaskLog>();
    }
}
