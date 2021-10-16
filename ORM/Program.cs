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

                MenuSelections();

                ConsoleKey pressedKey = PressedKey(conn);

                Product product = new Product();

                switch (pressedKey)
                {

                    case keyInfo1:
                        Console.WriteLine("Press --> Enter <-- without any word to exit");
                        Console.Write("Enter product First_Name > ");
                        product.Title = Console.ReadLine();
                        if (product.Title != "")
                        {
                            Console.Write("Enter product Last_Name > ");
                            product.Description = Console.ReadLine();
                            Console.Write("Enter product Quantity > ");
                            product.Quantity = int.Parse(Console.ReadLine());
                            product.Insert();
                            break;
                        }
                        Console.Clear();
                        break;

                    case keyInfo2:
                        Console.WriteLine("Press --> Enter <-- without any word to exit");
                        Console.Write("Specify what productID you want to delete > ");
                        string InputId = Console.ReadLine();
                        if (InputId != "")
                        {
                            product.Id = int.Parse(InputId);
                            product.Delete();
                            break;
                        }
                        Console.Clear();
                        break;

                    case keyInfo3:
                        Console.WriteLine("Press --> Enter <-- without any word to exit");
                        Console.Write("What ID do you want to update > ");
                        string Updateproduct = Console.ReadLine();
                        if (Updateproduct != "")
                        {
                            product.Id = int.Parse(Updateproduct);
                            Console.Write("Enter product First_Name > ");
                            product.Title = Console.ReadLine();
                            Console.Write("Enter product Last_Name > ");
                            product.Description = Console.ReadLine();
                            Console.Write("Enter product Quantity > ");
                            product.Quantity = int.Parse(Console.ReadLine());
                            product.Update();
                            break;
                        }
                        Console.Clear();
                        break;

                    // case keyInfo4:
                    //     Console.Clear();
                    //     Console.WriteLine("");
                    //     Console.WriteLine("All products\n");
                    //     Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15} {4,-15}", "productID:", "First_Name:", "Last_Name:", "Quantity:", "Class_ID:");
                    //     product.Select();
                    //     break;

                    case keyInfo5:
                        Environment.Exit(0);
                        break;
                }
            }
            
            static void MenuSelections()
            {
                Console.WriteLine("Menu\n" +
                                  "Press 1 Insert product.\n" +
                                  "Press 2 Delete product.\n" +
                                  "Press 3 Update product.\n" +
                                  "Press 4 ShowAll products.\n" +
                                  "Press Esc to exit the Program.\n");
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
                        MenuSelections();
                        Console.WriteLine("Invalid key!");
                        pressed = Console.ReadKey(true).Key;
                    }
                    
                    if (pressed is keyInfo1 or keyInfo2 or keyInfo3 or keyInfo4 or keyInfo5)
                        return pressed;
                    Console.Clear();
                } while (true);
            }
        }
    }
}