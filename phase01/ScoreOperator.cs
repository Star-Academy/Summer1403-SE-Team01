namespace ScoreBoard
{
    public class ScoreOperator 
    {
        
        private List<Student> _students;
        private List<StudentScore> _studentScores;

        public ScoreOperator(List<Student> students, List<StudentScore> studentScores) 
        {
            _students = students;
            _studentScores = studentScores;
        }

        public void calculateAveg()
        {
            _students
                .ForEach(s=>s.Average=_studentScores
                .Where(ss=>ss.StudentNumber==s.StudentNumber)
                .Average(x=>x.Score));
        }

        public void sortScores()
         {
            _students.Sort();
        }

        public IEnumerable<Student> takeFirst(int n) 
        {
            return _students.Take(n);
        }
    }
}