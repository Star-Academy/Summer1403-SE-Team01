using System.Runtime.Intrinsics.Arm;
using System.Text.Json;

namespace ScoreBoard
{
    public class Student
    {
        private int _studentNumber;
        public int StudentNumber
        {
            get { return _studentNumber; }
            set { _studentNumber = value; }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private Dictionary<string, double> _courses = new Dictionary<string, double>();
        public Dictionary<string, double> Courses
        {
            get { return _courses; }
            set { _courses = value; }
        }

        private double _avg;
        public double Avg
        {
            get { return _avg; }
            set { _avg = value; }
        }
    }



    public class Course
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private double _score;
        public double Score
        {
            get { return _score; }
            set { _score = value; }
        }
    }

    public class StudentLesson
    {
        private int _studentNumber;
        public int StudentNumber
        {
            get { return _studentNumber; }
            set { _studentNumber = value; }
        }

        private string _lesson;
        public string Lesson
        {
            get { return _lesson; }
            set { _lesson = value; }
        }

        private double _score;
        public double Score
        {
            get { return _score; }
            set { _score = value; }
        }
    }



    class Program
    {
        static void Main(String[] args)
        {
            JsonSerializerOptions _options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            var json = File.ReadAllText("./files/students.json");
            List<Student> students = JsonSerializer.Deserialize<List<Student>>(json, _options);

            var json1 = File.ReadAllText("./files/scores.json");
            List<StudentLesson> studentLessons = JsonSerializer.Deserialize<List<StudentLesson>>(json1, _options);

            int count = 0, i = 0;
            Student cur = students.ElementAt(i);
            double sum = 0;

            foreach(var sl in studentLessons) 
            {
                if(sl.StudentNumber != cur.StudentNumber) {
                    cur.Avg = (double)(sum/count);
                    if(++i<=students.Count)
                        cur = students.ElementAt(i);
                    count = 0;
                    sum = 0;
                }
                cur.Courses.Add(sl.Lesson, sl.Score);
                sum += sl.Score;
                count++;
            }

            cur.Avg = (double)(sum / count);
            var sorted = students.OrderByDescending(x=>x.Avg);
            
            Student s0 = sorted.ElementAt(0);
            Student s1 = sorted.ElementAt(1);
            Student s2 = sorted.ElementAt(2);
            Console.WriteLine(s0.FirstName + " " + s0.LastName + " " + s0.Avg);
            Console.WriteLine(s1.FirstName + " " + s1.LastName + " " + s1.Avg);
            Console.WriteLine(s2.FirstName + " " + s2.LastName + " " + s2.Avg);
        }
    }
}