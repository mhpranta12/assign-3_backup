using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class CourseTeacher
    {
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public Course course { get; set; }
        public User teacher { get; set; }
    }
}
