﻿using Lab10_Anropa_databasen.Data;
using Lab10_Anropa_databasen.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Lab10_Anropa_databasen
{
    public class MethodCollection

    {
        /*Lägga till kund. Användaren ska kunna lägga till en kund och fylla i värden för alla
         * kolumner utom IDn. iD behöver ni generera en slumpad sträng för (5 tecken lång). 
         * Om användaren inte fyller i ett värde ska null skickas till databasen, 
         * inte en tom sträng.*/

        public static string RandomIdGenerator()
        {
            string rndId = "";  //tom id sträng
            char letter;    //char variabel 
            int rndValue;   //tom variabel

            Random rnd = new Random();  //Random metod som kommer att användas senare 
            for (int i = 0; i < 5; i++)    //for loop som itteeras 5 gr
            {
                rndValue = rnd.Next(0, 26);     //skapar 5 slumpmässiga nummer mellan 0-26
                letter = Convert.ToChar(rndValue + 65); //omvandlar värdet vi får från rndValue till bokstäver genom att lägga till .ToChar
                                                        //och addera 65,
                                                        //eftersom bokstaven A har nr65 osv fram till Z
                rndId = rndId + letter;     //Adderar bokstäverna så att de bildar en slumpmässig sträng av bokstäver 
            }

            return rndId;
        }

        public static void CreateNewClient()
        {
            using (NorthWindDbContext context = new NorthWindDbContext())//skapar instans av Classen Northwind som interagerar med databsen 
            {

                //slumpmässigt generera ett id ¨som omvandlar fyra siffror till bokstäver
                //Användaren lägger in sträng för alla värden
                //spara och uppdatera

                string randomId = RandomIdGenerator();  //anropar metod
                Console.WriteLine("Please enter company name: ");
                string companyName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(companyName))  //while loop som förhindrar användare att skriva tom sträng
                {
                    Console.WriteLine("you hawe to enter a companyname.");  
                    companyName = Console.ReadLine();   //förhindrar oändlig loop
                }
                Console.WriteLine("Please enter contact name: ");
                string input = Console.ReadLine();
                //vilkor som sätter värdet till Null ifall användaren inte anger något värde
                string contactName = string.IsNullOrWhiteSpace(input) ? null : input; //kontrollerar ifall input är tom, : skapar vilkor ifall readline är tom skapas null annars input
                Console.WriteLine("Please enter contact title: ");
                input = Console.ReadLine();
                string contactTitle = string.IsNullOrWhiteSpace(input) ? null : input;
                Console.WriteLine("Please enter adress: ");
                input = Console.ReadLine();
                string adress = string.IsNullOrWhiteSpace(input) ? null : input;
                Console.WriteLine("Please enter city: ");
                input = Console.ReadLine();
                string city = string.IsNullOrWhiteSpace(input) ? null : input;
                Console.WriteLine("Please enter region: ");
                input = Console.ReadLine();
                string region = string.IsNullOrWhiteSpace(input) ? null : input;
                Console.WriteLine("Please enter postal code: ");
                input = Console.ReadLine();
                string postalCode = string.IsNullOrWhiteSpace(input) ? null : input;
                Console.WriteLine("Please enter Country: ");
                input = Console.ReadLine();
                string country = string.IsNullOrWhiteSpace(input) ? null : input;
                Console.WriteLine("Please enter phone number: ");
                input = Console.ReadLine();
                string phone = string.IsNullOrWhiteSpace(input) ? null : input;
                Console.WriteLine("Please enter fax number: ");
                input = Console.ReadLine();
                string fax = string.IsNullOrWhiteSpace(input) ? null : input;

                var newCustomer = new Customer  //ny instans av Customer med variabel namn newCustomer
                {
                    CustomerId = randomId,  
                    CompanyName = companyName,
                    ContactName = contactName,
                    ContactTitle = contactTitle,
                    Address = adress,
                    City = city,
                    Region = region,
                    PostalCode = postalCode,
                    Country = country,
                    Phone = phone,
                    Fax = fax
                };

                context.Customers.Add(newCustomer); //lägger till användare
                context.SaveChanges();  //sparar
                Console.Clear();    //rensar consol
                Console.WriteLine($"{companyName} was added to the database");
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

                     .Select(o => new   //skapar ett nytt object för nedanstående 
                     {
                         o.CompanyName,
                         o.Country,
                         o.Region,
                         o.Phone,
                         OrderCount = o.Orders.Count(), //räknar antalet ordrar
                         ShippedCount = o.Orders.Where(c => c.ShippedDate != null).Count(),//räknar antalet shipeddate som inte är null 
                         NotShippedCount = o.Orders.Where(c => c.ShippedDate == null).Count()//räknar antalet shippeddate som är null 
                     })

                     .ToList();

                if (descOrAesc.ToLower() == "o")    // sorterar svar
                {
                    ordersByCustomers = ordersByCustomers.OrderBy(c => c.CompanyName).ToList();
                }
                else if (descOrAesc.ToLower() == "a")
                {
                    ordersByCustomers = ordersByCustomers.OrderByDescending(c => c.CompanyName).ToList();
                }
                else
                {
                    Console.WriteLine("Please type [O] for ordered or [A] for aescending");
                }
                var result = ordersByCustomers.ToList();

                foreach (var c in ordersByCustomers)    //foreach för att skriva ut information 
                {
                    Console.WriteLine($"{c.CompanyName}--{c.Country}--{c.Region}--{c.Phone} " +
                     $"has made {c.OrderCount} orders. " +
                     $"Total Orders Shipped: {c.ShippedCount}, " +
                     $"with {c.NotShippedCount} orders not shipped:");

                    Console.WriteLine();

                }
            }
        }

        public static void ShowOrdersMadeByClient()
        {
            Console.WriteLine("Please type the Clients Name you want to search for: ");
            string clientSearch = Console.ReadLine();
            Console.WriteLine();
            using (NorthWindDbContext context = new NorthWindDbContext())
            {


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
                if (customerSearch.Count == 0)  //förhindrar bugg
                {
                    Console.Clear();
                    Console.WriteLine($"No client with named {clientSearch} was found");
                }
                else
                {
                    List<Order> ordersByClient = context.Customers //samlar ordrar kopplade till kunder och sparar dem i en Lista
                     .Where(o => o.CompanyName == clientSearch) //söker efter kund
                     .Include(o => o.Orders)    //includerar Orders 
                     .Single()  //hämta första matchningen annars ge undantag
                     .Orders    //hämta orders
                     .ToList(); //omvandlar data och lagrar dem i ordersByClient

                    foreach (var o in ordersByClient)
                    {
                        Console.WriteLine($"has made the following orders: {o.OrderId}, {o.OrderDate}");
                    }
                }

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
            }
        }
    }
}