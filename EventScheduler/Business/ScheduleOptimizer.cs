using EventScheduler.Business.Abstracts;
using EventScheduler.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventScheduler.Business
{
    public class ScheduleOptimizer : IScheduleOptimizer
    {
        private readonly List<Event> _events;
        private readonly List<LocationTransition> _transitions;

        public ScheduleOptimizer(List<Event> events, List<LocationTransition> transitions)
        {
            _events = events;
            _transitions = transitions;
        }

        public List<Event> GetOptimalSchedule()
        {
            int n = _events.Count;
            int maxPriority = 0;
            List<Event> bestSchedule = new List<Event>();

            for (int mask = 0; mask < (1 << n); mask++)
            {
                var currentSchedule = new List<Event>();
                var lastEvent = default(Event);
                int currentPriority = 0;

                for (int i = 0; i < n; i++)
                {
                    if ((mask & (1 << i)) != 0)
                    {
                        var ev = _events[i];

                        if (lastEvent != null)
                        {
                            var transition = _transitions.FirstOrDefault(t => t.From == lastEvent.Location && t.To == ev.Location);
                            if (transition != null)
                            {
                                var transitionTime = TimeSpan.FromMinutes(transition.DurationMinutes);
                                if (ev.StartTime < lastEvent.EndTime + transitionTime)
                                {
                                    currentPriority = 0;
                                    currentSchedule.Clear();
                                    break;
                                }
                            }
                            else if (ev.StartTime < lastEvent.EndTime)
                            {
                                currentPriority = 0;
                                currentSchedule.Clear();
                                break;
                            }
                        }

                        currentSchedule.Add(ev);
                        currentPriority += ev.Priority;
                        lastEvent = ev;
                    }
                }

                if (currentPriority > maxPriority)
                {
                    maxPriority = currentPriority;
                    bestSchedule = new List<Event>(currentSchedule);
                }
            }

            return bestSchedule;
        }
    }
}
