using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TheHotel.Data;
using TheHotel.Displays.MenuFolder;
using TheHotel.Models;

namespace TheHotel.Services.GuestServices;

public class CreateGuest
{
    public void Create(DbContextOptions<ApplicationDbContext> options)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            var guest = new Guest();
            var validatePhoneContext = new ValidationContext(guest) { MemberName = nameof(guest.PhoneNumber)};
            var validateEmailContext = new ValidationContext(guest) { MemberName = nameof(guest.Email) };

            string firstName = null;
            string lastName = null;
            int ssn = 0;
            string phoneNumber = null;
            string email = null;
            string streetAdress = null;
            int postalCode = 0;
            string city = null;
            string country = null;

            var contentList = new List<string> 
            {
                " ", " ", " ", " ", " ", " ", " ", " ", " " 
            };

            List<string> menuOptions = new List<string>
            {
                "Förnamn", "Efternamn", "Personnummer",
                "Telefonummer", "Email", "Gatuadress", 
                "Postnummer", "Stad", "Land", "Spara kund"
            };

            MenuTemplate.ShowMenuWithTable("Tillbaka", "guest",
                "REGISTRERA NY GÄST", menuOptions, contentList, selection =>
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
                        if (firstName == null || lastName == null || ssn == 0
                        || phoneNumber == null || email == null || streetAdress == null
                        || postalCode == 0 || city == null || country == null)
                        {
                            Console.WriteLine("Fyll i tomma fält.");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            guest.FirstName = firstName;
                            guest.LastName = lastName;
                            guest.SSN = ssn;
                            guest.PhoneNumber = phoneNumber;
                            guest.Email = email;
                            guest.StreetAddress = streetAdress;
                            guest.PostalCode = postalCode;
                            guest.City = city;
                            guest.Country = country;
                            guest.IsActive = true;

                            dbContext.Guests.Add(guest);
                            dbContext.SaveChanges();

                            Console.WriteLine("Kunden är registrerad." +
                                "\nTryck valfri tangent för att fortsätta.");
                            Console.ReadKey();
                            return false;
                        }
                }
                return true;
            });

        }
    }
}
