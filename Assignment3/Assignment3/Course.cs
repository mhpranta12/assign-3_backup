using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public double Fees { get; set; }
        public List<CourseSchedule> CourseSchedules { get; set; }
        public CourseTeacher courseTeacher { get; set; }
        public List<CourseStudent> CourseStudents { get; set; }
    }
}
