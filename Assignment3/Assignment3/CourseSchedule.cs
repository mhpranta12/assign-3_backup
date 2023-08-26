using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class CourseSchedule
    {
        public int CourseId { get; set; }
        public int ScheduleId { get; set; }
        public Course course { get; set; }
        public Schedule schedule { get; set; }
    }
}
