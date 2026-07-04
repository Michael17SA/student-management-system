using System.Text.RegularExpressions;

namespace StudentManagementSystem;

internal abstract class Person
{
    protected Person(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; }
    public string Name { get; }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}, Name: {Name}");
    }
}

internal sealed class Student : Person
{
    public Student(string id, string name, int[] grades)
        : base(id, name)
    {
        Grades = grades;
    }

    public int[] Grades { get; }

    public double GetAverage()
    {
        return Grades.Average();
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}, Name: {Name}, Grades: {string.Join(", ", Grades)}, Average: {GetAverage():F2}");
    }
}

internal static partial class Program
{
    private static readonly List<Student> Students = [];

    private static void Main()
    {
        int choice;

        do
        {
            Console.WriteLine("\n--- Student Management System ---");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View All Students");
            Console.WriteLine("3. Search Student");
            Console.WriteLine("4. Remove Student");
            Console.WriteLine("5. Total Students");
            Console.WriteLine("6. Exit");
            Console.Write("Choose: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddStudent();
                    break;
                case 2:
                    ShowStudents();
                    break;
                case 3:
                    SearchStudent();
                    break;
                case 4:
                    RemoveStudent();
                    break;
                case 5:
                    Console.WriteLine($"Total Students: {Students.Count}");
                    break;
                case 6:
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        } while (choice != 6);
    }

    private static void AddStudent()
    {
        try
        {
            string id = ReadStudentId();

            if (Students.Any(student => student.Id.Equals(id, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("A student with this ID already exists.");
                return;
            }

            string name = ReadRequiredText("Enter Student Name: ");
            int[] grades = ReadGrades(3);

            Students.Add(new Student(id, name, grades));
            Console.WriteLine("Student added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void ShowStudents()
    {
        if (Students.Count == 0)
        {
            Console.WriteLine("No students found.");
            return;
        }

        foreach (Student student in Students)
        {
            student.DisplayInfo();
        }
    }

    private static void SearchStudent()
    {
        string id = ReadRequiredText("Enter ID to search: ");
        Student? found = Students.Find(student => student.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

        if (found is not null)
        {
            found.DisplayInfo();
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    private static void RemoveStudent()
    {
        string id = ReadRequiredText("Enter ID to remove: ");
        Student? found = Students.Find(student => student.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

        if (found is null)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        Students.Remove(found);
        Console.WriteLine("Student removed successfully.");
    }

    private static string ReadStudentId()
    {
        string id = ReadRequiredText("Enter Student ID (e.g. S12345): ").ToUpperInvariant();

        if (!StudentIdPattern().IsMatch(id))
        {
            throw new FormatException("Invalid Student ID format. Use S followed by 5 digits, for example S12345.");
        }

        return id;
    }

    private static int[] ReadGrades(int count)
    {
        int[] grades = new int[count];

        for (int index = 0; index < count; index++)
        {
            grades[index] = ReadGrade($"Enter grade {index + 1}: ");
        }

        return grades;
    }

    private static int ReadGrade(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            if (int.TryParse(Console.ReadLine(), out int grade) && grade is >= 0 and <= 100)
            {
                return grade;
            }

            Console.WriteLine("Invalid grade. Enter a number between 0 and 100.");
        }
    }

    private static string ReadRequiredText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? value = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }

            Console.WriteLine("This field cannot be empty.");
        }
    }

    [GeneratedRegex("^S\\d{5}$")]
    private static partial Regex StudentIdPattern();
}
