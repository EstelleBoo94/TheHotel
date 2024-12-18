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
    public class RoomServices : ICrudOperations<RoomList>
    {
        public void Create(RoomList rooms)
        {
            RoomsMenu roomsMenu = new();
            //RoomList rooms = new();

            List<string> menuOptions = new List<string>
            {
            "Rumsnummer", "Rumstyp", "Storlek", "Status", "Spara rum"
            };

            int selection = 0;
            bool inMenu = true;

            int roomNumber = 0;
            string roomType = null;
            int size = 0;
            string free = null;

            bool isActive = false;
            RoomType whatRoomType = RoomType.Single;

            var contentList = new List<string> { " ", " ", " ", " " };

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


                RoomTableClass.DisplayRoomTable(contentList);

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
                        Console.WriteLine("Ange rumsnummer:");
                        roomNumber = int.Parse(Console.ReadLine());
                        contentList.Insert(0, roomNumber.ToString());
                    }
                    else if (selection == 1)
                    {
                        int selectRoomType = RoomTypeMenu.ShowRoomTypeMenu("Vilken typ av rum?");
                        if (selectRoomType == 1)
                        {
                            roomType = "Enkelrum";
                            whatRoomType = RoomType.Single;
                            contentList.Insert(1, roomType);
                        }
                        else if (selectRoomType == 2) 
                        {
                            roomType = "Dubbelrum";
                            whatRoomType = RoomType.Double;
                            contentList.Insert(1, roomType);
                        }
                        else if (selectRoomType == 3)
                        {
                            roomType = "Svit";
                            whatRoomType = RoomType.Suite;
                            contentList.Insert(1, roomType);
                        }
                    }
                    else if (selection == 2)
                    {
                        Console.WriteLine("Storlek:");
                        size = int.Parse(Console.ReadLine());
                        contentList.Insert(2, size.ToString());
                    }
                    else if (selection == 3)
                    {
                        int yesOrNo = YesNoMenu.ShowYesNoMenu("Är rummet aktivt för bokning?");
                        if (yesOrNo == 1)
                        {
                            free = "Aktivt";
                            isActive = true;
                            contentList.Insert(3, free);
                        }
                        else if (yesOrNo == 2) 
                        {
                            free = "Avstängt";
                            isActive = false;
                            contentList.Insert(3, free);
                        }
                    }
                    else if (selection == 4)
                    {
                        rooms.Rooms.Add(new Room(roomNumber, whatRoomType, size, false, isActive));
                        Console.WriteLine("Rummet är registrerat.\nTryck valfri tangent för att fortsätta.");
                        Console.ReadKey();
                        roomsMenu.ShowRoomMenu(rooms);
                    }
                }

            }

            roomsMenu.ShowRoomMenu(rooms);
        }

        public void Read(RoomList rooms)
        {

        }

        public void Update(RoomList rooms)
        {

        }

        public void Delete(RoomList rooms)
        {

        }
    }
}
