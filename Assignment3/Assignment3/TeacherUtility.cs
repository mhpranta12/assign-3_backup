using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class TeacherUtility
    {
        EDBContext context;
        private User _teacher;
        public TeacherUtility(User user)
        {
            context = new EDBContext();
            _teacher = user;
        }
        public void Interface()
        {
            Console.Clear();
            Console.WriteLine($"Welcome {_teacher.Name}");
            Console.WriteLine("1. View Attendance Report of a Student");
            Console.WriteLine("2. View Class Days");
            Console.WriteLine("3. View Attendance Report of this course");
            Console.WriteLine("4. Log Out");
            Action();
        }
        public void Action()
        {
            string command = Console.ReadLine();
            if (command == "1")
            {
                Console.WriteLine("Enter Student Name to view attendance report : ");
                string studentName = Console.ReadLine();
                CheckAttendanceReport(studentName);
            }
            if(command == "2")
            {
                CheckClassDays();
            } 
            if(command == "3")
            {
                Console.WriteLine("Not done yet");
            }
            if (command == "4")
            {
                UserUtility user = new UserUtility();
                user.Logout();
            }
        }
        public bool KeyOperation()
        {
            if (Console.ReadKey().Key == ConsoleKey.Backspace)
            {
                return true;
            }
            return false;
        }
        public void HasGetBack()
        {
            Console.WriteLine("Press <- (backspace) to go back ");
            bool keyPressed = KeyOperation();
            if (keyPressed)
            {
                Interface();
            }
        }
        public int GetTeachersCourseID()
        {
            User teacher = context.Users.Where(u => u.Id == _teacher.Id)
                .Include(tc => tc.TeachersCourse)
                .FirstOrDefault();
            return teacher.TeachersCourse.CourseId;
        }
         
        public void CheckAttendanceReport(string studentName)
        {
            Dictionary<string, string> attendanceSheet = GenerateReport(studentName);
            if(attendanceSheet.Count>0)
            {
                Console.WriteLine($"Attendance Report of {studentName}:");
                Console.WriteLine("\nAttended\t ^");
                Console.WriteLine("\nAbsent\t -");
                Console.WriteLine("\t\t\t\t Date \t Attendance");
                foreach (KeyValuePair<string, string> record in attendanceSheet)
                {
                    Console.WriteLine($"\t\t\t\t{record.Key} \t  {record.Value}");
                }
                HasGetBack();
            }
            else
                Console.WriteLine("Sorry this student didn't attend any class");
            HasGetBack();
        }
        public Dictionary<string,string> GenerateReport(string studentName)
        {
            //Getting the calender by generating through method
            List<string> calender = GetCalender();
            //Getting Blank Attendance sheet
            Dictionary<string, string> attendanceSheet = new Dictionary<string, string>();
            
            // Retreving records of attendance of that particular student through his Id
            int student_id = GetStudentId(studentName);
            List<Attendance> attendanceRecord = GetStudentRecord(student_id);

            //If he has minimal attendance 
            if(attendanceRecord.Count>0)
            {
                //then sending the records for processing through algorithm
                attendanceSheet = ProcessRecord(calender, attendanceSheet,attendanceRecord);
            }
            //Eventually returning the records in a Dictionary <string,string>
            //Means , Sheet <date,checkmark>
            return attendanceSheet;
        }
        public Dictionary<string, string> ProcessRecord(List<string> calender, Dictionary<string, string> attendanceSheet, List<Attendance> attendanceRecord)
        {
            int i = 0, j = 0;
            while (j < calender.Count)
            {
                if (i > attendanceRecord.Count - 1)
                {
                    attendanceSheet.Add(calender[j], "-");
                }
                else
                {
                    DateTime date = attendanceRecord[i].TimeStamps.Date;
                    string attendanceDate = date.ToString("d-MMM-yy");
                    if (calender[j] == attendanceDate)
                    {
                        attendanceSheet.Add(attendanceDate, "^");
                        i++;
                    }
                    if (calender[j] != attendanceDate)
                    {
                        attendanceSheet.Add(calender[j], "-");
                    }
                }
                j++;
            }
            return attendanceSheet;
        }
        public List<Attendance> GetStudentRecord(int studentId)
        {
            List<Attendance> attendanceRecord = new List<Attendance>(500);
            User student = context.Users.Where(u=>u.Id==studentId)
                .Include(s=>s.StudentsAttendance)
                .ThenInclude(s=>s.attendance)
                .FirstOrDefault();
            
            var studentsAttendances = student.StudentsAttendance;
            var orderedstudentsAttendances = from s in studentsAttendances orderby s.attendance.TimeStamps select s;
            foreach (var studentsAttendance in orderedstudentsAttendances)
            {
                attendanceRecord.Add(studentsAttendance.attendance);
            }
            return attendanceRecord;
        }
        public int GetStudentId(string studentName)
        {
            User student = context.Users.Where(u => u.Name.Equals(studentName))
                .FirstOrDefault();
            return student.Id;
        }
        public void CheckClassDays()
        {
          List<string> calender = new List<string>(GetCalender());
            foreach (string date in calender)
            {
                Console.WriteLine($"\t\t\t\t class date = {date}  ");
            }
            HasGetBack();
        }
        //Generates Calender based on start and end date of a course
        public List<string> GetCalender()
        {
            List<string> calender = new List<string>();
            List<string> days = new List<string>(20);
            int course_id = GetTeachersCourseID();
            Course course = context.Courses.Where(c=>c.Id == course_id)
                .Include(cs=>cs.CourseSchedules)
                .ThenInclude(s=>s.schedule)
                .FirstOrDefault();
            var course_schedules = course.CourseSchedules;
            foreach (var course_schedule in course_schedules)
            {
                days.Add(course_schedule.schedule.Day.ToString());
            }
            DateTime start_date =  course.StartDate.Date;
            DateTime end_date = course.EndDate.Date;
            for (var date=start_date; date<=end_date; date = date.AddDays(1))
            {
                foreach (var day in days)
                {
                    if(date.DayOfWeek.ToString().Equals(day))
                    {
                        calender.Add(date.Date.ToString("d-MMM-yy"));
                    }
                }
            }
            return calender;
        }
    }
}
