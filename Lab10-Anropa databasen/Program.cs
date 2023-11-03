using Lab10_Anropa_databasen.Data;
using Lab10_Anropa_databasen.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab10_Anropa_databasen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ///substring for random id 
            //Actions.OrderByClient();

            //Användaren ska kunna välja en kund i listan.
            //Alla fält (utom IDn) för kunden ska då visas samt en lista på alla ordrar kunden har gjort.
            using (NorthWindDbContext context = new NorthWindDbContext()) 
            {

                var ordersByCustomers = context.Customers
                     .Select(c => new
                     {
                         c.CompanyName,
                         c.ContactName,
                         c.ContactTitle,
                         c.Address,
                         c.City,
                         c.Region,
                         c.PostalCode,
                         c.Country,
                         c.Phone,
                         c.Fax                         
                     });
            }

        }
    }
}
