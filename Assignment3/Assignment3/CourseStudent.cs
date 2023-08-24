using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class CourseStudent
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public Course course { get; set; }
        public User Student { get; set; }
    }
}
