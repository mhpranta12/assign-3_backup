using Assignment3;
using Microsoft.EntityFrameworkCore;

UserUtility user = new UserUtility();
user.Login();
//admin.CreateStudent("Ehmad", "8888");
//admin.GetAllStudents();
//Add a new Course
//admin.GetAllTeachersCourse();
//context.Courses.Add(new Course { Name = "Programming with c#",CourseCode="PC#", Fees = 8000 });
//context.SaveChanges();

//Get All Users

//List<User> userlist = new List<User>();
//userlist = context.Users.ToList();
//foreach (var user in userlist)
//{
//    Console.WriteLine($"    Name = {user.Name}       Type = {user.Type} ");
//}


//Getting all Student of a course

//Course c = context.Courses.Where(x => x.Id == 2)
//    .Include(y => y.CourseStudents)
//    .ThenInclude(z => z.Student).FirstOrDefault();

//foreach (var item in c.CourseStudents)
//{
//    Console.WriteLine(item.Student.Name);
//}

//Create a Teacher

//context.Users.Add(new User { Name = "Jalal Uddin", Password = "1111", Type = "Teacher" });
//context.SaveChanges();

//Assign a Teacher in a Course

//User teacher = context.Users.Where(x => x.Name == "Jalal Uddin").FirstOrDefault();
//Course c = context.Courses.Where(x => x.Id == 2).FirstOrDefault();
//CourseTeacher courseTeacher = new CourseTeacher();
//courseTeacher.teacher = teacher;
//c.courseTeacher = courseTeacher;
//context.SaveChanges();

//Getting teacher of a specific Course

//Course course = context.Courses.Where(x => x.Id == 2)
//    .Include(ct => ct.courseTeacher)
//    .ThenInclude(t => t.teacher)
//    .FirstOrDefault();
//Console.WriteLine(course.courseTeacher.teacher.Name);


//Create a Day Time for a schedule 

//context.DayTimes.Add(new DayTime { Day = "Monday", Stime = 9, Etime = 1, Meridiam = "PM" });
//context.DayTimes.Add(new DayTime { Day = "Wednesday", Stime = 12, Etime = 1, Meridiam = "PM" });
//context.DayTimes.Add(new DayTime { Day = "Thrusday", Stime = 9, Etime = 11, Meridiam = "PM" });
//context.SaveChanges();

//Create a Schedule 

//context.Schedules.Add(new Schedule { StartDate = new DateTime(2023, 8, 23), EndDate = new DateTime(2023, 10, 11) });
//context.SaveChanges();

//Assign Daytimes in a Schedule
//Schedule schedule = context.Schedules.Where(s => s.Id == 1).FirstOrDefault();
//DayTime dayTime = context.DayTimes.Where(d => d.Id == 3).FirstOrDefault();
//List<ScheduleDaytime> scheduleDaytimes = new List<ScheduleDaytime>()
//{
//    new ScheduleDaytime {dayTime = dayTime},
//};
//schedule.SchedulesDayTimes = scheduleDaytimes;
//context.SaveChanges();

//Assign a Schedule in a Course 
//Schedule schedule = context.Schedules.Where(s => s.Id == 1).FirstOrDefault();
//Course course = context.Courses.Where(c => c.Id == 2)
//        .Include(cs => cs.CourseSchedules)
//        .ThenInclude(s => s.schedule)
//        .FirstOrDefault();
//List<CourseSchedule> courseSchedules = new List<CourseSchedule>()
//{
//     new CourseSchedule { schedule=schedule}
//};
//course.CourseSchedules = courseSchedules;
//context.SaveChanges();

//Remove a Day Time

//DayTime dayTime = context.DayTimes.Where(d => d.Day == "Wednesday").FirstOrDefault();
//context.DayTimes.Remove(dayTime);
//context.SaveChanges();

//  Giving attendance operation for Student 
//Course course = context.Courses.Where(c => c.Id == 2)
//    .Include(ct => ct.courseTeacher)
//    .ThenInclude(t => t.teacher)
//    .Include(cs => cs.CourseSchedules)
//    .ThenInclude(s => s.schedule)
//    .ThenInclude(sd => sd.SchedulesDayTimes)
//    .ThenInclude(d => d.dayTime)
//    .FirstOrDefault();

//DateTime now = DateTime.Now;
//string day = DateTime.Now.DayOfWeek.ToString();
//bool flag = false;
//foreach (var item in course.CourseSchedules)
//{

//    Console.WriteLine("S date = " + item.schedule.StartDate.Date);
//    var schedule = item.schedule.SchedulesDayTimes;
//    foreach (var dt in schedule)
//    {
//        var DT = dt.dayTime;
//        if (DT.Meridiam == "PM")
//        {
//            DT.Stime += 12;
//            DT.Etime += 12;
//        }
//        Console.WriteLine($"DayNow = {day}  DayDB = {DT.Day} , STime = {DT.Stime}, TimeNow = {now.Hour} ,E Time = {DT.Etime}");
//        if ((now.Date >= item.schedule.StartDate.Date && now.Date <= item.schedule.EndDate.Date) && day == DT.Day && (now.Hour >= DT.Stime || now.Hour <= DT.Etime))
//        {

//            if (now.Hour == DT.Etime)
//            {
//                if (now.Minute > 0)
//                {
//                    break;
//                }
//            }
//            flag = true;
//            Console.WriteLine("Attendance Taken ");
//            break;
//        }
//    }
//    if (!flag)
//        Console.WriteLine("Sry u can't give attendance today");
//}


//Getting Schedule with Day and Time of a course 
//Course course = context.Courses.Where(c => c.Id == 2)
//    .Include(ct => ct.courseTeacher)
//    .ThenInclude(t => t.teacher)
//    .Include(cs => cs.CourseSchedules)
//    .ThenInclude(s => s.schedule)
//    .ThenInclude(sd => sd.SchedulesDayTimes)
//    .ThenInclude(d => d.dayTime)
//    .FirstOrDefault();
//Console.WriteLine("Teacher " +course.courseTeacher.teacher.Name);
//foreach (var item in course.CourseSchedules)
//{
//    var schedule = item.schedule.SchedulesDayTimes;
//    foreach (var dt in schedule)
//    {
//        var DT = dt.dayTime;
//        Console.WriteLine($" {DT.Day} ({DT.Stime} - {DT.Etime}) {DT.Meridiam}");
//        Console.WriteLine();
//    }
//}