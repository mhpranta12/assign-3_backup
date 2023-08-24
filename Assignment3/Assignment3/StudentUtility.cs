﻿using Microsoft.EntityFrameworkCore;
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
        public StudentUtility(User user)
        {
            context = new EDBContext();
            _student = user;
        }
        public void Interface()
        {
            Console.Clear();
            Console.WriteLine($"Welcome {_student.Name}");
            //if (Console.ReadKey().Key == ConsoleKey.DownArrow)
            //{
            //    if (Console.ReadKey().Key == ConsoleKey.Enter)
            //        GiveAttendance();
            //}
            Console.WriteLine("1. Give Attendance");
            Console.WriteLine("2. Log Out");
            string a = Console.ReadLine();
            Action(a);
        }
        public void Action(string a)
        {
            if (a == "1")
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
            User student = new User();
            student = context.Users.Where(u => u.Id == _student.Id).
                Include(sc => sc.StudentsCourse)
                .ThenInclude(c => c.course)
                .FirstOrDefault();

            Console.WriteLine("You are enrolled in :");
            var students_course = student.StudentsCourse;
            foreach (var studentcourse in students_course)
            {
                int i = 1;
                Console.WriteLine($"{i} Course Name = {studentcourse.course.Name} Course Code = {studentcourse.course.CourseCode}");
                i++;
            }
        }

        public void GiveAttendance()
        {
            DisplayEnrolledCourses();
            Console.WriteLine("Enter Course Code to give attendance :");
            string cc = Console.ReadLine();
            PerformAttendance(cc);
        }
        public void PerformAttendance(string coursecode)
        {
            OnTime(coursecode);
            bool hascourse = false;
            bool ontime = false;
            User currentstudent = context.Users.
                Where(u => u.Id == _student.Id)
                .FirstOrDefault();

            Course coursetoattend = context.Courses
                .Where(c => c.CourseCode == coursecode)
                .FirstOrDefault();
            hascourse = HasCourse(currentstudent, coursecode);
            ontime = OnTime(coursecode);
            if (!hascourse)
            {
                Console.WriteLine("Sorry, You are not enrolled in this course");
            }
            if (!ontime)
            {
                Console.WriteLine("Sorry You aren't allowed to give attendance at this time ");
            }
            if (hascourse && ontime)
            {
                AttendanceStudents attendingStudents = (new AttendanceStudents { Students = currentstudent });
                context.Attendances.Add(new Attendance { student = currentstudent, course = coursetoattend, TimeStamps = DateTime.Now, attendanceStudents = attendingStudents });
                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine("Attendance taken !");
                }
            }
        }
        public bool HasCourse(User student, string coursecode)
        {
            User currentstudent = context.Users.Where(u => u.Id == student.Id)
                .Include(sc => sc.StudentsCourse)
                .ThenInclude(c => c.course).FirstOrDefault();
            var StudentCourses = student.StudentsCourse;
            foreach (var scourse in StudentCourses)
            {
                if (scourse.course.CourseCode.Equals(coursecode))
                {
                    return true;
                }
            }
            return false;
        }
        public bool OnTime(string coursecode)
        {
            bool date = false;
            bool day = false;
            bool time = false;
            DateTime now = DateTime.Now;
            ////Retreving course including schedule and day time
            Course coursetoattend = context.Courses.Where(c => c.CourseCode == coursecode)
                .Include(cs => cs.CourseSchedules)
                .ThenInclude(s => s.schedule)
                .ThenInclude(sdt => sdt.SchedulesDayTimes)
                .ThenInclude(dt => dt.dayTime)
                .FirstOrDefault();
            var courseschedules = coursetoattend.CourseSchedules;
            var today = DateOnly.FromDateTime(now);
            string thisday = now.DayOfWeek.ToString();
            //Date Checking
            foreach (var courseschedule in courseschedules)
            {
                if (((DateOnly.FromDateTime(courseschedule.schedule.StartDate)) <= today) && (today <= (DateOnly.FromDateTime(courseschedule.schedule.EndDate))))
                {
                    date = true;
                }
                foreach (var schedulesDaytimes in courseschedule.schedule.SchedulesDayTimes)
                {
                    if ((now.TimeOfDay >= schedulesDaytimes.dayTime.Start_time.TimeOfDay) &&
                        (now.TimeOfDay <= schedulesDaytimes.dayTime.End_time.TimeOfDay))
                    {
                        time = true;
                    }
                    if (schedulesDaytimes.dayTime.Day.Equals(thisday))
                    {
                        day = true;
                    }
                }
            }
            if (date && time && day)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //public void OnTimeCheck(string coursecode)
        //{
        //    bool date = false;
        //    bool time = false;
        //    DateTime now = DateTime.Now;
        //    ////Retreving course including schedule and day time
        //    Course coursetoattend = context.Courses.Where(c => c.CourseCode == coursecode)
        //        .Include(cs => cs.CourseSchedules)
        //        .ThenInclude(s => s.schedule)
        //        .ThenInclude(sdt => sdt.SchedulesDayTimes)
        //        .ThenInclude(dt => dt.dayTime)
        //        .FirstOrDefault();
        //    var courseschedules = coursetoattend.CourseSchedules;
        //    //Date Checking
        //    foreach (var courseschedule in courseschedules)
        //    {
        //        Console.WriteLine($"sd ={courseschedule.schedule.StartDate.Date} now= {now.Date} ed = {courseschedule.schedule.EndDate}");
        //        if ((DateOnly.FromDateTime(courseschedule.schedule.StartDate)) >= (DateOnly.FromDateTime(now)))
        //            Console.WriteLine("Strat date ok");
        //        if((DateOnly.FromDateTime(courseschedule.schedule.EndDate) == DateOnly.FromDateTime(now)))
        //            Console.WriteLine("End date ok");
        //        //if ((courseschedule.schedule.StartDate.Date >= now.Date) && (courseschedule.schedule.EndDate <= now.Date))
        //        //{
        //        //    Console.WriteLine("Date Ok");
        //        //}
        //        foreach (var schedulesDaytimes in courseschedule.schedule.SchedulesDayTimes)
        //        {
        //            Console.WriteLine($"stime = {schedulesDaytimes.dayTime.Start_time.TimeOfDay} now ={DateTime.Now.TimeOfDay} etime = {schedulesDaytimes.dayTime.End_time.TimeOfDay}");
        //            if ((now.TimeOfDay >= schedulesDaytimes.dayTime.Start_time.TimeOfDay) &&
        //                (now.TimeOfDay <= schedulesDaytimes.dayTime.End_time.TimeOfDay))
        //            {
        //                Console.WriteLine("Time OK");
        //            }
        //        }
        //    }
        //}
        //}
        //public bool OnTime(Course courseschedule.schedule.SchedulesDayTimes)
        //{

        //}
    }
}
