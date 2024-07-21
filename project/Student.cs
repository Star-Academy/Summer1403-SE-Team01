namespace ScoreBoard
{
    public class Student : IComparable<Student>
    {
        public int StudentNumber {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public double Average {get; set;}

        public int CompareTo(Student? other)
        {
            return other.Average.CompareTo(this.Average);
        }

        public override string ToString()
        {
            return "firsName: " + this.FirstName + 
                "\tlastName: " + this.LastName + 
                "\tavg: " + this.Average;
        }
    }
}
