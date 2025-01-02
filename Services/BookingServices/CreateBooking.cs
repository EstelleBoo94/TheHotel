using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Displays.MenuFolder;
using TheHotel.Displays.TablesFolder;
using TheHotel.Models;
using TheHotel.Services.GuestServices;
using TheHotel.Services.InvoiceServices;
using TheHotel.Services.RoomServices;

namespace TheHotel.Services.BookingServices;

public class CreateBooking
{
    public void Create(DbContextOptions<ApplicationDbContext> options)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            var isGuestRegisterd = YesNoMenu.ShowYesNoMenu(
                "Är gästen som vill boka registrerad hos hotellet?", "NY BOKNING");
            if (isGuestRegisterd == 2)
            {
                Console.WriteLine("Tryck valfri tangent för att gå till att skapa en ny gästprofil");
                Console.ReadKey();
                CreateGuest createGuest = new();
                createGuest.Create(options);
            }
            else if (isGuestRegisterd == 0)
            {
                return;
            }

            bool isEmpty = GuestTableClass.DisplayAllActiveGuestsTable(options, "NY BOKNING");
            if (!isEmpty)
            {
                Console.WriteLine("Tryck för att gå tillbaka.");
                Console.ReadKey();
                return;
            }

            bool isValidInput = false;
            int guestId = 0;
            var guest = new Guest();

