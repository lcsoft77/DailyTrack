using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyTrack.Common.ViewModel
{
    public class ActivitiyNewViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ActivitiyTypeViewModel Type { get; set; }
    }
}
