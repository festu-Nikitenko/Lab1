using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Ivan
{
    interface IDateAndCopy
    {
        object DeepCopy();
        DateTime Date { get; set; }
    }
    class Person: IDateAndCopy
    {
        private string Name;
        private string LastName;
        private DateTime Birth;
        public string name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }
        public string lastname
        {
            get
            {
                return LastName;
            }
            set
            {
                LastName = value;
            }
        }
        public DateTime Date
        {
            get
            {
                return Birth;
            }
            set
            {
                Birth = value;
            }
        }
        public Person()
        {
            Name = "Ivan";
            LastName = "Kuznetsov";
            Birth = new DateTime(1993,1,1);

        }
        public Person(string N, string L, DateTime B)
        {
            Name = N;
            LastName = L;
            Birth = B;
        }
        public override string ToString()
        {
            return Name + " " + LastName + " " + Birth.ToShortDateString();
        }
        public string ToShortString()
        {
            return Name + " " + LastName;
        }
        public override bool Equals(object obj)
        {
            Person p2;
            if (obj != null)
            {
                p2 = obj as Person;
            }
            else
            {
                return false;
            }
            if (p2.name == Name & p2.lastname == LastName & p2.Date == Birth)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator ==(Person a, Person b)
        {
            if (a != null && b != null)
            {
                return a.Equals(b);
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(Person a, Person b)
        {
            return !a.Equals(b);            
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ LastName.GetHashCode() ^ Birth.GetHashCode();
        }
        public object DeepCopy()
        {
            return new Person(Name, LastName, new DateTime(Birth.Year, Birth.Month, Birth.Day));
        }
    }
    public enum Education
    {
        Specialist = 0,
        Bachelor = 1,
        SecondEducation = 2
    }
    class Exam:IDateAndCopy
    {
        public string Name;
        public int Mark;
        DateTime Day;
        public Exam(string name, int mark, DateTime date)
        {
            Name = name;
            Mark = mark;
            Day = date;
        }
        public Exam()
        {
            Name = "OOP";
            Mark = 5;
            Day = new DateTime(2013, 2, 01);
        }
        public override string ToString()
        {
            return Name + " " + Mark.ToString() + " " + Day.ToShortDateString();
        }
        public DateTime Date
        {
            get
            {
                return Day;
            }
            set
            {
                Day = value;
            }
        }
        public object DeepCopy()
        {
            return new Exam(Name, Mark, new DateTime(Day.Year, Day.Month, Day.Day));
        }
    }
    class Test:IDateAndCopy
    {
        string Name;
        bool Pass;
        DateTime Day = DateTime.Now;
        public DateTime Date
        {
            get
            {
                return Day;
            }
            set
            {
                Day = value;
            }
        }
        public Test()
        {
            Name = "OOP";
            Pass = true;
        }
        public Test(string name, bool pass)
        {
            Name = name;
            Pass = pass;
        }
        public override string ToString()
        {
            switch (Pass)
            {
                case true:
                    return Name + " Yes " + Day.ToShortDateString();
                case false:
                    return Name + " No " + Day.ToShortDateString();
                default:
                    return Name + " Unknown " + Day.ToShortDateString();
            }
        }
        public object DeepCopy()
        {
            return new Test(Name, Pass);
        }
    }
    class Student : Person, IDateAndCopy
    {
        private Education Form;
        private int Group;
        private ArrayList Tests;
        private Exam[] Exams;
        public Student(Person Pers, Education form, int group)
        {
            name = Pers.name;
            lastname = Pers.lastname;
            Date = Pers.Date;
            Form = form;
            Group = group;
            Tests = new ArrayList();
            Exams = new Exam[0];
        }
        public Student()
        {
            name = "Ivan";
            lastname = "Kuznetsov";
            Date = new DateTime(1993, 1, 1);
            Form = Education.Specialist;
            Group = 100;
            Tests = new ArrayList();
            Exams = new Exam[0];
        }
        public Person pers
        {
            get
            {
                return new Person(name, lastname, Date);
            }
            set
            {
                name = value.name;
                lastname = value.lastname;
                Date = value.Date;
            }
        }
        public ArrayList tests
        {
            get
            {
                return Tests;
            }
            set
            {
                Tests = value;
            }
        }
        public Exam[] exams
        {
            get
            {
                return Exams;
            }
            set
            {
                Exams = value; 
            }
        }
        public double Average
        {
            get
            {
                int sum = 0;
                if (Exams.Length != 0)
                {
                for(int i=0; i<Exams.Length; i++)
                {
                    sum+=Exams[i].Mark;
                }
                return sum / Exams.Length;
                }
                else
                {
                    return 0;
                }
            }
        }
        public void AddExams(params Exam[] Add)
        {
            int L = Exams.Length;
            Array.Resize(ref Exams, Exams.Length + Add.Length);
            Array.ConstrainedCopy(Add, 0, Exams, L, Add.Length);
        }
        public void AddTests(ArrayList add)
        {
            Tests.AddRange(add);
        }
        public override string ToString()
        {
            string s1 = "";
            string s2 = "";
            for (int i = 0; i < Tests.Count; i++)
            {
                s1 = s1 + Tests[i].ToString()+ "; ";
            }
            for (int i = 0; i < Exams.Length; i++)
            {
                s2 = s2 + Exams[i].ToString() + "; ";
            }
            return base.ToString() + " " + Form.ToString() + " " + Group.ToString() + " " + "Tests: " + s1 + "Exams: " + s2;
        }
        public new string ToShortString()
        {
            return base.ToString() + " " + Form.ToString() + " " + Group.ToString() + " " + Average;
        }
        public new object DeepCopy()
        {
            return new Student(base.DeepCopy() as Person, Form, Group); 
        }
        public int group
        {
            get
            {
                return Group;
            }
            set
            {
                if ((value < 101) && (value > 599))
                {
                    throw new Exception("Group number should be in 100-599 range");
                }
                Group = value;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Student Some = new Student(new Person("Ivan", "Kozlov", new DateTime(1993, 12, 12)), Education.Bachelor, 101);
            Console.WriteLine(Some.ToShortString());
            Console.WriteLine();
            Console.WriteLine(Some.ToString());
            Console.WriteLine();
            Some.AddExams(new Exam[]{new Exam("OOP", 5, DateTime.Now), new Exam("GoTh", 4, DateTime.Today)});
            Console.WriteLine(Some.ToString());
            Console.WriteLine();
            Person A = new Person();
            Person B = new Person();
            Console.WriteLine((A == B).ToString());
            Console.WriteLine(A.GetHashCode().ToString() + "  " + B.GetHashCode().ToString());
            Console.WriteLine();
            ArrayList Tests = new ArrayList();
            Tests.Add(new Test());
            Tests.Add(new Test("IGPR", false));
            Some.AddTests(Tests);
            Console.WriteLine(Some.ToString());
            Console.WriteLine();            
            Console.WriteLine(Some.pers.ToString());
            Console.WriteLine();

        }
    }
}
