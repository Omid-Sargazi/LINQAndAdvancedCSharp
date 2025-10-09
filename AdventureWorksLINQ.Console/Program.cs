using AdventureWorksLINQ.Console.DependancyInjection;
using AdventureWorksLINQ.Console.EFCore;
using AdventureWorksLINQ.Console.EFCore.EFCorePerformanceTuning;
using AdventureWorksLINQ.Console.Generics;
using AdventureWorksLINQ.Console.IqueryableProblem;
using AdventureWorksLINQ.Console.Product;
using AdventureWorksLINQ.Console.QueryExample;
using AdventureWorksLINQ.Console.QueryExamples;
using AdventureWorksLINQ.Console.Reflection;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        // var optionsBuilder = new DbContextOptionsBuilder<EFCoreDbcontext>();
        // optionsBuilder.UseInMemoryDatabase("UniversityDB");

        // using (var context = new EFCoreDbcontext(optionsBuilder.Options))
        // {
        //     var student1 = new Student { Name = "Ali" };
        //     var student2 = new Student { Name = "Sara" };
        //     var course1 = new Course { Title = "Mathematics" };
        //     var course2 = new Course { Title = "Physics" };

        //     context.Students.AddRange(student1, student2);
        //     context.Courses.AddRange(course1, course2);

        //     context.StudentCourses.AddRange(
        //         new StudentCourse { Student = student1, Course = course1, EnrollmentDate = DateTime.Now },
        //         new StudentCourse { Student = student1, Course = course2, EnrollmentDate = DateTime.Now },
        //         new StudentCourse { Student = student2, Course = course2, EnrollmentDate = DateTime.Now },
        //         new StudentCourse { Student = student2, Course = course1, EnrollmentDate = DateTime.Now }
        //     );

        //     context.SaveChanges();

        //     var studentsWithMultipleCourses = context.Students
        //     .Include(s => s.StudentCourses)
        //     .ThenInclude(sc => sc.Course)
        //     .Where(s => s.StudentCourses.Count > 1)
        //     .Select(s => new
        //     {
        //         s.Name,
        //         Courses = s.StudentCourses.Select(sc => sc.Course.Title).ToList()
        //     }).ToList();

        //     Console.WriteLine($"Students with more than one course:");
        //     foreach (var student in studentsWithMultipleCourses)
        //     {
        //         Console.WriteLine($"Student: {student.Name}, Courses: {string.Join(", ", student.Courses)}");
        //     }
        // }

        // RunContextOfLibrary.Run();

        // SqlBook.Run();

        // Problem.Run();
        // DependencyInjection.Run();


        // Queries.Run();

        // DIClient.Run();

        // DIClient2.Run();

        // Box<int>.Run(); 

        System.Console.WriteLine("Helloooooo");

        Iqueryable1.Run();
    }
}