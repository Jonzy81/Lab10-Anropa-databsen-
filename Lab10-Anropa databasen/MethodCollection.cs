using Lab10_Anropa_databasen.Data;
using Lab10_Anropa_databasen.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10_Anropa_databasen
{
    public class MethodCollection

    {
        /*Lägga till kund. Användaren ska kunna lägga till en kund och fylla i värden för alla
         * kolumner utom IDn. iD behöver ni generera en slumpad sträng för (5 tecken lång). 
         * Om användaren inte fyller i ett värde ska null skickas till databasen, 
         * inte en tom sträng.*/
        public static void CreateNewClient()
        {
            using (NorthWindDbContext db = new NorthWindDbContext())
            {

                //slumpmässigt generera ett id ¨som omvandlar fyra siffror till bokstäver
                //Användaren lägger in sträng för alla värden
                //spara och uppdatera


                string rndId = "";  //tom id sträng
                char letter;    //char variabel 
                int rndValue;   //tom variabel
                  
                Random rnd = new Random();  //Random metod som kommer att användas senare 
                for (int i = 0; i <= 5; i++)    //for loop som itteeras 5 gr
                {
                    rndValue = rnd.Next(0, 26);     //skapar 5 slumpmässiga nummer mellan 0-26
                    letter = Convert.ToChar(rndValue + 65); //omvandlar värdet vi får från rndValue till bokstäver genom att lägga till .ToChar
                                                            //och addera 65,
                                                            //eftersom bokstaven A har nr65 osv fram till Z
                    rndId = rndId + letter;     //Adderar bokstäverna så att de bildar en slumpmässig sträng av bokstäver 
                }
                

                Console.WriteLine("Please enter company name: ");
                string companyName=Console.ReadLine();
                Console.WriteLine("Please enter contact name: ");
                string contactName=Console.ReadLine();
                Console.WriteLine("Please enter contact title: ");
                string contactTitle=Console.ReadLine();
                Console.WriteLine("Please enter adress: ");
                string adress= Console.ReadLine();
                Console.WriteLine("Please enter city: ");
                string city=Console.ReadLine();
                Console.WriteLine("Please enter region: ");
                string region= Console.ReadLine();
                Console.WriteLine("Please enter postal code: ");
                string postalCode=Console.ReadLine();
                Console.WriteLine("Please enter Country: ");
                string country= Console.ReadLine();
                Console.WriteLine("Please enter phone number: ");
                int phone = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Please enter fax number: ");
                int fax= Convert.ToInt32(Console.ReadLine());


            }
        }


        public static void ShowAllClients()
        {
            /*Hämta alla kunder. Visa företagsnamn, land, region, telefonnummer och antal ordrar de har
                Sortera på företagsnamn. Användaren ska kunna välja stigande eller fallande ordning.*/

            using (NorthWindDbContext context = new NorthWindDbContext())
            {



                Console.WriteLine("Do you want the information in an ordered list or an descending list? if so type o for ordered or a for ascending ");
                string descOrAesc = Console.ReadLine();
                Console.WriteLine();

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
                    Console.WriteLine("Please type [O] for ordered or [A] for aescending");
                }
                var result = ordersByCustomers.ToList();

                int listNumber = 1;
                foreach (var o in result)
                {
                    Console.WriteLine($"{listNumber}:--{o.CompanyName}--{o.Country}--{o.Region}--{o.Phone} has made {o.Orders.Count()} orders");
                    listNumber++;
                    Console.WriteLine();

                }
            }
        }

        public static void ShowOrdersMadeByClient()
        {
            Console.WriteLine("Please type the Clients Name you want to search for: ");
            string clientSearch=Console.ReadLine();
            Console.WriteLine();
            using (NorthWindDbContext context = new NorthWindDbContext())
            {
                List<Order> ordersByClient = context.Customers
                     .Where(o => o.CompanyName == clientSearch)
                     
                     .Include(o => o.Orders)
                     .Single()
                     .Orders
                     .ToList();

                var customerSearch = context.Customers
                    .Where(c => c.CompanyName == clientSearch)
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
                        c.Fax,

                    })
                    .ToList();

                foreach (var o in customerSearch)
                {
                    //Wrote all this code instead of just Console.Writeline(o) just to not hawe {} in the console 
                    Console.WriteLine($"Company name: {o.CompanyName}," +
                        $" Contact name: {o.ContactName}," +
                        $" Contact title: {o.ContactTitle}," +
                        $" Adress: {o.Address}," +
                        $" City: {o.City}," +
                        $" Region: {o.Region}," +
                        $" Postal code: {o.PostalCode}," +
                        $" Country: {o.Country}," +
                        $" Phone: {o.Phone}," +
                        $" Fax: {o.Fax}");
                }

                foreach (var o in ordersByClient)
                {
                    Console.WriteLine($"has made the following orders: {o.OrderId}, {o.OrderDate}");
                }
                
            }

        }



            //foreach (var o in ordersByCustomers)
            //{
            //    Console.WriteLine($"{o.Orders.Count()}");

            //}

        
       
        

    }
}