using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class CourseAttendance
    {
        public int CourseId { get; set; }
        public int AttendanceId { get; set; }
        public Course course { get; set; }
        public Attendance attendance { get; set; }

    }
}
