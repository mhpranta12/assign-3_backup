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
        public string Day { get; set; }
        public DateTime Start_time { get; set; }
        public DateTime End_time { get; set; }
        public CourseSchedule SchedulesCourse { get; set; }
    }
}
