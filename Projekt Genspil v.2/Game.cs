using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Genspil_v._2
{
    internal class Game
    {
        int antal;
        int i = 0;
        int j = 0;
        public string title { get; set; }
        public string[] version = new string[10];
        public string genre { get; set; }
        private int amount;
        public int[] players = new int[2];  // Minimum til maksimum spillere
        public char[,] gameCondiditon = new char[10, 50];
        public int[,] price = new int[10, 50];
        public string[,] notes = new string[10, 50];

        public void CreateGame()
        {
            Console.Write("Hvad hedder spillet: ");
            title = Console.ReadLine();
            Console.Write("Hvilken genre er spillet: ");
            genre = Console.ReadLine();
            Console.Write("Minimum antal spiller? ");
            while (players[0] == 0) Int32.TryParse(Console.ReadLine(), out players[0]);
            Console.Write("Maksimum antal spiller: ");
            while (players[1] == 0) Int32.TryParse(Console.ReadLine(), out players[1]);
            Console.Write("Hvilken version er spillet: ");
            version[i] = Console.ReadLine();
            Console.Write("Hvilken stand er spillet i: ");
            do
            {
                try
                {
                    gameCondiditon[i, j] = Convert.ToChar(Console.ReadLine());
                    char.ToUpper(gameCondiditon[i, j]); 
                }
                catch
                {
                    Console.WriteLine("Forkert input - prøv igen");
                }
            } while (gameCondiditon[i, j] == null);
            Console.Write("Hvad koster spillet: ");
            while (price[i, j] == 0) Int32.TryParse(Console.ReadLine(), out price[i, j]);
            Console.Write("Noter: ");
            notes[i, j] = Console.ReadLine();
            i++;
            j++;
            antal++;
        }
        public void GetGame()
        {
            Console.Write("Tittel        : " + title);
            Console.Write(" | Genre         : " + genre);
            Console.Write(" | Antal spillere: {0} - {1}", players[0], players[1]);
            for (int g = 0; g < version.Length; g++)
            {
                if (version[g] != null)
                {
                    Console.WriteLine("Udgave        : " + version[g]);
                    for (int n = 0; n < price.Length; n++)
                    {
                        if (price[g, n] == 0)
                        {
                            Console.WriteLine($"     Index: {n} | Stand: {gameCondiditon[g, n]} | Pris {price[g, n]} | Noter: {notes[g, n]}");
                        }
                        else
                            break;
                    }
                }
                else
                    break;
            }
        }
    }
}
