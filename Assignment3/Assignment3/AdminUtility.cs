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
        // Interfaces
        public void Interface()
        {
            Console.Clear();
            Console.WriteLine("1.  Course");
            Console.WriteLine("2.  Teacher");
            Console.WriteLine("3.  Student");
            Console.WriteLine("4.  Log Out");
            Action();
        }
        public void Action()
        {
            string command = Console.ReadLine();
            if (command == "0")
            {
                Interface();
            }
            if (command == "1")
            {
                CourseInterface();
            }
            if (command == "4")
            {
                LogOut();
            }
        }
        public void CourseInterface()
        {
            Console.Clear();
            Console.WriteLine("0. Main Menu");
            Console.WriteLine("1. Create Course");
            Console.WriteLine("2. Edit a Course");
            Console.WriteLine("3. Remove a Course");
            Console.WriteLine("4. View All Courses");
            Console.WriteLine("5. View All Student of a Course");
            CourseAction();
        }
        public void CourseAction()
        {
            string command = Console.ReadLine();
            if (command == "0")
            {
                Interface();
            }
            if (command == "1")
            {
                AddCourse();
            }
            if (command == "2")
            {
                AlterCourse();
            }
            if (command == "3")
            {
                RemoveCourse();
            }
            if (command == "4")
            {
                ViewAllCourses();
            }
            if (command == "5")
            {
                ViewAllStudentOfCourse();
            }
        }
        public void TeacherInterface()
        {
            Console.Clear();
            Console.WriteLine("0. Main Menu");
            Console.WriteLine("1.  Course");
            Console.WriteLine("2.  Teacher");
            Console.WriteLine("3.  Student");
            Console.WriteLine("4.  Student");
            TeacherAction();
        }
        public void TeacherAction()
        {
            string command = Console.ReadLine();
            if (command == "0")
            {
                Interface();
            }
            if (command == "1")
            {
                CourseInterface();
            }
        }
        public void StudentInterface()
        {
            Console.Clear();
            Console.WriteLine("Press <- (backspace) to go back ");
            Console.WriteLine("1.  Course");
            Console.WriteLine("2.  Teacher");
            Console.WriteLine("3.  Student");
            Console.WriteLine("4.  Student");
            StudentAction();
        }
        public void StudentAction()
        {
            string command = Console.ReadLine();
            if (command == "0")
            {
                Interface();
            }
            if (command == "1")
            {
                CourseInterface();
            }
        }

        //-------------------- User Side's Operation --------------------------
        //-------------------- Course's Operations ----------------------------
        public void AddCourse()
        {
            Console.Clear();
            Console.WriteLine("Add a new Course");
            string courseName,courseCode,startDate,endDate;
            Console.WriteLine("Name :");
            courseName = Console.ReadLine();
            Console.WriteLine("Course Code (Must be Unique) :");
            courseCode = Console.ReadLine();
            Console.WriteLine("Fees (৳) :");
            double fees = double.Parse(Console.ReadLine());
            Console.WriteLine("Start Date (day/month/year) :");
            startDate = Console.ReadLine();
            Console.WriteLine("End Date (day/month/year) :");
            endDate = Console.ReadLine();
            CreateCourse(courseName,courseCode,fees,startDate,endDate);
            HasGetBack("ci");
        }
        public void AlterCourse()
        {
            Console.Clear();
            Console.WriteLine("Edit a Course");
            string courseName, courseCode, startDate, endDate;
            Console.WriteLine("Course Code to Edit:");
            courseCode = Console.ReadLine();
            Console.WriteLine("Enter Empty if you don't want to change a field");
            Console.WriteLine("Name :");
            courseName = Console.ReadLine();
            Console.WriteLine("Fees (৳):");
            double fees = double.Parse(Console.ReadLine());
            Console.WriteLine("Start Date (day/month/year):");
            startDate = Console.ReadLine();
            Console.WriteLine("End Date (day/month/year) :");
            endDate = Console.ReadLine();
            EditCourse(courseCode,courseName,fees, startDate, endDate);
            HasGetBack("ci");
        }
        public void RemoveCourse()
        {
            string courseCode;
            Console.WriteLine("Enter Course Code to delete  :");
            courseCode = Console.ReadLine();
            DeleteCourse(courseCode);
        }
        public void ViewAllCourses()
        {
            Console.Clear();
            List<Course> courses = GetAllCourses();
            if (courses.Count > 0)
            {
                Console.WriteLine("Courses Lists :");
                int i = 1;
                foreach (var course in courses)
                {
                    Console.WriteLine(i+".");
                    if (course.courseTeacher != null)
                    {
                        Console.WriteLine($"Course Code = {course.CourseCode}\tCourse Name = {course.Name}\tFees = {course.Fees} tk.\nCourse Teacher = {course.courseTeacher.teacher.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Course Code = {course.CourseCode}\tCourse Name = {course.Name}\tFees = {course.Fees}\nCourse Teacher = No teacher assigned ");
                    }
                    //Displaying Schedules of a Course
                    Console.WriteLine("Schedules :");
                        if (course.CourseSchedules.Count > 0)
                        {
                            Console.WriteLine("\t Schedules");

                            foreach (var courseSchedule in course.CourseSchedules)
                            {
                                Console.WriteLine($"\t {courseSchedule.schedule.Day} \t {courseSchedule.schedule.Start_time.ToString("h:m tt")} - {courseSchedule.schedule.End_time.ToString("h:m tt")}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Schedule was assigned for this course");
                        }
                    Console.WriteLine();
                    i++;
                }
            }
            else
            {
                Console.WriteLine("No Courses are there");
            }
            HasGetBack("ci");
        }
        public void ViewAllStudentOfCourse()
        {
            Console.Clear();
            Console.WriteLine("Enter Course Code to View All Students :");
            string courseCode = Console.ReadLine();
            GetAllStudentOfCourse(courseCode);
        }
        //-------------------- Course's Operations End ----------------------------
        //-------------------- User Side's Operation End--------------------------

        //------------------------ DB Operations----------------------------------

        //------------------------------Course----------------------------------

        // Create a Course
        public void CreateCourse(string courseName, string courseCode, double fees, string startDate, string endDate)
        {
            bool uniqueCode = true;
            List<Course> courses = GetAllCourses();
            if(courses.Count>0)
            {
               uniqueCode = IsUniqueCode(courses, courseCode);
            }
            if (uniqueCode)
            {
                DateTime startdate = GetDate(startDate);
                DateTime enddate = GetDate(endDate);
                context.Courses.Add(new Course { Name = courseName, CourseCode = courseCode, Fees = 2000, StartDate = startdate, EndDate = enddate });
                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine($"Course : {courseCode} was successfully added . . .");
                    ViewAllCourses();
                }
                else
                    ErrorMsg();
            }
            else
                Console.WriteLine("This code has already been taken please choose another one");
        }
        
        //Edit a Course
        public void EditCourse(string courseCode, string newCourseName, double newFees, string newStartDate, string newEndDate)
        {
            Course course = context.Courses.Where(c => c.CourseCode.Equals(courseCode)).FirstOrDefault();
            if(newCourseName!="")
            {
                course.Name = newCourseName;
            }
            if(newFees != null)
            {
                course.Fees = newFees;
            }
            if(newStartDate != "")
            {
                DateTime startdate = GetDate(newStartDate);
                course.StartDate = startdate;
            }
            if(newEndDate != "")
            {
                DateTime enddate = GetDate(newEndDate);
                course.EndDate = enddate;
            }
            
            if (context.SaveChanges() > 0)
            {
                Console.WriteLine($"{courseCode} was successfully edited as  {newCourseName} . . .");
                ViewAllCourses();
            }
            else
                Console.WriteLine("No changes were made");
        }
        public void DeleteCourse(string courseCode)
        {
            Course course = context.Courses.Where(u => u.CourseCode == courseCode).FirstOrDefault();
            if(course!=null)
            {
                context.Courses.Remove(course);
                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine("Deleted Successfully");
                }
                else
                {
                    ErrorMsg();
                }
            }
            else
            {
                Console.WriteLine("Code not found");
            }
            
            HasGetBack("ci");
        }
        //Get All Courses
        public List<Course> GetAllCourses()
        {
            List<Course> Courses = new List<Course>(200);
            Courses = context.Courses
                .Include(c => c.courseTeacher)
                .ThenInclude(c => c.teacher)
                .Include(c => c.CourseSchedules)
                .ThenInclude(c => c.schedule)
                .ToList();
            return Courses;
        }
        public void GetAllStudentOfCourse(string course_code)
        {
            
            Course course = context.Courses.Where(c => c.CourseCode == course_code)
                .Include(cs => cs.CourseStudents)
                .ThenInclude(s => s.Student)
                .FirstOrDefault();

            if(course!=null)
            {
                if (course.CourseStudents.Count > 0)
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
            else
            {
                Console.WriteLine("No course found with this code");
            }
            HasGetBack("ci");
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
        //---------------Course End -----------

        // -------------- Student--------------- 
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
        public void EditUser(string uName, string newName, string newPassword)
        {
            User user = context.Users.Where(u=>u.Name.Equals(uName)).FirstOrDefault();
            user.Name = newName;
            user.Password = newPassword;
            if (context.SaveChanges() > 0)
            {
                Console.WriteLine($"{uName} was successfully renamed as a {newName} . . .");
                GetAllUsers();
            }
            else
                ErrorMsg();
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

        

        //Schedules
        //Create  Schedules
        
        public void CreateSchedule(string schedule_code, string day, string start_time, string end_time)
        {
            DateTime starttime = GetTime(start_time);
            DateTime endtime = GetTime(end_time);
            context.Schedules.Add(new Schedule { SCode = schedule_code, Day = day, Start_time = starttime, End_time = endtime });
            if (context.SaveChanges() > 0)
            {
                Console.WriteLine($"{day} was added with code {schedule_code}");
            }
        }
        public void AssignSchedule(string course_code, params string[] schedule_code)
        {
            Course course = context.Courses.Where(c => c.CourseCode.Equals(course_code))
                .Include(sc => sc.CourseSchedules)
                .ThenInclude(s => s.schedule)
                .FirstOrDefault();
            List<CourseSchedule> courseSchedules = new List<CourseSchedule>(100);
            foreach (var item in schedule_code)
            {
                Schedule found_schedule = context.Schedules.Where(s => s.SCode == item)
                    .FirstOrDefault();
                CourseSchedule courseSchedule = (new CourseSchedule { schedule = found_schedule});
                courseSchedules.Add(courseSchedule);
            }
            course.CourseSchedules = courseSchedules;
            if (context.SaveChanges() > 0)
            {
                Console.WriteLine("Schedules were assigned");
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
        public void AssignSchedule(string course_code, string schedule_code)
        {
            Course course = context.Courses.Where(c => c.CourseCode == course_code)
                .Include(sc => sc.CourseSchedules)
                .ThenInclude(s => s.schedule)
                .FirstOrDefault();
            Schedule found_schedule = context.Schedules.Where(s => s.SCode == schedule_code)
                .FirstOrDefault();
            List<CourseSchedule> courseschedules = new List<CourseSchedule>()
            {
                new CourseSchedule { schedule=found_schedule }
            };
            course.CourseSchedules = courseschedules;
            if (context.SaveChanges() > 0)
            {
                Console.WriteLine($"Schedule {schedule_code} was added in {course.Name} course");
            }
        }

        //Utilities
        public void LogOut()
        {
            UserUtility user = new UserUtility();
            user.Logout();
        }
        public void ErrorMsg() => Console.WriteLine("Sorry an error occured");

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
        public bool IsUniqueCode(List<Course> courses, string courseCode)
        {
            bool unique = true;
            foreach (var course in courses)
            {
                if (course.CourseCode.Equals(courseCode))
                {
                    unique = false;
                    break;
                }
            }
            return unique;
        }
        public bool KeyOperation()
        {
            if (Console.ReadKey().Key == ConsoleKey.Backspace)
            {
                return true;
            }
            return false;
        }
        public void HasGetBack(string code)
        {
            Console.WriteLine("Press <- (backspace) to go back ");
            bool keyPressed = KeyOperation();
            if (keyPressed)
            {
                if(code=="i")
                {
                    Interface();
                }
                if (code == "ci")
                {
                    CourseInterface();
                }
                if (code == "ti")
                {
                    TeacherInterface();
                }
                if (code == "si")
                {
                    StudentInterface();
                }

            }
        }
    }
}
