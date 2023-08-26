﻿// <auto-generated />
using System;
using Assignment3;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assignment3.Migrations
{
    [DbContext(typeof(EDBContext))]
    [Migration("20230825174356_CreateAssnDB")]
    partial class CreateAssnDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Assignment3.Attendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("TimeStamps")
                        .HasColumnType("datetime2");

                    b.Property<int>("courseId")
                        .HasColumnType("int");

                    b.Property<int>("studentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("courseId");

                    b.HasIndex("studentId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("Assignment3.AttendanceStudents", b =>
                {
                    b.Property<int>("AttendanceId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("AttendanceId", "StudentId");

                    b.HasIndex("AttendanceId")
                        .IsUnique();

                    b.HasIndex("StudentsId");

                    b.ToTable("AttendanceStudents");
                });

            modelBuilder.Entity("Assignment3.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Fees")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Assignment3.CourseAttendance", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("AttendanceId")
                        .HasColumnType("int");

                    b.HasKey("CourseId", "AttendanceId");

                    b.HasIndex("AttendanceId")
                        .IsUnique();

                    b.ToTable("CourseAttendance");
                });

            modelBuilder.Entity("Assignment3.CourseSchedule", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.HasKey("CourseId", "ScheduleId");

                    b.HasIndex("ScheduleId")
                        .IsUnique();

                    b.ToTable("CourseSchedule");
                });

            modelBuilder.Entity("Assignment3.CourseStudent", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("CourseId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("CourseStudent");
                });

            modelBuilder.Entity("Assignment3.CourseTeacher", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("CourseId", "TeacherId");

                    b.HasIndex("CourseId")
                        .IsUnique();

                    b.HasIndex("TeacherId")
                        .IsUnique();

                    b.ToTable("CourseTeacher");
                });

            modelBuilder.Entity("Assignment3.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("End_time")
                        .HasColumnType("datetime2");

                    b.Property<string>("SCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Start_time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Assignment3.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin",
                            Password = "123456",
                            Type = "Admin"
                        });
                });

            modelBuilder.Entity("Assignment3.Attendance", b =>
                {
                    b.HasOne("Assignment3.Course", "course")
                        .WithMany()
                        .HasForeignKey("courseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignment3.User", "student")
                        .WithMany()
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");

                    b.Navigation("student");
                });

            modelBuilder.Entity("Assignment3.AttendanceStudents", b =>
                {
                    b.HasOne("Assignment3.Attendance", "attendance")
                        .WithOne("attendanceStudents")
                        .HasForeignKey("Assignment3.AttendanceStudents", "AttendanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignment3.User", "Students")
                        .WithMany("StudentsAttendance")
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Students");

                    b.Navigation("attendance");
                });

            modelBuilder.Entity("Assignment3.CourseAttendance", b =>
                {
                    b.HasOne("Assignment3.Attendance", "attendance")
                        .WithOne("attendanceCourse")
                        .HasForeignKey("Assignment3.CourseAttendance", "AttendanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignment3.Course", "course")
                        .WithMany("CourseAttendances")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("attendance");

                    b.Navigation("course");
                });

            modelBuilder.Entity("Assignment3.CourseSchedule", b =>
                {
                    b.HasOne("Assignment3.Course", "course")
                        .WithMany("CourseSchedules")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignment3.Schedule", "schedule")
                        .WithOne("SchedulesCourse")
                        .HasForeignKey("Assignment3.CourseSchedule", "ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");

                    b.Navigation("schedule");
                });

            modelBuilder.Entity("Assignment3.CourseStudent", b =>
                {
                    b.HasOne("Assignment3.Course", "course")
                        .WithMany("CourseStudents")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignment3.User", "Student")
                        .WithMany("StudentsCourse")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("course");
                });

            modelBuilder.Entity("Assignment3.CourseTeacher", b =>
                {
                    b.HasOne("Assignment3.Course", "course")
                        .WithOne("courseTeacher")
                        .HasForeignKey("Assignment3.CourseTeacher", "CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignment3.User", "teacher")
                        .WithOne("TeachersCourse")
                        .HasForeignKey("Assignment3.CourseTeacher", "TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");

                    b.Navigation("teacher");
                });

            modelBuilder.Entity("Assignment3.Attendance", b =>
                {
                    b.Navigation("attendanceCourse")
                        .IsRequired();

                    b.Navigation("attendanceStudents")
                        .IsRequired();
                });

            modelBuilder.Entity("Assignment3.Course", b =>
                {
                    b.Navigation("CourseAttendances");

                    b.Navigation("CourseSchedules");

                    b.Navigation("CourseStudents");

                    b.Navigation("courseTeacher")
                        .IsRequired();
                });

            modelBuilder.Entity("Assignment3.Schedule", b =>
                {
                    b.Navigation("SchedulesCourse")
                        .IsRequired();
                });

            modelBuilder.Entity("Assignment3.User", b =>
                {
                    b.Navigation("StudentsAttendance");

                    b.Navigation("StudentsCourse");

                    b.Navigation("TeachersCourse")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
