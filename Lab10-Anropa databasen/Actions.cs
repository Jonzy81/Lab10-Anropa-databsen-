using Lab10_Anropa_databasen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10_Anropa_databasen
{
    internal class Actions
    {

        public static void OrderByClient()
        {
            /*Hämta alla kunder. Visa företagsnamn, land, region, telefonnummer och antal ordrar de har
                Sortera på företagsnamn. Användaren ska kunna välja stigande eller fallande ordning.*/


            /
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
                var result = ordersByCustomers.ToList();


                foreach (var o in result)
                {
                    Console.WriteLine($"{o.CompanyName}--{o.Country}--{o.Region}{o.Phone} has made {o.Orders.Count()} orders");

                }

            }



            //foreach (var o in ordersByCustomers)
            //{
            //    Console.WriteLine($"{o.Orders.Count()}");

            //}

        }
    }
}