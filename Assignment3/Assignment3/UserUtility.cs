using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class UserUtility
    {
        EDBContext context;
        User _user;
        public UserUtility()
        {
            context = new EDBContext();
        }
        public void Init()
        {
            if(_user.Type=="Admin")
            {
                AdminUtility admin = new AdminUtility();
                admin.Interface();
            }
            if(_user.Type=="Student")
            {
                StudentUtility student = new StudentUtility();
                student.Interface(_user);
            }
        }
        public void Login()
        {
            Console.Clear();
            string name,password;
            Console.WriteLine("______  Login ______");
            Console.WriteLine("UserName : ");
            name = Console.ReadLine();
            Console.WriteLine("Password : ");
            password = Console.ReadLine();
            User user = CredentialCheck(name, password);
            if (user!=null)
            {
               
                _user= user;
                Init();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Sorry.. Invalid credentials provided ");
                Login();
            }
        }
        public void Logout()
        {
            _user = null;
            Login();
            //Environment.Exit(0);
        }
        public object CredentialCheck(string uname, string password)
        {
            User user = context.Users.Where(u=>u.Name.Equals(uname) && u.Password.Equals(password)).FirstOrDefault();
            if(user != null)
            {
                if (user.Type=="Student" && user.StudentsCourse != null)
                {
                   object student = context.Users.Where(u=>u.Name.Equals(uname)).Include(sc=>sc.StudentsCourse).ThenInclude(c=>c.course);
                    return student;
                }
                if (user.TeachersCourse != null)
                {
                    object teacher = context.Users.Where(u => u.Name.Equals(uname) && u.Password.Equals(password)).Include(sc => sc.StudentsCourse);
                }
                else
                    return user;
            }
            
            else
                return null;
        }
    }
}
