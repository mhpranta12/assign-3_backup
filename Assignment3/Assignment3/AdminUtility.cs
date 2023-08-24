using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class AdminUtility
    {
        EDBContext context;
        public AdminUtility()
        {
            context = new EDBContext();
        }
        public void Interface()
        {
            Console.WriteLine("1. Create Course");
            Console.WriteLine("2. Create Teacher");
            Console.WriteLine("3. Create Student");
        }
        // Create A Student 
        public void CreateStudent(string uname, string password)
        {
            context.Users.Add(new User { Name = uname, Password = password, Type = "Student" });
            if (context.SaveChanges() > 0)
            {
                Console.WriteLine($"{uname} was successfully added as a student . . .");
                GetAllStudents();
            }
        }
        // Enroll Student in a course
        public void EnrollStudent(string sname, string course_code)
        {
            User u = context.Users.Where(x => x.Name == sname).FirstOrDefault();
            Course course = context.Courses.Where(x => x.CourseCode == course_code).FirstOrDefault();
            List<CourseStudent> coursestudents = new List<CourseStudent>();
            CourseStudent student = new CourseStudent();
            student.Student = u;
            coursestudents.Add(student);
            course.CourseStudents = coursestudents;
            if (context.SaveChanges() > 0)
            {
                Console.WriteLine($"{sname} , was enrolled in {course.Name}");
            }
        }

        // Remove A Student
        public void RemoveStudent(string uname)
        {
            User user = context.Users.Where(u => u.Name == uname).FirstOrDefault();
            context.Users.Remove(user);
            if (context.SaveChanges() > 0)
            {
                Console.WriteLine("Deleted Successfully");
            }
        }
        public void CreateTeacher(string uname, string password)
        {
            context.Users.Add(new User { Name = uname, Password = password, Type = "Teacher" });
            if (context.SaveChanges() > 0)
            {
                Console.WriteLine($"{uname} was successfully added as a teacher . . .");
                GetAllTeachers();
            }
        }
        public void AssignTeacher(string teacher_name, string course_code)
        {
            User teacher = context.Users.Where(x => x.Name == teacher_name).FirstOrDefault();
            Course course = context.Courses.Where(x => x.CourseCode == course_code).FirstOrDefault();
            CourseTeacher courseTeacher = new CourseTeacher();
            courseTeacher.teacher = teacher;
            course.courseTeacher = courseTeacher;
            if (context.SaveChanges() > 0)
            {
                Console.WriteLine($"{teacher_name} , was assigned in {course.Name}");
                GetAllCourses();
            }
        }
        public void GetAllUsers()
        {
            Console.WriteLine("Users Lists :");
            List<User> Users = new List<User>(200);
            Users = context.Users.ToList();
            foreach (var user in Users)
            {
                Console.WriteLine($"Name = {user.Name}\t Password = {user.Password}\t Type = {user.Type}");
            }
        }
        public void GetAllStudents()
        {
            Console.WriteLine("Student Lists :");
            List<User> Students = new List<User>(200);
            Students = context.Users.Where(s => s.Type == "Student").ToList();
            int i = 1;
            foreach (var student in Students)
            {
                Console.WriteLine($"{i++}.Student Name = {student.Name}\tPassword={student.Password}");
            }
        }
        public void GetAllTeachers()
        {
            Console.WriteLine("Teachers Lists :");
            List<User> Teachers = new List<User>(200);
            Teachers = context.Users.Where(s => s.Type == "Teacher").ToList();
            foreach (var teacher in Teachers)
            {
                Console.WriteLine($"Teacher Name = {teacher.Name}\tPassword={teacher.Password}");
            }
        }

        //Course

        // Create a Course
        public void CreateCourse(string course_name,string course_code,double fees )
        {
            context.Courses.Add(new Course { Name = course_name,CourseCode = course_code, Fees = 2000 });
            if (context.SaveChanges() > 0)
            {
                Console.WriteLine($"Course : {course_code} was successfully added . . .");
                GetAllCourses();
            }
        }
        //Get All Courses
        public void GetAllCourses()
        {
            Console.WriteLine("Teachers Lists :");
            List<Course> Courses = new List<Course>(200);
            Courses = context.Courses.ToList();
            foreach (var course in Courses)
            {
                if(course.courseTeacher.teacher != null)
                {
                    Console.WriteLine($"Course Code = {course.CourseCode}\tCourse Name = {course.Name}\tFees = {course.Fees}\nCourse Teacher = {course.courseTeacher.teacher.Name}");
                }
                else
                {
                    Console.WriteLine($"Course Code = {course.CourseCode}\tCourse Name = {course.Name}\tFees = {course.Fees}\nCourse Teacher = No teacher assigned ");
                }
            }
        }
        public void GetAllStudentOfCourse(string course_code)
        {
            Course course = context.Courses.Where(c => c.CourseCode == course_code)
                .Include(cs => cs.CourseStudents)
                .ThenInclude(s=>s.Student)
                .FirstOrDefault();
            if(course.CourseStudents.Count>0)
            {
                Console.WriteLine($"Students of {course.Name}");
                var CS = course.CourseStudents;
                foreach (var coursestudent in CS)
                {
                    Console.WriteLine($" Student Name = {coursestudent.Student.Name}");
                }
            }
            else
                Console.WriteLine("No student enrolled in this course");
        }
        public void GetAllTeachersCourse()
        {
            Console.WriteLine("Teachers Course Lists :");
            List<User> Teachers = new List<User>(200);
            var TeachersWithCourse = context.Users.Where(s => s.Type == "Teacher")
                .Include(tc => tc.TeachersCourse)
                .ThenInclude(c => c.course).ToList();
            foreach (var teacher in TeachersWithCourse)
            {
                Console.WriteLine($"Teacher Name = {teacher.Name}\t Course = {teacher.TeachersCourse.course.Name}\t Course Code = {teacher.TeachersCourse.course.CourseCode}");
            }
        }

        //Schedules
        //Day Time
        //Create a Day Time 
        public void AddDayTime(string dtcode, string day, string start_time, string end_time)
        {
            DateTime starttime = GetTime(start_time);
            DateTime endtime = GetTime(end_time);
            context.DayTimes.Add(new DayTime { DTCode=dtcode, Day=day, Start_time=starttime, End_time= endtime});
            if(context.SaveChanges()>0)
            {
                Console.WriteLine($"{day} was added with code {dtcode}");
            }
        }
        public DateTime GetTime(string time)
        {
                string[] splitted_string = time.Split(' ');
                string[] ttime = splitted_string[0].Split(":");
                int hour = int.Parse(ttime[0]);
                int minutes = int.Parse(ttime[1]);
                if (time.Contains("PM"))
                {
                    hour += 12;
                }
                DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minutes, 0);
                return dateTime;
        }
        public void CreateDayTime(string dtcode,string day,DateTime start_time,DateTime end_time)
        {

        }
        public void CreateSchedule(string schedule_code, string start_date, string end_date)
        {
            DateTime startdate = GetDate(start_date);
            DateTime enddate = GetDate(end_date);
            context.Add(new Schedule { SCode = schedule_code, StartDate =startdate, EndDate = enddate });
            if (context.SaveChanges() > 0)
            {
                Console.WriteLine($"Schedule was added with code {schedule_code}");
            }
        }
        public void AssignDayTime(string schedule_code,params string[] daytime_code)
        {
            Schedule schedule = context.Schedules.Where(s=>s.SCode.Equals(schedule_code))
                .Include(s=>s.SchedulesDayTimes)
                .ThenInclude(s=>s.dayTime)
                .FirstOrDefault();
            Console.WriteLine(schedule.StartDate);
            List<ScheduleDaytime> schedulesDayTimes = new List<ScheduleDaytime>(100);
            foreach (var item in daytime_code)
            {
                DayTime dt = context.DayTimes.Where(dt => dt.DTCode == item)
                    .FirstOrDefault();
                ScheduleDaytime schedulesdaytime = (new ScheduleDaytime { dayTime = dt });
                schedulesDayTimes.Add(schedulesdaytime);
            }
            schedule.SchedulesDayTimes = schedulesDayTimes;
            if (context.SaveChanges() > 0)
            {
                Console.WriteLine("Day time was assigned");
            }
        }
        public DateTime GetDate(string date)
        {
            string[] splitted_text = date.Split('/');
            int year, month, day;
            day = int.Parse(splitted_text[0]);
            month = int.Parse(splitted_text[1]);
            if (splitted_text[2].Length < 3)
            {
                string corrected_year = "20" + splitted_text[2];
                year = int.Parse(corrected_year);
            }
            else
            {
                year = int.Parse(splitted_text[2]);
            }
            DateTime now = DateTime.Now;
            DateTime parseddateTime = new DateTime(year, month, day, now.Hour, now.Minute, now.Second);
            return parseddateTime;
        }
        public void AssignSchedule(string course_code,string schedule_code)
        {
            Course course = context.Courses.Where(c => c.CourseCode == course_code)
                .Include(sc => sc.CourseSchedules)
                .ThenInclude(s=>s.schedule)
                .FirstOrDefault();
            Schedule found_schedule = context.Schedules.Where(s => s.SCode == schedule_code)
                .FirstOrDefault();
            List<CourseSchedule> courseschedules = new List<CourseSchedule>()
            {
                new CourseSchedule { schedule=found_schedule }
            };
            course.CourseSchedules = courseschedules;
            if(context.SaveChanges()>0)
            {
                Console.WriteLine($"Schedule {schedule_code} was added in {course.Name} course");
            }
        }
    }
}
