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

        private double _avergae;
        public double Average
        {
            get { return _avergae; }
            set { _avergae = value; }
        }
    }


    public class StudentScore
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

            var studentsJson = File.ReadAllText("./files/students.json");
            List<Student> students = JsonSerializer.Deserialize<List<Student>>(studentsJson, _options);

            var scoreJson = File.ReadAllText("./files/scores.json");
            List<StudentScore> studentScores = JsonSerializer.Deserialize<List<StudentScore>>(scoreJson, _options);

            int count = 0, i = 0;
            Student cur = students.ElementAt(i);
            double sum = 0;

            foreach(var ss in studentScores) 
            {
                if(ss.StudentNumber != cur.StudentNumber) {
                    cur.Average = (double)(sum/count);
                    if(++i<=students.Count)
                        cur = students.ElementAt(i);
                    count = 0;
                    sum = 0;
                }
                cur.Courses.Add(ss.Lesson, ss.Score);
                sum += ss.Score;
                count++;
            }
            cur.Average = (double)(sum / count);
            
            var sorted = students.OrderByDescending(x=>x.Average).Take(3);
            foreach(var s in sorted)
                Console.WriteLine($"name: {s.FirstName} {s.LastName}, avg: {s.Average}");
            
        }
    }
}