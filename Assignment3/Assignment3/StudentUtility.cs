using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class StudentUtility
    {
        EDBContext context;
        User _student;
        public StudentUtility()
        {
            context = new EDBContext();
        }
        public void Interface(User user)
        {
            Console.Clear();
            Console.WriteLine($"Welcome {user.Name}");
            Console.WriteLine("1. Give Attendance");
            Console.WriteLine("2. Log Out");
            string a = Console.ReadLine();
            Action(a);
        }
        public void Action(string a)
        {
            if(a=="1")
            {
                GiveAttendance();
            }
            if (a == "2")
            {
                UserUtility user = new UserUtility();
                user.Logout();
            }
        }
        public void DisplayEnrolledCourses()
        {
            Console.WriteLine("You are enrolled in :");
            foreach (var studentcourse in _student.StudentsCourse)
            {
                int i = 0;
                Console.WriteLine($"{i} Course Name = {studentcourse.course.Name} Course Code = {studentcourse.course.CourseCode}");
                i++;
            }
        }
        public void GiveAttendance()
        {
            DisplayEnrolledCourses();
            Console.WriteLine("Enter Course Code to give attendance :");
            string course_code = Console.ReadLine();
            Course course = context.Courses.Where(c=>c.CourseCode== course_code).FirstOrDefault();
            PerformAttendance(course);
        }
        public void PerformAttendance(Course coursetoattend)
        {
            int id = _student.Id;
            AttendanceStudents attendingStudents = (new AttendanceStudents { Students = _student});
            context.Attendances.Add(new Attendance { student= _student,course=coursetoattend,TimeStamps=DateTime.Now,attendanceStudents=attendingStudents});
            if (context.SaveChanges()>0)
            {
                Console.WriteLine("attendance taken");
            }
        }
    }
}
