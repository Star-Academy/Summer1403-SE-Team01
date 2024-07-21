/*P1*/
using System;
using System.Text.Json;
class Student
{
  private int _StudentNumber;
  public int StudentNumber   
  {
    get { return _StudentNumber; }
    set { _StudentNumber = value; }
  }
  private string _FirstName;
  public string FirstName   
  {
    get { return _FirstName; }
    set { _FirstName = value; }
  }
  private string _LastName;
  public string LastName
  {
    get { return _LastName; }
    set { _LastName = value; }
  }
}

class Scores
{
  private int _StudentNumber;
  public int StudentNumber   
  {
    get { return _StudentNumber; }
    set { _StudentNumber = value; }
  }
  private string _Lesson;
  public string Lesson   
  {
    get { return _Lesson; }
    set { _Lesson = value; }
  }
  private double _Score;
  public double Score
  {
    get { return _Score; }
    set { _Score = value; }
  }  
}

class Result
{
    private int _StudentNumber;
    public int StudentNumber   
    {
        get { return _StudentNumber; }
        set { _StudentNumber = value; }
    }

     private double _Avg;
  public double Avg   
  {
    get { return _Avg; }
    set { _Avg = value; }
  }
}
class Program 
{
    public static void Main(String[] args)
    {
        
        JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        var json_students = File.ReadAllText("./files/students.json");
        List<Student> students = JsonSerializer.Deserialize<List<Student>>(json_students, _options);
        Console.WriteLine("**************************************",students.ElementAt(0).FirstName);
        var json_scores = File.ReadAllText("./files/scores.json");
        List<Scores> scores = JsonSerializer.Deserialize<List<Scores>>(json_scores, _options);

        
        int cur = 1, count = 0;
        double sum = 0;
        List<Result> res = new List<Result>();
        foreach(var s in scores)
        {
            if(s.StudentNumber == cur) 
            {
                sum += s.Score;
                count++;
            
            } 
            else 
            {
                res.Add(new Result{StudentNumber = s.StudentNumber, Avg = (double)(sum/count)});
                count = 0;
                sum = 0;
                cur++;
            }
        
        }

        //List<Result> ordered = res.OrderByDescending<Result>(x => x.Avg);
        var ordered = res.OrderByDescending(x => x.Avg);

        var s1 = students.Single(s=>s.StudentNumber==ordered.ElementAt(0).StudentNumber);
        Console.WriteLine(ordered.ElementAt(0).Avg + " " + s1.FirstName + " " + s1.LastName);
        var s2 = students.Single(s=>s.StudentNumber==ordered.ElementAt(1).StudentNumber);
        Console.WriteLine(ordered.ElementAt(1).Avg + " " + s2.FirstName + " " + s2.LastName);
        var s3 = students.Single(s=>s.StudentNumber==ordered.ElementAt(2).StudentNumber);
        Console.WriteLine(ordered.ElementAt(2).Avg + " " + s3.FirstName + " " + s3.LastName);
        
    }
}