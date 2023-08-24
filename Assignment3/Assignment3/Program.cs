using Assignment3;

//AdminUtility admin = new AdminUtility();
//admin.CreateStudent("Aslam", "1111");
//admin.CreateTeacher("Jalal Uddin", "2222");
//admin.CreateCourse("Professional Programming With C#", "C#", 8000);
//admin.AssignTeacher("Jalal Uddin", "C#");
//admin.EnrollStudent("Ishqiaque", "C#");
//admin.GetAllStudentOfCourse("C#");
//admin.GetAllTeachersCourse();
//admin.AddDayTime("dt1", "Monday", "9:00 PM", "11:00 PM");
//admin.AddDayTime("dt2", "Thursday", "9:00 PM", "11:00 PM");
//admin.CreateSchedule("S1", "1/5/23", "31/8/23");
//admin.AssignDayTime("S1", "dt1", "dt2");
//admin.AssignSchedule("C#", "S1");

UserUtility user = new UserUtility();
user.Login();
//Getting all Student of a course

//Course c = context.Courses.Where(x => x.Id == 1)
//    .Include(y => y.CourseStudents)
//    .ThenInclude(z => z.Student).FirstOrDefault();

//foreach (var item in c.CourseStudents)
//{
//    Console.WriteLine(item.Student.Name);
//}


//Assign a Teacher in a Course

//User teacher = context.Users.Where(x => x.Name == "Jalal Uddin").FirstOrDefault();
//Course c = context.Courses.Where(x=>x.Id==1).FirstOrDefault();
//CourseTeacher courseTeacher = new CourseTeacher();
//courseTeacher.teacher = teacher;
//c.courseTeacher = courseTeacher;
//context.SaveChanges();

//Getting teacher of a specific Course

//Course course = context.Courses.Where(x => x.Id == 1)
//    .Include(ct => ct.courseTeacher)
//    .ThenInclude(t => t.teacher)
//    .FirstOrDefault();
//Console.WriteLine(course.courseTeacher.teacher.Name);


//Create a Day Time for a schedule 

//context.DayTimes.Add(new DayTime { Day = "Tuesday", Stime = 6, Etime = 8, Meridiam = "PM" });
//context.DayTimes.Add(new DayTime { Day = "Thrusday", Stime = 9, Etime = 11, Meridiam = "PM" });
//context.SaveChanges();

//Create a Schedule 

//context.Schedules.Add(new Schedule { StartDate = new DateTime(2023, 8, 23), EndDate = new DateTime(2023, 10, 11) });
//context.SaveChanges();

//Assign Daytimes in a Schedule
//Schedule schedule = context.Schedules.Where(s => s.Id == 2).FirstOrDefault();
//DayTime dayTime = context.DayTimes.Where(d => d.Id == 3).FirstOrDefault();
//DayTime dayTime2 = context.DayTimes.Where(d => d.Id == 4).FirstOrDefault();
//List<ScheduleDaytime> scheduleDaytimes = new List<ScheduleDaytime>()
//{
//    new ScheduleDaytime {dayTime = dayTime},
//    new ScheduleDaytime {dayTime = dayTime2}
//};
//schedule.SchedulesDayTimes = scheduleDaytimes;
//context.SaveChanges();

//Assign a Schedule in a Course 
//Schedule schedule = context.Schedules.Where(s => s.Id == 2).FirstOrDefault();
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
//                if (now.Minute>0)
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
