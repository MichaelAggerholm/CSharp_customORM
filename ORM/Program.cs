using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ORM.models;

namespace ORM
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Database db = new Database();
            SqlConnection conn = Database.connection();
            conn.Open();
            SwitchCase(conn);
        }

        const ConsoleKey keyInfo1 = ConsoleKey.D1;
        const ConsoleKey keyInfo2 = ConsoleKey.D2;
        const ConsoleKey keyInfo3 = ConsoleKey.D3;
        const ConsoleKey keyInfo4 = ConsoleKey.D4;
        const ConsoleKey keyInfo5 = ConsoleKey.Escape;

        private static void SwitchCase(SqlConnection conn)
        {

            while (true)
            {

                Menu.MenuMethod();

                ConsoleKey pressedKey = PressedKey(conn);

                Student student = new Student();

                switch (pressedKey)
                {

                    case keyInfo1:
                        Console.WriteLine("Press --> Enter <-- without any word to exit");
                        Console.Write("Enter Student First_Name > ");
                        student.FirstName = Console.ReadLine();
                        if (student.FirstName != "")
                        {
                            Console.Write("Enter Student Last_Name > ");
                            student.LastName = Console.ReadLine();
                            Console.Write("Enter Student Age > ");
                            student.Age = int.Parse(Console.ReadLine());
                            Console.Write("Enter Student Class_Id > ");
                            student.ClassId = int.Parse(Console.ReadLine());
                            student.Insert();
                            break;
                        }
                        Console.Clear();
                        break;

                    case keyInfo2:
                        Console.WriteLine("Press --> Enter <-- without any word to exit");
                        Console.Write("Specify what StudentID you want to delete > ");
                        string InputId = Console.ReadLine();
                        if (InputId != "")
                        {
                            student.Id = int.Parse(InputId);
                            student.Delete();
                            break;
                        }
                        Console.Clear();
                        break;

                    case keyInfo3:
                        Console.WriteLine("Press --> Enter <-- without any word to exit");
                        Console.Write("What ID do you want to update > ");
                        string UpdateStudent = Console.ReadLine();
                        if (UpdateStudent != "")
                        {
                            student.Id = int.Parse(UpdateStudent);
                            Console.Write("Enter Student First_Name > ");
                            student.FirstName = Console.ReadLine();
                            Console.Write("Enter Student Last_Name > ");
                            student.LastName = Console.ReadLine();
                            Console.Write("Enter Student Age > ");
                            student.Age = int.Parse(Console.ReadLine());
                            Console.Write("Enter Student Class_Id > ");
                            student.ClassId = int.Parse(Console.ReadLine());
                            student.Update();
                            break;
                        }
                        Console.Clear();
                        break;

                    // case keyInfo4:
                    //     Console.Clear();
                    //     Console.WriteLine("");
                    //     Console.WriteLine("All Students\n");
                    //     Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15} {4,-15}", "StudentID:", "First_Name:", "Last_Name:", "Age:", "Class_ID:");
                    //     student.Select();
                    //     break;

                    // case keyInfo5:
                    //     System.Environment.Exit(0);
                    //     break;
                }
            }

            static ConsoleKey PressedKey(SqlConnection conn)
            {

                List<ConsoleKey> key_Array = new List<ConsoleKey> { keyInfo1, keyInfo2, keyInfo3, keyInfo4, keyInfo5};
                ConsoleKey pressed = Console.ReadKey(true).Key;

                do
                {
                    while (!key_Array.Contains(pressed))
                    {
                        Console.Clear();
                        Menu.MenuMethod();
                        Console.WriteLine("Invalid key!");
                        pressed = Console.ReadKey(true).Key;
                    }
                    //ConsoleKey pressed = Console.ReadKey(true).Key;
                    if (pressed is keyInfo1 or keyInfo2 or keyInfo3 or keyInfo4 or keyInfo5)
                        return pressed;
                    Console.Clear();
                } while (true);
            }
        }
    }
}