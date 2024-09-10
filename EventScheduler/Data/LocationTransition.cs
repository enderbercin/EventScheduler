using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventScheduler.Data
{
    public class LocationTransition
    {
        public string From { get; set; }
        public string To { get; set; }
        public int DurationMinutes { get; set; }
    }
}