            while (!isValidInput)
            {
                Console.WriteLine("Ange gästens kundId:");

                int.TryParse(Console.ReadLine(), out guestId);
                if (guestId == 0)
                {
                    return;
                }
                else if (guestId < 0)
                {
                    Console.WriteLine("Ogiltigt gästId.");
                }
                else
                {
                    guest = dbContext.Guests.FirstOrDefault(g => g.GuestId == guestId && g.IsActive == true);
                    if (guest == null)
                    {
                        Console.WriteLine("Gästen hittades inte.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
            }

            List<string> contentList = new List<string>
            {
                guestId.ToString(), " ", " ", " ", " ", " "
            };
            List<string> menuOptions = new List<string>
            {
                "Antal gäster",
                "Antal rum",
                "Rumstyper",
                "Startdatum",
                "Slutdatum",
                "Välj rum",
                "Spara bokning"
            };

            var numberOfGuests = 0;
            var numberOfRooms = 0;
            var roomTypes = new List<RoomType>();
            var bookingStart = DateOnly.MinValue;
            var bookingEnd = DateOnly.MinValue;
            var findRoom = new Room();
            var bookedRooms = new List<Room>();
            var extraBeds = 0;
            int numberOfNights = 0;

            MenuTemplate.ShowMenuWithTableForBooking("Tillbaka", "ÄNDRA BOKNING",
                menuOptions, contentList, bookedRooms, selection =>
            {
                switch (selection)
                {
                    case 0:
                        Console.WriteLine("Ange med en siffra hur många gäster bokningen gäller för:");
                        int.TryParse(Console.ReadLine(), out numberOfGuests);
                        if (numberOfGuests <= 0)
                        {
                            Console.WriteLine("Ogiltigt antal gäster.");
                            Console.ReadKey();
                            return true;
                        }
                        else
                        {
                            contentList[1] = numberOfGuests.ToString();
                        }
                        break;

                    case 1:
                        if (numberOfGuests == 0)
                        {
                            Console.WriteLine("Ange antal gäster först.");
                            Console.ReadKey();
                        }
                        else if (numberOfGuests == 1)
                        {
                            Console.WriteLine("Ett rum bokas.");
                            numberOfRooms = 1;
                            contentList[2] = "1";
                            Console.ReadKey();
                        }
                        else if (numberOfGuests > 1 && numberOfGuests < 5)
                        {
                            var moreThenOneRoom = YesNoMenu.ShowYesNoMenu(
                                "Ska fler än ett rum bokas?", "NY BOKNING");
                            if (moreThenOneRoom == 1)
                            {
                                Console.WriteLine("Ange med en siffra hur många rum som ska bokas:");
                                int.TryParse(Console.ReadLine(), out numberOfRooms);
                                if (numberOfRooms <= 0)
                                {
                                    Console.WriteLine("Ogiltigt antal rum.");
                                    Console.ReadKey();
                                    return true;
                                }
                                else
                                {
                                    contentList[2] = numberOfRooms.ToString();
                                }
                            }
                            else if (moreThenOneRoom == 2)
                            {
                                numberOfRooms = 1;
                                contentList[2] = "1";
                            }
                        }
                        else if (numberOfGuests >= 5)
                        {
                            Console.WriteLine("Ange med en siffra hur många rum som ska bokas:");
                            int.TryParse(Console.ReadLine(), out numberOfRooms);
                            if (numberOfRooms <= 0)
                            {
                                Console.WriteLine("Ogiltigt antal rum.");
                                Console.ReadKey();
                                return true;
                            }
                            else
                            {
                                contentList[2] = numberOfRooms.ToString();
                            }
                        }
                        break;

                    case 2:
                        if (numberOfRooms == 0)
                        {
                            Console.WriteLine($"Ange antal rum först.");
                            Console.ReadKey();
                        }
                        else
                        {
                            roomTypes.Clear();
                            for (int i = 1; i <= numberOfRooms; i++)
                            {

                                var selectedRoomType = RoomTypeMenu.ShowRoomTypeMenu(
                                    $"Välj rumstyp för rum {i}:", "NY BOKNING");
                                if (selectedRoomType == 1)
                                {
                                    roomTypes.Add(RoomType.Single);
                                    Console.WriteLine("Enkelrum sparat.");
                                    Console.ReadKey();
                                }
                                else if (selectedRoomType == 2)
                                {
                                    roomTypes.Add(RoomType.Double);
                                    Console.WriteLine("Dubbelrum sparat.");
                                    Console.ReadKey();
                                }
                                else if (selectedRoomType == 3)
                                {
                                    roomTypes.Add(RoomType.Suite);
                                    Console.WriteLine("Svit sparad.");
                                    Console.ReadKey();
                                }

                            }

                        }
                        break;


                    case 3:
                        if (roomTypes.Count == 0)
                        {
                            Console.WriteLine("Ange rumstyper först.");
                            Console.ReadKey();
                        }
                        else
                        {
                            bookingStart = CalenderMenu.ShowCalenderMenu(
                                "Välj startdatum för bokningen:", "NY BOKNING", options, roomTypes);
                            contentList[3] = bookingStart.ToString();
                        }
                        break;

                    case 4:
                        if (bookingStart == DateOnly.MinValue)
                        {
                            Console.WriteLine("Ange startdatum först.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Ange med en siffra hur många nätter gästen vill boka:");
                            int.TryParse(Console.ReadLine(), out numberOfNights);
                            if (numberOfNights <= 0)
                            {
                                Console.WriteLine("Ogiltigt antal nätter.");
                                Console.ReadKey();
                                return true;
                            }
                            else
                            {
                                bookingEnd = bookingStart.AddDays(numberOfNights);
                                contentList[4] = bookingEnd.ToString();
                            }
                        }
                        break;

                    case 5:
                        CheckIfAvailiable ifAvailiable = new();
                        var avaliableRooms = ifAvailiable.GetAvailiableRooms(options,
                            bookingStart, bookingEnd, roomTypes);
                        if (avaliableRooms.Count == 0)
                        {
                            Console.WriteLine("Inga lediga rum för det valda datumintervallet. Välj nya datum.");
                            Console.ReadKey();
                        }
                        else if (avaliableRooms.Count < numberOfRooms)
                        {
                            Console.WriteLine("För få lediga rum för det valda datumintervallet. Välj nya datum.");
                            Console.ReadKey();
                        }
                        else
                        {
                            bookedRooms.Clear();
                            for (int i = 1; i <= numberOfRooms; i++)
                            {
                                RoomTableClass.DisplayRoomForBooking(avaliableRooms,
                                    "Lediga rum för perioden:", "NY BOKNING");

                                int findId = 0;
                                Console.WriteLine($"\nAnge rumsId för bokning av rum nummer {i}:");
                                int.TryParse(Console.ReadLine(), out findId);

                                if (findId <= 0)
                                {
                                    Console.WriteLine("Ogiltigt rumsId.");
                                }
                                else
                                {
                                    findRoom = avaliableRooms.FirstOrDefault(r => r.RoomId == findId);
                                    if (findRoom == null)
                                    {
                                        Console.WriteLine("Rummet hittades inte.");
                                    }
                                    else
                                    {
                                        avaliableRooms.Remove(findRoom);
                                        bookedRooms.Add(findRoom);
                                    }
                                }

                                if (findRoom.Size > 24 && numberOfGuests > 1)
                                {
                                    var extraBed = YesNoMenu.ShowYesNoMenu(
                                        "Vill du lägga till en extrasäng i rummet?", "NY BOKNING");
                                    if (extraBed == 1)
                                    {
                                        Console.WriteLine("Extrasäng tillagd.");
                                        Console.ReadKey();
                                        extraBeds += 1;
                                        contentList[5] = extraBeds.ToString();
                                    }

                                    if (findRoom.Size > 30 && numberOfGuests > 1)
                                    {
                                        extraBed = YesNoMenu.ShowYesNoMenu(
                                            "Vill du lägga till en till extrasäng i rummet?", "NY BOKNING");
                                        if (extraBed == 1)
                                        {
                                            Console.WriteLine("Extrasäng tillagd.");
                                            Console.ReadKey();
                                            extraBeds += 1;
                                            contentList[5] = extraBeds.ToString();
                                        }
                                    }

                                    Console.WriteLine("Rummet är bokat.");
                                }

                            }
                        }
                        break;

                    case 6:
                        if (numberOfGuests == 0 || bookingStart == DateOnly.MinValue
                        || bookingEnd == DateOnly.MinValue || bookedRooms.Count == 0)
                        {
                            Console.WriteLine("Fyll i tomma fält.");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            foreach (var room in bookedRooms)
                            {
                                dbContext.Attach(room);
                            }

                            var booking = new Booking
                            {
                                StartDate = bookingStart,
                                EndDate = bookingEnd,
                                NumberOfGuests = numberOfGuests,
                                InvoiceId = null,
                                GuestId = guestId,
                                ExtraBeds = extraBeds,
                                RoomsBooked = bookedRooms
                                
                            };

                            dbContext.Bookings.Add(booking);
                            dbContext.SaveChanges();

                            CreateInvoice createInvoice = new();
                            createInvoice.Create(options, guestId, booking.BookingId,
                                bookedRooms, bookingStart);

                            booking.InvoiceId = dbContext.Invoices
                            .First(i => i.BookingId == booking.BookingId).InvoiceId;
                            dbContext.SaveChanges();

                            booking.Guest.ActiveBookings.Add(booking);
                            dbContext.SaveChanges();

                            Console.WriteLine("Bokningen är registrerad." +
                                "\nKom ihåg att fakturan ska vara betalad inom " +
                                "10 dagar eller en dag innan incheckning." +
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
