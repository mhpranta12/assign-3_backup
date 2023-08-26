using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public List<CourseStudent> StudentsCourse { get; set; }
        public List<AttendanceStudents> StudentsAttendance { get; set; }
        public CourseTeacher TeachersCourse { get; set; }
    }
}
