using EventScheduler.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventScheduler.Business.Abstracts
{
    public interface IScheduleOptimizer
    {
        List<Event> GetOptimalSchedule();
    }
}
