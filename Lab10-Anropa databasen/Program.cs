using Lab10_Anropa_databasen.Data;
using Lab10_Anropa_databasen.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab10_Anropa_databasen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Hämta alla kunder. Visa företagsnamn, land, region, telefonnummer och antal ordrar de har
             Sortera på företagsnamn. Användaren ska kunna välja stigande eller fallande ordning.*/


            //substring for random id 
            using (NorthWindDbContext context = new NorthWindDbContext())
            {
                Console.WriteLine("Do you want the information in an ordered list or an descending list? if so type o for ordered or a for ascending ");
                string descOrAesc = Console.ReadLine();

                var ordersByCustomers = context.Customers
                     .Select(c => new
                     {
                         c.CompanyName,
                         c.Country,
                         c.Region,
                         c.Phone,
                         c.Orders
                     });

                //.OrderByDescending(c => c.CompanyName)

                //.ToList();
                if (descOrAesc.ToLower() == "o")
                {
                    ordersByCustomers = ordersByCustomers.OrderBy(c => c.CompanyName);
                }
                else if (descOrAesc.ToLower() == "a")
                {
                    ordersByCustomers = ordersByCustomers.OrderByDescending(c => c.CompanyName);
                }
                else
                {
                    Console.WriteLine("Please type o for ordered or a fro aescending");
                }
                var result=ordersByCustomers.ToList();


                foreach (var o in result)
                {
                    Console.WriteLine($"{o.CompanyName} has made {o.Orders.Count()}");

                }

            }



            //foreach (var o in ordersByCustomers)
            //{
            //    Console.WriteLine($"{o.Orders.Count()}");

            //}


        }
    }
}
