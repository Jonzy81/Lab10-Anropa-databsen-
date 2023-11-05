using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10_Anropa_databasen
{
    public class Meny
    {
        public static void UserChoice()
        {
            int choice=-1;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t\t Main Meny:\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t\t----------------------------------------\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "\t\t Please chose from the List below:\n" +
                    "\t\t [1] View all clients \n" +
                    "\t\t [2] View clients orderdetails \n" +
                    "\t\t [3] Add new client to list \n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t\t----------------------------------------\n");
                Console.ResetColor();

                
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();//Väljaren gör sitt val 
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("\n\t" + e.Message);
                    Console.WriteLine("\n\tPlease use a number between 1 - 3 in from the meny ");       //Väljaren får ett felmeddelande vid fel val
                    continue;
                }
                if (choice >=1 && choice <=3)
                switch (choice)
                {
                    case 1:
                            MethodCollection.ShowAllClients();
                        break;
                    case 2: break;
                    case 3: break;


                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please use a number from the meny");
                    Console.WriteLine();
                }
            }

        }
       
    }
}
