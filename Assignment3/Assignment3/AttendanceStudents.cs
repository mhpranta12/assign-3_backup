using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class AttendanceStudents
    {
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public Attendance attendance { get; set; }
        public User Students { get; set; }
    }
}
