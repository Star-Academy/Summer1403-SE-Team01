using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScoreBoard
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ReadJson rj = new ReadJson();
            string studentsJsonPath = "./files/students.json", 
                   scoresJsonPath = "./files/scores.json";

            List<Student> students = await rj.ReadAsync<Student>(studentsJsonPath);
            List<StudentScore> studentScores = await rj.ReadAsync<StudentScore>(scoresJsonPath);
            
            ScoreOperator sop = new ScoreOperator(students, studentScores);
            sop.calculateAveg();
            sop.sortScores();
            var sorted = sop.takeFirst(3);

            foreach(var s in sorted)
                Console.WriteLine(s.ToString());
        }
    }
}
