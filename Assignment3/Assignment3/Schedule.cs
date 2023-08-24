using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class Schedule
    {
        public int Id { get; set; }
        public string SCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<ScheduleDaytime> SchedulesDayTimes { get; set; }
        public CourseSchedule SchedulesCourse { get; set; }
    }
}
