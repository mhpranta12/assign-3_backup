using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class EDBContext :DbContext
    {
        private string constr;
        public EDBContext()
        {
            constr = "Server=DESKTOP-LUSTUV3\\SQLEXPRESS;Database=Assignment;User Id=user;Password=1234;TrustServerCertificate=True";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(constr);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //composite primary key generations
            modelBuilder.Entity<CourseStudent>()
                .HasKey((x) => new { x.CourseId, x.StudentId });
            modelBuilder.Entity<CourseTeacher>()
                .HasKey((x)=> new {x.CourseId,x.TeacherId});
            modelBuilder.Entity<CourseSchedule>()
                .HasKey((x) => new { x.CourseId, x.ScheduleId });
            modelBuilder.Entity<AttendanceStudents>()
                .HasKey((x) => new { x.AttendanceId, x.StudentId });
            modelBuilder.Entity<ScheduleDaytime>()
                .HasKey((x) => new { x.ScheduleId, x.DayTimeId });
            //Indexing
            modelBuilder.Entity<Course>()
                .HasIndex(x => x.Name);
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Name);
            modelBuilder.Entity<Attendance>()
                .HasOne(x => x.course);

            //Course->Teacher one to one 
            modelBuilder.Entity<CourseTeacher>()
                .HasOne(x => x.course)
                .WithOne(x => x.courseTeacher);
            modelBuilder.Entity<CourseTeacher>()
                .HasOne(x => x.teacher)
                .WithOne(x => x.TeachersCourse);
            //Course->Student Many to Many
            modelBuilder.Entity<CourseStudent>()
                .HasOne(x => x.course)
                .WithMany(x => x.CourseStudents);
            modelBuilder.Entity<CourseStudent>()
                .HasOne(x => x.Student)
                .WithMany(x => x.StudentsCourse);
            //Schedule->Course One to Many
            modelBuilder.Entity<CourseSchedule>()
                .HasOne(x => x.schedule)
                .WithOne(x => x.SchedulesCourse);
            modelBuilder.Entity<CourseSchedule>()
                .HasOne(x => x.course)
                .WithMany(x => x.CourseSchedules);
            //Schedule -> Daytime One to Many
            modelBuilder.Entity<ScheduleDaytime>()
                .HasOne(x => x.schedule)
                .WithMany(x => x.SchedulesDayTimes);
            modelBuilder.Entity<ScheduleDaytime>()
                .HasOne(x => x.dayTime)
                .WithOne(x => x.DaytimesSchedule);
            //Attendance->Students One to Many
            modelBuilder.Entity<AttendanceStudents>()
                .HasOne(x => x.attendance)
                .WithOne(x => x.attendanceStudents);
            modelBuilder.Entity<AttendanceStudents>()
                .HasOne(x => x.Students)
                .WithMany(x => x.StudentsAttendance);

            //Seeding 
            modelBuilder.Entity<User>()
                .HasData(new User { Id = 1, Name = "admin", Password = "1234", Type = "Admin" });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<DayTime> DayTimes { get; set; }
    }
}
