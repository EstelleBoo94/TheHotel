using Microsoft.EntityFrameworkCore;
using TheHotel.Data;
using TheHotel.Displays.MenuFolder;
using TheHotel.Displays.TablesFolder;
using TheHotel.Models;

namespace TheHotel.Services.RoomServices;

public class UpdateRoom
{
    public void Update(DbContextOptions<ApplicationDbContext> options)
    {
        using (var dbContext = new ApplicationDbContext(options))
        {
            bool isEmpty = RoomTableClass.DisplayAllRoomsTable(options, "ÄNDRA RUM");
            if (!isEmpty)
            {
                Console.WriteLine("Tryck för att gå tillbaka.");
                Console.ReadKey();
                return;
            }

            bool isValidInput = false;
            int findId = 0;
            var findRoom = new Room();

            while (!isValidInput)
            {
                Console.WriteLine("Ange rumsId för rummet du vill uppdatera:" +
                    "\nAnge 0 eller lämna tomt för att gå tillbaka");

                int.TryParse(Console.ReadLine(), out findId);
                if (findId == 0)
                {
                    return;
                }
                else if (findId < 0)
                {
                    Console.WriteLine("Ogiltigt rumsId.");
                    Console.ReadKey();
                }
                else
                {
                    findRoom = dbContext.Rooms.FirstOrDefault(r => r.RoomId == findId);
                    if (findRoom == null)
                    {
                        Console.WriteLine("Rummet hittades inte.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
            }

            if (findRoom != null)
            {
                int roomNumber = findRoom.RoomNumber;
                string roomTypeString = findRoom.Type.ToString();
                int size = findRoom.Size;
                decimal price = findRoom.Price;
                RoomType roomTypeEnum = findRoom.Type;

                var contentList = new List<string>
                {
                    findRoom.RoomNumber.ToString(), findRoom.Type.ToString(),
                    findRoom.Size.ToString(), findRoom.Price.ToString()
                };

                List<string> menuOptions = new List<string>
                {
                    "Rumsnummer", "Rumstyp", "Storlek", "Pris","Spara rum"
                };

                MenuTemplate.ShowMenuWithTable("Tillbaka", "roomNoActive",
                    "ÄNDRA RUM", menuOptions, contentList, selection =>
                {
                    switch (selection)
                    {
                        case 0:
                            Console.WriteLine("Ange rumsnummer:");
                            int.TryParse(Console.ReadLine(), out roomNumber);
                            if (roomNumber <= 0)
                            {
                                Console.WriteLine("Ogiltigt rumsnummer.\nTryck för att fortsätta.");
                                Console.ReadKey();
                                return true;
                            }
                            {
                                contentList[0] = roomNumber.ToString();
                            }
                            break;

                        case 1:
                            int selectRoomType = RoomTypeMenu.ShowRoomTypeMenu
                            ("Vilken typ av rum?", "ÄNDRA RUM");
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
                            findRoom.RoomNumber = roomNumber;
                            findRoom.Type = roomTypeEnum;
                            findRoom.Size = size;
                            findRoom.Price = price;

                            dbContext.SaveChanges();
                            Console.WriteLine("Rummet är uppdaterat." +
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
