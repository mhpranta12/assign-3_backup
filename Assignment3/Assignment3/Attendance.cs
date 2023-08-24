using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class Attendance
    {
        public int Id { get; set; }
        public User student { get; set; }
        public Course course { get; set; }
        public DateTime TimeStamps { get; set; }
        public AttendanceStudents attendanceStudents { get; set; }
        public CourseAttendance attendanceCourse { get; set; }
    }
}
