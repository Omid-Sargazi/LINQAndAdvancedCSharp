using System.Reflection.Metadata;

namespace AlgorithemInCSharp.RpositoryPattern
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public interface IStudentRepository
    {
        bool Delete(Student student);
        Student GetStudent(int Id);
        bool Update(int Id);
        void CreateStudent(Student student);
        void GetAll();
    }

    public class StudentRepository : IStudentRepository
    {

        private List<Student> _students;

        public StudentRepository()
        {
            _students = new();
        }

        public void CreateStudent(Student student)
        {
            _students.Add(student);
        }

        public bool Delete(Student student)
        {
            if (_students.Contains(student))
            {
                _students.Remove(student);
                return true;
            }
            else
            {
                throw new FileNotFoundException("Student is not in detabase");
                return false;
            }
        }

        public void GetAll()
        {
            foreach(var student in _students)
            {
                Console.WriteLine(student);
            }
        }

        public Student GetStudent(int Id)
        {
            throw new NotImplementedException();
        }

        public bool Update(int Id)
        {
            throw new NotImplementedException();
        }
    }
}