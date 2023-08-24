using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class AdminUtility : IInterface
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
        public void CreateStudent(string uname,string password)
        {
            context.Users.Add(new User { Name=uname,Password = password ,Type = "Student" });
            if(context.SaveChanges()>0)
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
            if(context.SaveChanges()>0)
            {
                Console.WriteLine($"{sname} , was enrolled in {course.Name}");
            }
        }

        // Remove A Student
        public void RemoveStudent(string uname)
        {
            User user = context.Users.Where(u => u.Name == uname).FirstOrDefault();
            context.Users.Remove(user);
            if (context.SaveChanges()>0)
            {
                Console.WriteLine("Deleted Successfully");
            }
        }
        public void CreateTeacher(string uname,string password)
        {
            context.Users.Add(new User { Name=uname,Password = password ,Type = "Teacher" });
            if(context.SaveChanges()>0)
            {
                Console.WriteLine($"{uname} was successfully added as a teacher . . .");
                GetAllTeachers();
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
            Students = context.Users.Where(s=>s.Type== "Student").ToList();
            foreach (var student in Students)
            {
                Console.WriteLine($"Student Name = {student.Name}\tPassword={student.Password}");
            }
        }
        public void GetAllTeachers()
        {
            Console.WriteLine("Teachers Lists :");
            List<User> Teachers = new List<User>(200);
            Teachers = context.Users.Where(s=>s.Type== "Teacher").ToList();
            foreach (var teacher in Teachers)
            {
                Console.WriteLine($"Teacher Name = {teacher.Name}\tPassword={teacher.Password}");
            }
        }
        public void GetAllTeachersCourse()
        {
            Console.WriteLine("Teachers Course Lists :");
            List<User> Teachers = new List<User>(200);
            var TeachersWithCourse = context.Users.Where(s=>s.Type== "Teacher").Include(tc=>tc.TeachersCourse).ThenInclude(c=>c.course).ToList();
            foreach (var teacher in TeachersWithCourse)
            {
                
                Console.WriteLine($"Teacher Name = {teacher.Name}\t Course = {teacher.TeachersCourse.course.Name}\t Course Code = {teacher.TeachersCourse.course.CourseCode}");
            }
        }
    }
}
