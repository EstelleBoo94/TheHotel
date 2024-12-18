using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHotel.Displays.MenuFolder;
using TheHotel.Displays.TablesFolder;
using TheHotel.Models;

namespace TheHotel.Services
{
    public class CustomerServices : ICrudOperations<CustomerList>
    {

        public void Create(CustomerList customers)
        {
            CustomersMenu customersMenu = new CustomersMenu();

            List<string> menuOptions = new List<string>
            {
            "KundId", "Förnamn", "Efternamn", "Personnummer",
                "Telefonummer", "Email", "Gatuadress", "Postnummer", "Stad", "Land", "Spara kund"
            };

            int selection = 0;
            bool inMenu = true;

            int customerId = 0;
            string firstName = null;
            string lastName = null;
            int ssn = 0;
            int phoneNumber = 0;
            string email = null;
            string streetAdress = null;
            int postalCode = 0;
            string city = null;
            string country = null;

            var contentList = new List<string> { " ", " ", " ", " ", " ", " ", " ", " ", " ", " " };

            while (inMenu == true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Välj alternativ med piltangenterna:\n");
                Console.ResetColor();

                for (int i = 0; i < menuOptions.Count; i++)
                {
                    if (i == selection)
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(menuOptions[i]);

                    Console.ResetColor();
                }

                if (selection == menuOptions.Count)
                {
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine("Tillbaka");
                Console.ResetColor();

                Console.WriteLine("\n");


                CustomerTableClass.DisplayCustomerTable(contentList);

                var keyInput = Console.ReadKey(true);


                if (keyInput.Key == ConsoleKey.UpArrow)
                {
                    selection--;
                    if (selection < 0)
                    {
                        selection = menuOptions.Count;
                    }
                }

                else if (keyInput.Key == ConsoleKey.DownArrow)
                {
                    selection++;
                    if (selection > menuOptions.Count)
                    {
                        selection = 0;
                    }
                }


                else if (keyInput.Key == ConsoleKey.Enter)
                {
                    if (selection == menuOptions.Count)
                    {
                        inMenu = false;
                    }
                    else if (selection == 0)
                    {
                        Console.WriteLine("Ange kundId:");
                        customerId = int.Parse(Console.ReadLine());
                        contentList.Insert(0, customerId.ToString());
                    }
                    else if (selection == 1)
                    {
                        Console.WriteLine("Ange förnamn:");
                        firstName = Console.ReadLine();
                        contentList.Insert(1, firstName);
                    }
                    else if (selection == 2)
                    {
                        Console.WriteLine("Ange efternamn:");
                        lastName = Console.ReadLine();
                        contentList.Insert(2, lastName);
                    }
                    else if (selection == 3)
                    {
                        Console.WriteLine("Ange personnummer:");
                        ssn = int.Parse(Console.ReadLine());
                        contentList.Insert(3, ssn.ToString());
                    }
                    else if (selection == 4)
                    {
                        Console.WriteLine("Ange telefonnummer:");
                        phoneNumber = int.Parse(Console.ReadLine());
                        contentList.Insert(4, phoneNumber.ToString());
                    }
                    else if (selection == 5)
                    {
                        Console.WriteLine("Ange mailadress:");
                        email = Console.ReadLine();
                        contentList.Insert(5, email);
                    }
                    else if (selection == 6)
                    {
                        Console.WriteLine("Ange gatuadress:");
                        streetAdress = Console.ReadLine();
                        contentList.Insert(6, streetAdress);
                    }
                    else if (selection == 7)
                    {
                        Console.WriteLine("Ange postnummer:");
                        postalCode = int.Parse(Console.ReadLine());
                        contentList.Insert(7, postalCode.ToString());
                    }
                    else if (selection == 8)
                    {
                        Console.WriteLine("Ange stad:");
                        city = Console.ReadLine();
                        contentList.Insert(8, city);
                    }
                    else if (selection == 9)
                    {
                        Console.WriteLine("Ange land:");
                        country = Console.ReadLine();
                        contentList.Insert(9, country);
                    }
                    else if (selection == 10)
                    {
                        customers.Customers.Add(new Customer(customerId, firstName, lastName, ssn, phoneNumber, email,
                            streetAdress, postalCode, city, country));
                        Console.WriteLine("Kunden är registrerad.\nTryck valfri tangent för att fortsätta.");
                        Console.ReadKey();
                        customersMenu.ShowCustomerMenu(customers);
                    }
                }

            }

            customersMenu.ShowCustomerMenu(customers);
        }

        public void Read(CustomerList customers)
        {
            Console.Clear();
            CustomerTableClass.DisplayAllCustomersTable(customers);
            Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
            Console.ReadKey();
        }

        public void Update(CustomerList customers)
        {
            Console.Clear();
            Console.WriteLine("Ange kundId för kunden du vill uppdatera:");
            int findId = int.Parse(Console.ReadLine());

            var findCustomer = customers.Customers.FirstOrDefault(c => c.CustomerId == findId);

            if (findCustomer != null)
            {

                CustomersMenu customersMenu = new CustomersMenu();

                List<string> menuOptions = new List<string>
                {
                    "KundId", "Förnamn", "Efternamn", "Personnummer",
                    "Telefonummer", "Email", "Gatuadress", "Postnummer", "Stad", "Land", "Spara kund"
                };

                int selection = 0;
                bool inMenu = true;

                int customerId = 0;
                string firstName = null;
                string lastName = null;
                int ssn = 0;
                int phoneNumber = 0;
                string email = null;
                string streetAdress = null;
                int postalCode = 0;
                string city = null;
                string country = null;

                var contentList = new List<string> { findCustomer.CustomerId.ToString(), findCustomer.FirstName, findCustomer.LastName, 
                    findCustomer.SSN.ToString(), findCustomer.PhoneNumber.ToString(), findCustomer.Email, findCustomer.StreetAddress,
                    findCustomer.PostalCode.ToString(), findCustomer.City, findCustomer.Country };

                while (inMenu == true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Välj alternativ med piltangenterna:\n");
                    Console.ResetColor();

                    for (int i = 0; i < menuOptions.Count; i++)
                    {
                        if (i == selection)
                        {
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }

                        Console.WriteLine(menuOptions[i]);

                        Console.ResetColor();
                    }

                    if (selection == menuOptions.Count)
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine("Tillbaka");
                    Console.ResetColor();

                    Console.WriteLine("\n");


                    CustomerTableClass.DisplayCustomerTable(contentList);

                    var keyInput = Console.ReadKey(true);


                    if (keyInput.Key == ConsoleKey.UpArrow)
                    {
                        selection--;
                        if (selection < 0)
                        {
                            selection = menuOptions.Count;
                        }
                    }

                    else if (keyInput.Key == ConsoleKey.DownArrow)
                    {
                        selection++;
                        if (selection > menuOptions.Count)
                        {
                            selection = 0;
                        }
                    }


                    else if (keyInput.Key == ConsoleKey.Enter)
                    {
                        if (selection == menuOptions.Count)
                        {
                            inMenu = false;
                        }
                        else if (selection == 0)
                        {
                            Console.WriteLine("Ange kundId:");
                            customerId = int.Parse(Console.ReadLine());
                            contentList.Insert(0, customerId.ToString());
                        }
                        else if (selection == 1)
                        {
                            Console.WriteLine("Ange förnamn:");
                            firstName = Console.ReadLine();
                            contentList.Insert(1, firstName);
                        }
                        else if (selection == 2)
                        {
                            Console.WriteLine("Ange efternamn:");
                            lastName = Console.ReadLine();
                            contentList.Insert(2, lastName);
                        }
                        else if (selection == 3)
                        {
                            Console.WriteLine("Ange personnummer:");
                            ssn = int.Parse(Console.ReadLine());
                            contentList.Insert(3, ssn.ToString());
                        }
                        else if (selection == 4)
                        {
                            Console.WriteLine("Ange telefonnummer:");
                            phoneNumber = int.Parse(Console.ReadLine());
                            contentList.Insert(4, phoneNumber.ToString());
                        }
                        else if (selection == 5)
                        {
                            Console.WriteLine("Ange mailadress:");
                            email = Console.ReadLine();
                            contentList.Insert(5, email);
                        }
                        else if (selection == 6)
                        {
                            Console.WriteLine("Ange gatuadress:");
                            streetAdress = Console.ReadLine();
                            contentList.Insert(6, streetAdress);
                        }
                        else if (selection == 7)
                        {
                            Console.WriteLine("Ange postnummer:");
                            postalCode = int.Parse(Console.ReadLine());
                            contentList.Insert(7, postalCode.ToString());
                        }
                        else if (selection == 8)
                        {
                            Console.WriteLine("Ange stad:");
                            city = Console.ReadLine();
                            contentList.Insert(8, city);
                        }
                        else if (selection == 9)
                        {
                            Console.WriteLine("Ange land:");
                            country = Console.ReadLine();
                            contentList.Insert(9, country);
                        }
                        else if (selection == 10)
                        {
                            customers.Customers.Remove(findCustomer);
                            customers.Customers.Add(new Customer(customerId, firstName, lastName, ssn, phoneNumber, email,
                                streetAdress, postalCode, city, country));
                            Console.WriteLine("Kunden är uppdaterad.\nTryck valfri tangent för att fortsätta.");
                            Console.ReadKey();
                            customersMenu.ShowCustomerMenu(customers);
                        }
                    }

                }

            }
            else
            {
                Console.WriteLine("Kunden hittades inte.");
                Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
                Console.ReadKey();
            }
        }

        public void Delete(CustomerList customers)
        {
            Console.Clear();
            Console.WriteLine("Ange kundId för kunden du vill avaktivera:");
            int findId = int.Parse(Console.ReadLine());

            var findCustomer = customers.Customers.FirstOrDefault(c => c.CustomerId == findId);

            if (findCustomer != null)
            {
                int deactivate = YesNoMenu.ShowYesNoMenu($"Är du säker på att du vill avaktivera kund med kundId {findCustomer.CustomerId}?");
                if (deactivate == 1)
                {
                    customers.Customers.Remove(findCustomer);
                    Console.WriteLine("Kunden är avaktiverad.");
                    Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
                    Console.ReadKey();
                }
                else if (deactivate == 2)
                {
                    Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
                    Console.ReadKey();
                }

            }
            else
            {
                Console.WriteLine("Kunden hittades inte.");
                Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
                Console.ReadKey();
            }
        }

        public void FindCustomer(CustomerList customers)
        {
            Console.Clear();
            Console.WriteLine("Ange kundId för kunden du vill visa information om:");
            int findId = int.Parse(Console.ReadLine());

            var findCustomer = customers.Customers.FirstOrDefault(c => c.CustomerId == findId);

            if (findCustomer != null)
            {
                CustomerTableClass.DisplayOneCustomerTable(findCustomer);
                Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Kunden hittades inte.");
                Console.WriteLine("Tryck valfri tangent för att gå tillbaka.");
                Console.ReadKey();
            }
        }
    }
}
