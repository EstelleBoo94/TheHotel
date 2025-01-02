using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Displays.MenuFolder;
using TheHotel.Models;

namespace TheHotel.Services.RoomServices;

public class CreateRoom
{
    public void Create(DbContextOptions<ApplicationDbContext> options)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            int roomNumber = 0;
            string roomTypeString = null;
            int size = 0;
            decimal price = 0;
            string isActiveString = null;
            bool isActive = false;
            RoomType roomTypeEnum = RoomType.Single;

            var contentList = new List<string> { " ", " ", " ", " ", " " };

            List<string> menuOptions = new List<string>
            {
               "Rumsnummer", "Rumstyp", "Storlek", "Pris", "Status", "Spara rum"
            };

            MenuTemplate.ShowMenuWithTable("Tillbaka", "room", "NYTT RUM",
                menuOptions, contentList, selection =>
            {
                switch (selection)
                {
                    case 0:
                        Console.WriteLine("Ange rumsnummer:");
                        int.TryParse(Console.ReadLine(), out roomNumber);
                        if (roomNumber <= 0)
                        {
                            Console.WriteLine("Ogiltigt rumsnummer." +
                                "\nTryck för att fortsätta.");
                            Console.ReadKey();
                            return true;
                        }
                        if (dbContext.Rooms.Any(r => r.RoomNumber == roomNumber))
                        {
                            Console.WriteLine("Rumsnummer finns redan." +
                                "\nTryck för att fortsätta.");
                            Console.ReadKey();
                            return true;
                        }
                        else
                        {
                            contentList[0] = roomNumber.ToString();
                        }
                        break;

                    case 1:
                        int selectRoomType = RoomTypeMenu.ShowRoomTypeMenu("Vilken typ av rum?", "NYTT RUM");
                        if (selectRoomType == 1)
                        {
                            roomTypeString = "Enkelrum";
                            roomTypeEnum = RoomType.Single;
                            contentList[1] = roomTypeString;
                        }
                        else if (selectRoomType == 2)
                        {
                            roomTypeString = "Dubbelrum";
                            roomTypeEnum = RoomType.Double;
                            contentList[1] = roomTypeString;
                        }
                        else if (selectRoomType == 3)
                        {
                            roomTypeString = "Svit";
                            roomTypeEnum = RoomType.Suite;
                            contentList[1] = roomTypeString;
                        }
                        break;

                    case 2:
                        Console.WriteLine("Ange storlek:");
                        int.TryParse(Console.ReadLine(), out size);
                        if (size < 10 || size > 100)
                        {
                            Console.WriteLine("Storlek måste vara mellan 10 och 100 kvm." +
                                "\nTryck för att fortsätta.");
                            Console.ReadKey();
                            return true;
                        }
                        else
                        {
                            contentList[2] = size.ToString();
                        }
                        break;

                    case 3:
                        Console.WriteLine("Ange pris:");
                        decimal.TryParse(Console.ReadLine(), out price);
                        if (price <= 0)
                        {
                            Console.WriteLine("Priset måste vara över 0kr." +
                                "\nTryck för att fortsätta.");
                            Console.ReadKey();
                            return true;
                        }
                        {
                            contentList[3] = price.ToString();
                        }
                        break;

                    case 4:
                        int yesOrNo = YesNoMenu.ShowYesNoMenu("Är rummet aktivt för bokning?", "NYTT RUM");
                        if (yesOrNo == 1)
                        {
                            isActiveString = "Aktivt";
                            isActive = true;
                            contentList[4] = isActiveString;
                        }
                        else if (yesOrNo == 2)
                        {
                            isActiveString = "Avstängt";
                            isActive = false;
                            contentList[4] = isActiveString;
                        }
                        break;

                    case 5:
                        if (roomNumber == 0 || size == 0 || price == 0) 
                        {
                            Console.WriteLine("Fyll i tomma fält.");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            dbContext.Rooms.Add(new Room
                            {
                                RoomNumber = roomNumber,
                                Type = roomTypeEnum,
                                Size = size,
                                Price = price,
                                IsActive = isActive

                            });
                            dbContext.SaveChanges();

                            Console.WriteLine("Rummet är registrerat." +
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
