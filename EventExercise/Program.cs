using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExercise
{
    public class Student
    {
        private string name;
        private string sex;
        private string ID;
        public Student(string name, string sex, string ID)
        {
            this.name = name;
            this.sex = sex;
            this.ID = ID;
        }

        public delegate void StudenterEventHandler(Object studentObj,StudentEventArgs e);
        public event StudenterEventHandler Studenter;

        public class StudentEventArgs:EventArgs
        {
            public readonly string name;
            public readonly string sex;
            public readonly string ID;
            public StudentEventArgs(string name,string sex,string ID)
            {
                this.name = name;
                this.sex = sex;
                this.ID = ID;
            }
        }
        protected virtual void OnDisplay(StudentEventArgs e)
        {
            if(Studenter!=null)
            {
                Studenter(this,e);
            }
        }
        public void Display()
        {
            StudentEventArgs e = new StudentEventArgs(name,sex,ID);
            OnDisplay(e);
        }
    }

    public class DisplayName
    {
        public void displayName(Object studentObj, Student.StudentEventArgs e)
        {
            Student student=(Student)studentObj;
            Console.WriteLine("student name:{0}",e.name);
            Console.WriteLine();
        }
    }
    public class DisplaySex
    {
        public void displaySex(Object studentObj,Student.StudentEventArgs e)
        {
            Student student = (Student)studentObj;
            Console.WriteLine("student sex:{0}",e.sex);
            Console.WriteLine();
        }
    }
    public class DisplayID
    {
        public void displayID(Object studentObj,Student.StudentEventArgs e)
        {
            Student student = (Student)studentObj;
            Console.WriteLine("student sex:{0}", e.ID);
            Console.WriteLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student("彭麒麟","male","2017234259");
            DisplayName displayname = new DisplayName();
            DisplaySex displaysex = new DisplaySex();
            DisplayID displayid = new DisplayID();
            student.Studenter += displayname.displayName;
            student.Studenter += displaysex.displaySex;
            student.Studenter += displayid.displayID;
            student.Display();
        }
    }
}
