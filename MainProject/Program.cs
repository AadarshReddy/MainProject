using System;
using System.Collections.Generic;
using System.IO;

class Teacher
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Class { get; set; }
    public string Section { get; set; }

    public override string ToString()
    {
        return $"{ID}\t{Name}\t{Class}\t{Section}";
    }
}

class TeacherManager
{
    private const string FilePath = "teacher_records.txt";

    public static List<Teacher> ReadTeachersFromFile()
    {
        List<Teacher> teachers = new List<Teacher>();
        try
        {
            if (File.Exists(FilePath))
            {
                string[] lines = File.ReadAllLines(FilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('\t');
                    if (parts.Length == 4)
                    {
                        Teacher teacher = new Teacher
                        {
                            ID = int.Parse(parts[0]),
                            Name = parts[1],
                            Class = parts[2],
                            Section = parts[3]
                        };
                        teachers.Add(teacher);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }
        return teachers;
    }

    public static void WriteTeachersToFile(List<Teacher> teachers)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                foreach (Teacher teacher in teachers)
                {
                    writer.WriteLine(teacher.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }
}

class Program
{
    static void Main()
    {
        List<Teacher> teachers = TeacherManager.ReadTeachersFromFile();

        while (true)
        {
            Console.WriteLine("1. View Teachers");
            Console.WriteLine("2. Add Teacher");
            Console.WriteLine("3. Update Teacher");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        ViewTeachers(teachers);
                        break;
                    case 2:
                        AddTeacher(teachers);
                        break;
                    case 3:
                        UpdateTeacher(teachers);
                        break;
                    case 4:
                        TeacherManager.WriteTeachersToFile(teachers);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }

    static void ViewTeachers(List<Teacher> teachers)
    {
        Console.WriteLine("ID\tName\tClass\tSection");
        foreach (Teacher teacher in teachers)
        {
            Console.WriteLine(teacher.ToString());
        }
    }

    static void AddTeacher(List<Teacher> teachers)
    {
        Console.Write("Enter ID: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Enter Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Class: ");
        string classInput = Console.ReadLine();

        Console.Write("Enter Section: ");
        string section = Console.ReadLine();

        Teacher newTeacher = new Teacher { ID = id, Name = name, Class = classInput, Section = section };
        teachers.Add(newTeacher);

        Console.WriteLine("Teacher added successfully!");
    }

    static void UpdateTeacher(List<Teacher> teachers)
    {
        Console.Write("Enter the ID of the teacher to update: ");
        int idToUpdate = int.Parse(Console.ReadLine());

        Teacher teacherToUpdate = teachers.Find(t => t.ID == idToUpdate);
        if (teacherToUpdate != null)
        {
            Console.Write("Enter new Name: ");
            teacherToUpdate.Name = Console.ReadLine();

            Console.Write("Enter new Class: ");
            teacherToUpdate.Class = Console.ReadLine();

            Console.Write("Enter new Section: ");
            teacherToUpdate.Section = Console.ReadLine();

            Console.WriteLine("Teacher updated successfully!");
        }
        else
        {
            Console.WriteLine($"Teacher with ID {idToUpdate} not found.");
        }
    }
}
