using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.Displays;

public static  class WelcomeScreen
{
    public static void ShowWelcomeScreen()
    {
        int consoleWidth = Console.WindowWidth;
        string welcomeMessage = "            NET24\n                       THE MIND PALACE\n                          hotellet";
        int spaces = consoleWidth / 2 - welcomeMessage.Length / 2;
        string leadingSpaces = new string(' ', spaces);
        Console.WriteLine($"\n\n\n{leadingSpaces}I sammarbete med .NET24-Mataffären\n\n\n        {leadingSpaces}Välkommen till");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"{leadingSpaces}");
        Console.ResetColor();

        foreach (char c in welcomeMessage)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(c);
            Console.ResetColor();
            Thread.Sleep(70);
        }
        Thread.Sleep(500);
        Console.WriteLine($"\n\n\n{leadingSpaces}      Hitta ditt inre lugn\n" +
            $"{leadingSpaces}        Hitta ditt inre\n{leadingSpaces}          MIND PALACE");
        Thread.Sleep(500);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n\n\n{leadingSpaces}           Boka nu!\n" +
            $"{leadingSpaces}  valrfi tangent för att starta...");
        Console.ResetColor();
        Console.ReadKey();
    }
}
