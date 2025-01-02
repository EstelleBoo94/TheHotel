using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TheHotel.Data;
using TheHotel.Displays.MenuFolder;
using TheHotel.Displays.TablesFolder;
using TheHotel.Models;

namespace TheHotel.Services.GuestServices;

public class UpdateGuest
{
    public void Update(DbContextOptions<ApplicationDbContext> options)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            bool isEmpty = GuestTableClass.DisplayAllGuestsTable(
                options, "ÄNDRA GÄSTINFORMATION");
            if (!isEmpty)
            {
                Console.WriteLine("Tryck för att gå tillbaka.");
                Console.ReadKey();
                return;
            }

            bool isValidInput = false;
            int findId = 0;
            var findGuest = new Guest();
            var validatePhoneContext = new ValidationContext(findGuest) { MemberName = nameof(findGuest.PhoneNumber) };
            var validateEmailContext = new ValidationContext(findGuest) { MemberName = nameof(findGuest.Email) };

            while (!isValidInput)
            {
                Console.WriteLine("Ange gästId för gästen du vill uppdatera:" +
                    "\nAnge 0 eller lämna tomt för att gå tillbaka");
                
                int.TryParse(Console.ReadLine(), out findId);
                if (findId == 0)
                {
                    return;
                }
                else if (findId < 0)
                {
                    Console.WriteLine("Ogiltigt gästId.");
                }
                else
                {
                    findGuest = dbContext.Guests.FirstOrDefault(g => g.GuestId == findId);
                    if (findGuest == null)
                    {
                        Console.WriteLine("Gästen hittades inte.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
            }

            if (findGuest != null)
            {
                string firstName = findGuest.FirstName;
                string lastName = findGuest.LastName;
                int ssn = findGuest.SSN;
                string phoneNumber = findGuest.PhoneNumber;
                string email = findGuest.Email;
                string streetAdress = findGuest.StreetAddress;
                int postalCode = (int)findGuest.PostalCode;
                string city = findGuest.City;
                string country = findGuest.Country;

                var contentList = new List<string>
                {
                    findGuest.FirstName, findGuest.LastName,
                    findGuest.SSN.ToString(), findGuest.PhoneNumber.ToString(),
                    findGuest.Email, findGuest.StreetAddress,
                    findGuest.PostalCode.ToString(), findGuest.City, findGuest.Country
                };

                List<string> menuOptions = new List<string>
                {
                    "Förnamn", "Efternamn", "Personnummer",
                     "Telefonummer", "Email", "Gatuadress", "Postnummer",
                    "Stad", "Land", "Spara kund"
                };

                MenuTemplate.ShowMenuWithTable("Tillbaka", "guest", 
                    "ÄNDRA GÄSTINFORMATION", menuOptions, contentList, selection =>
                {
                    switch (selection)
                    {
                        case 0:
                            Console.WriteLine("Ange förnamn:");
                            firstName = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(firstName))
                            {
                                Console.WriteLine("Förnamn får inte vara tomt." +
                                    "\nTryck för att fortsätta.");
                                Console.ReadKey();
                                return true;
                            }
                            else
                            {
                                contentList[0] = firstName;
                            }
                            break;

                        case 1:
                            Console.WriteLine("Ange efternamn:");
                            lastName = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(lastName))
                            {
                                Console.WriteLine("Efternamn får inte vara tomt." +
                                    "\nTryck för att fortsätta.");
                                Console.ReadKey();
                                return true;
                            }
                            else
                            {
                                contentList[1] = lastName;
                            }
                            break;

                        case 2:
                            Console.WriteLine("Ange personnummer/födelsedag (YYYYMMDD," +
                                " ej sista fyra i personnummer):");
                            int.TryParse(Console.ReadLine(), out ssn);
                            if (ssn.ToString().Length != 8)
                            {
                                Console.WriteLine("Personnummer måste vara 8 siffror (YYYYMMDD)." +
                                    "\nTryck för att fortsätta.");
                                Console.ReadKey();
                                return true;
                            }
                            if (ssn - DateTime.Now.Year < 18)
                            {
                                Console.WriteLine("Gästen måste vara över 18 år för att kunna registreras." +
                                    "\nTryck för att fortsätta.");
                                Console.ReadKey();
                                return true;
                            }
                            else
                            {
                                contentList[2] = ssn.ToString();
                            }
                            break;

                        case 3:
                            Console.WriteLine("Ange telefonnummer:");
                            phoneNumber = Console.ReadLine();
                            if (Validator.TryValidateProperty(phoneNumber, validatePhoneContext, null))
                            {
                                contentList[3] = phoneNumber;
                            }
                            else
                            {
                                Console.WriteLine("Ogiltigt telefonnummer." +
                                    "\nTryck för att fortsätta.");
                                Console.ReadKey();
                            }
                            break;

                        case 4:
                            Console.WriteLine("Ange mailadress:");
                            email = Console.ReadLine();
                            if (Validator.TryValidateProperty(email, validateEmailContext, null) 
                            && !string.IsNullOrWhiteSpace(email))
                            {
                                contentList[4] = email;
                            }
                            else
                            {
                                Console.WriteLine("Ogiltig emailadress." +
                                    "\nTryck för att fortsätta.");
                                Console.ReadKey();
                            }
                            break;

                        case 5:
                            Console.WriteLine("Ange gatuadress:");
                            streetAdress = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(firstName))
                            {
                                Console.WriteLine("Gatuadress får inte vara tomt." +
                                    "\nTryck för att fortsätta.");
                                Console.ReadKey();
                                return true;
                            }
                            else
                            {
                                contentList[5] = streetAdress;
                            }
                            break;

                        case 6:
                            Console.WriteLine("Ange postnummer:");
                            int.TryParse(Console.ReadLine(), out postalCode);
                            if (postalCode.ToString().Length != 5)
                            {
                                Console.WriteLine("Postnummer ska vara 5 siffror." +
                                    "\nTryck för att fortsätta.");
                                Console.ReadKey();
                                return true;
                            }
                            else
                            {
                                contentList[6] = postalCode.ToString();
                            }
                            break;

                        case 7:
                            Console.WriteLine("Ange stad:");
                            city = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(firstName))
                            {
                                Console.WriteLine("Stad får inte vara tomt." +
                                    "\nTryck för att fortsätta.");
                                Console.ReadKey();
                                return true;
                            }
                            else
                            {
                                contentList[7] = city;
                            }
                            break;

                        case 8:
                            Console.WriteLine("Ange land:");
                            country = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(firstName))
                            {
                                Console.WriteLine("Land får inte vara tomt." +
                                    "\nTryck för att fortsätta.");
                                Console.ReadKey();
                                return true;
                            }
                            else
                            {
                                contentList[8] = country;
                            }
                            break;

                        case 9:
                            findGuest.FirstName = firstName;
                            findGuest.LastName = lastName;
                            findGuest.SSN = ssn;
                            findGuest.PhoneNumber = phoneNumber;
                            findGuest.Email = email;
                            findGuest.StreetAddress = streetAdress;
                            findGuest.PostalCode = postalCode;
                            findGuest.City = city;
                            findGuest.Country = country;

                            dbContext.SaveChanges();
                            Console.WriteLine("Kunden är uppdaterad." +
                                "\nTryck valfri tangent för att fortsätta.");
                            Console.ReadKey();
                            return false;
                    }
                    return true;
                });

            }
        }
    }
}
