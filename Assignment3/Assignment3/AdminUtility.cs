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
            Console.WriteLine("----------------------------------Welcome, Admin ---------------------------");
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
            if (command == "2")
            {
                TeacherInterface();
            } 
            if (command == "3")
            {
                StudentInterface();
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
            Console.WriteLine("1. Add New Course");
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
                EditCourse();
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
            Console.WriteLine("1.  Add New Teacher");
            Console.WriteLine("2.  Edit Teacher");
            Console.WriteLine("3.  Remove Teacher");
            Console.WriteLine("4.  View All Teacher");
            Console.WriteLine("5.  View A Teacher's Course");
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
                AddTeacher();
            }
            if (command == "2")
            {
                EditTeacher();
            }
            if (command == "3")
            {
                RemoveTeacher();
            }
            if (command == "4")
            {
                ViewAllTeachers();
            }
            if (command == "5")
            {
                ViewTeachersCourse();
            }

        }
        public void StudentInterface()
        {
            Console.Clear();
            Console.WriteLine("0. Main Menu");
            Console.WriteLine("1.  Add New Student");
            Console.WriteLine("2.  Edit A Student");
            Console.WriteLine("3.  Remove Student");
            Console.WriteLine("4.  View All Students");
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
                AddStudent();
            }
            if (command == "2")
            {
                EditStudent();
            }
            if (command == "3")
            {
                RemoveStudent();
            }
            if (command == "4")
            {
                ViewAllStudents();
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
        public void EditCourse()
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
            UpdateCourse(courseCode,courseName,fees, startDate, endDate);
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
        //--------------------  Teacher's Operations ------------------------------
        public void AddTeacher()
        {
            Console.Clear();
            Console.WriteLine("Add a New Teacher");
            string teacherName, password;
            Console.WriteLine("Name (Must be Unique) :");
            teacherName = Console.ReadLine();
            Console.WriteLine("Password : ");
            password = Console.ReadLine();
            if(teacherName!=null && password!= null)
            {
                CreateTeacher(teacherName, password);
            }
        }
        public void EditTeacher()
        {
            Console.Clear();
            Console.WriteLine("Edit a Teacher");
            string teacherName, newTeacherName, newPassword;
            Console.WriteLine("Enter Empty if you don't want to change a field");
            Console.WriteLine("Current User Name :");
            teacherName = Console.ReadLine();
            Console.WriteLine("New User Name :");
            newTeacherName = Console.ReadLine();
            Console.WriteLine("New Password :");
            newPassword = Console.ReadLine();
            UpdateTeacher(teacherName, newTeacherName, newPassword); 
        }
        public void RemoveTeacher()
        {
            Console.Clear();
            Console.WriteLine("Enter Teacher's User Name To Remove");
            string teacherName = Console.ReadLine();
            if (teacherName!=null)
            {
                DeleteTeacher(teacherName);
            }
        }
        public void ViewAllTeachers()
        {
            Console.Clear();
            List<User> teachers = GetAllTeachers();
            if(teachers.Count>0)
            {
                Console.WriteLine("Teachers Lists :");
                foreach (var teacher in teachers)
                {
                    Console.WriteLine($"Teacher Name = {teacher.Name}\tPassword={teacher.Password}");
                }
            }
            else
            {
                Console.WriteLine("There are no teachers");
            }
            HasGetBack("ti");
        }
        public void ViewTeachersCourse()
        {

        }
        // ---------------------- Teacher's Operation End ------------------------

        // ----------------------- Student's Operation ---------------------------
        public void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("Add a New Student");
            string studentName, password;
            Console.WriteLine("Name (Must be Unique) :");
            studentName = Console.ReadLine();
            Console.WriteLine("Password : ");
            password = Console.ReadLine();
            if (studentName != null && password != null)
            {
                CreateStudent(studentName, password);
            }
        }
        public void EditStudent()
        {
            Console.Clear();
            Console.WriteLine("Edit a Student");
            string studentName, newstudentName, newPassword;
            Console.WriteLine("Enter Empty if you don't want to change a field");
            Console.WriteLine("Current User Name of Student:");
            studentName = Console.ReadLine();
            Console.WriteLine("New User Name of Student:");
            newstudentName = Console.ReadLine();
            Console.WriteLine("New Password :");
            newPassword = Console.ReadLine();
            UpdateStudent(studentName,newstudentName, newPassword);
        }
        public void RemoveStudent()
        {
            Console.Clear();
            Console.WriteLine("Remove a student");
            Console.WriteLine("Enter Student Name to Remove");
            string studentName = Console.ReadLine();
            if(studentName is null)
            {
                Console.WriteLine("Enter Name");
            }
            else
            {
                DeleteStudent(studentName);
            }
        }
        public void ViewAllStudents()
        {
            Console.Clear();
            List<User> students = GetAllStudents();
            int i = 1;
            Console.WriteLine("Student Lists :");
            foreach (var student in students)
            {
                Console.WriteLine($"{i++}.Student Name = {student.Name}\tPassword={student.Password}");
            }
            HasGetBack("si");
        }
        // ---------------------- Student's Operation End ------------------------

        //-------------------- User(Admin) Console Side's Operation End--------------------------

        //------------------------ DB Operations----------------------------------

        //------------------------------Course DB Operations ----------------------------------

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
        public void UpdateCourse(string courseCode, string newCourseName, double newFees, string newStartDate, string newEndDate)
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
        //---------------------------- Course's DB Operations End ---------------------
        //----------------------------- Teacher's DB Operations -----------------------
        public void CreateTeacher(string userName, string password)
        {
            List<User> users = GetAllUsers();
            if(IsUniqueUserName(users, userName))
            {
                context.Users.Add(new User { Name = userName, Password = password, Type = "Teacher" });
                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine($"{userName} was successfully added as a teacher . . .");
                    GetAllTeachers();
                }
                else
                {
                    ErrorMsg();
                }
            }
            else
            {
                Console.WriteLine("Sorry this User Name has already been taken try any other one.");
            }
            HasGetBack("ti");
        }
        public void UpdateTeacher(string teacherName, string newTeacherName,string newPassword)
        {
            User teacher = context.Users.Where(u => u.Name.Equals(teacherName)).FirstOrDefault();
            if(teacher!=null)
            {
                if (newTeacherName != "")
                {
                    teacher.Name = newTeacherName;
                }
                if (newPassword != "")
                {
                    teacher.Password = newPassword;
                }
                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine($"{teacherName} was successfully renamed as a {newTeacherName} . . .");
                    ViewAllTeachers();
                }
                else
                {
                    ErrorMsg();
                }
            }
            else
            {
                Console.WriteLine("Teacher not found");
            }
           HasGetBack("ti");
        }
        public List<User> GetAllTeachers()
        {
            List<User> teachers = new List<User>(200);
            teachers = context.Users.Where(s => s.Type == "Teacher").ToList();
            return teachers;
        }
        public void DeleteTeacher(string uname)
        {
            User user = context.Users.Where(u => u.Name == uname).FirstOrDefault();
            if(user!= null)
            {
                context.Users.Remove(user);
                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine("Teacher Deleted Successfully");
                }
                else
                    ErrorMsg();
            }
            else
            {
                Console.WriteLine("Teacher not found");
            }
            HasGetBack("ti");
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
        //----------------------------- Teacher's DB Operations End -----------------------


        // -------------- Student's DB Operations --------------- 
        // Create A Student 
        public void CreateStudent(string studentName, string password)
        {
            List<User> users = GetAllUsers();
            if (IsUniqueUserName(users, studentName))
            {
                context.Users.Add(new User { Name = studentName, Password = password, Type = "Student" });
                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine($"{studentName} was successfully added as a student . . .");
                    ViewAllStudents();
                }
                else
                {
                    ErrorMsg();
                }
            }
            else
            {
                Console.WriteLine("Sorry this User Name has already been taken try any other one.");
            }
            HasGetBack("si");
        }
        public void UpdateStudent(string studentName, string newStudentName, string newPassword)
        {
            User student = context.Users.Where(u => u.Name.Equals(studentName)).FirstOrDefault();
            if (student != null)
            {
                if (newStudentName != "")
                {
                    student.Name = newStudentName;
                }
                if (newPassword != "")
                {
                    student.Password = newPassword;
                }
                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine($"{studentName} was successfully renamed as a {newStudentName} . . .");
                    ViewAllStudents();
                }
                else
                {
                    ErrorMsg();
                }
            }
            else
            {
                Console.WriteLine("Student not found");
            }
            HasGetBack("si");
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
        // Delete A Student
        public void DeleteStudent(string uname)
        {
            User student = context.Users.Where(u => u.Name == uname).FirstOrDefault();
            if(student!= null)
            {
                context.Users.Remove(student);
                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine("Student Deleted Successfully");
                }
                else
                {
                    ErrorMsg();
                }
            }
            else
            {
                Console.WriteLine("Sorry student was not found.");
            }
            HasGetBack("si");
        }
        public List<User> GetAllStudents()
        {
            List<User> students = new List<User>(200);
            students = context.Users.Where(s => s.Type == "Student").ToList();
            return students;
        }
        //------------------------------Student DB Operations End ------------------------

        //----------------------------- User DB Operations ------------------------
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
        
        public List<User> GetAllUsers()
        {
            //Console.WriteLine("Users Lists :");
            List<User> users = new List<User>(300);
            users = context.Users.ToList();
            //foreach (var user in Users)
            //{
            //    Console.WriteLine($"Name = {user.Name}\t Password = {user.Password}\t Type = {user.Type}");
            //}
            return users;
        }
        //----------------------------- User DB Operations End ------------------------

        // ---------------------------------- Course Schedule DB Operations--------------------
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
        // ---------------------------------- Course Schedule DB Operations End --------------------




        //--------------------------- Utility Function Built By Me Customizely----------------------
        public void LogOut()
        {
            UserUtility user = new UserUtility();
            user.Logout();
        }
        public void ErrorMsg() => Console.WriteLine("Sorry an error occured");
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
        public bool IsUniqueUserName(List<User> users, string userName)
        {
            bool unique = true;
            foreach (var user in users)
            {
                if (user.Name.Equals(userName))
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
        //--------------------------- Utility Function Built By Me Customizely----------------------

        // ----------------------------- END -------------------------------------
    }
}
