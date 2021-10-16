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
            SqlConnection conn = Database.connection();
            conn.Open();
            programFlow(conn);
        }
        
        const ConsoleKey key1 = ConsoleKey.D1;
        const ConsoleKey key2 = ConsoleKey.D2;
        const ConsoleKey key3 = ConsoleKey.D3;
        const ConsoleKey key4 = ConsoleKey.D4;
        const ConsoleKey key5 = ConsoleKey.Escape;

        private static void programFlow(SqlConnection conn)
        {

            while (true)
            {

                MenuSelections();
                
                ConsoleKey pressedKey = PressedKey(conn);
                Product product = new Product();

                switch (pressedKey)
                {

                    case key1:
                        Console.Write("Product title: ");
                        product.Title = Console.ReadLine();
                        if (product.Title != "")
                        {
                            Console.Write("Product description: ");
                            product.Description = Console.ReadLine();
                            if (product.Description != "")
                            {
                                Console.Write("Product Quantity: ");
                                product.Quantity = 
                                    int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                                product.Insert();
                            }
                        }
                        Console.Clear();
                        break;

                    case key2:
                        Console.Write("Product id to update: ");
                        string updateProductId = Console.ReadLine();
                        if (updateProductId != "")
                        {
                            product.Id = 
                                int.Parse(updateProductId ?? throw new InvalidOperationException());
                            Console.Write("Product title: ");
                            product.Title = Console.ReadLine();
                            if (product.Title != "")
                            {
                                Console.Write("Product description: ");
                                product.Description = Console.ReadLine();
                                if (product.Description != "")
                                {
                                    Console.Write("Product Quantity: ");
                                    product.Quantity = 
                                        int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                                    product.Update();
                                }
                            }
                        }
                        Console.Clear();
                        break;

                    case key3:
                        Console.Write("Product id to delete: ");
                        string deleteProductId = Console.ReadLine();
                        if (deleteProductId != "")
                        {
                            product.Id = 
                                int.Parse(deleteProductId ?? throw new InvalidOperationException());
                            product.Delete();
                        }
                        Console.Clear();
                        break;

                    case key4:
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("All products:\n");
                        Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15}", "ID:", "Title:", "Description:", "Quantity:");
                        product.Display();
                        Console.WriteLine("");
                        Console.WriteLine("Press foobar to return."); 
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case key5:
                        Environment.Exit(0);
                        break;
                }
            }

            static void MenuSelections()
            {
                Console.WriteLine("Menu\n" +
                                  "1 to insert a product\n" +
                                  "2 to Update a product\n" +
                                  "3 to Delete a product\n" +
                                  "4 to display all products\n" +
                                  "Esc to exit\n");
            }

            static ConsoleKey PressedKey(SqlConnection conn)
            {

                List<ConsoleKey> key_Array = new List<ConsoleKey> { key1, key2, key3, key4, key5};
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
                    
                    if (pressed is key1 or key2 or key3 or key4 or key5)
                        return pressed;
                    Console.Clear();
                } while (true);
            }
        }
    }
}