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
        public int i = -1;
        public int[] j = new int[10];
        public string title;
        public string[] version = new string[10];
        public string genre { get; set; }
        private int amount;
        public int[] players = new int[2];  // Minimum til maksimum spillere
        public char[,] gameCondiditon = new char[10, 20];
        public int[,] price = new int[10, 20];
        public string[,] notes = new string[10, 20];

        public void SetTitle(string Title)
        {
            title = Title;
        }
        public void SetVersion(string Version)
        {
            i++;
            version[i] = Version;
            
            
        }
        public void SetGenre(string Genre)
        {
            genre = Genre;
        }
        public void SetPlayers(int[] Players)
        {
            int P = 0;
            foreach (var p in Players)
            {
                players[i] = p;
                P++;
            }
        }
        public void SetGameCondition(char condition)
        {
            gameCondiditon[(i - 1), j[i]] = condition;
            j[i] = j[i] +1;
        }
        public void SetPrice(int Price)
        {
            price[(i - 1), j[i]] = Price;
        }
        public void SetNotes(string Notes)
        {
            if (Notes != null) notes[(i - 1), j[i]] = Notes;
            else
                notes[(i - 1), j[i]] = null;
        }

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
            i++;
            version[i] = Console.ReadLine();
            Console.Write("Hvilken stand er spillet i: ");
            do
            {
                try
                {
                    gameCondiditon[i, j[i]] = Convert.ToChar(Console.ReadLine());
                    char.ToUpper(gameCondiditon[i, j[i]]); 
                }
                catch
                {
                    Console.WriteLine("Forkert input - prøv igen");
                }
            } while (gameCondiditon[i, j[i]] == null);
            Console.Write("Hvad koster spillet: ");
            while (price[i, j[i]] == 0) Int32.TryParse(Console.ReadLine(), out price[i, j[i]]);
            Console.Write("Noter: ");
            notes[i, j[i]] = Console.ReadLine();
            j[i]++;
            antal++;
        }
        public void GetGame()
        {
            Console.Write("Tittel        : " + title);
            Console.Write(" | Genre         : " + genre);
            Console.WriteLine(" | Antal spillere: {0} - {1}", players[0], players[1]);
            for (int g = 0; g < version.Length; g++)
            {
                if (version[g] != null)
                {
                    Console.WriteLine("Udgave        : " + version[g]);
                    for (int n = 0; n < price.Length; n++)
                    {
                        if (price[g, n] != 0)
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
        public string GetTitle(string saveTitle)
        {
            saveTitle = $"Tittel: {title} : Genre: {genre} : Minimum spillere: {players[0]} : maximum: {players[1]} ";
            return saveTitle;
        }
        public string GetVersion(string[] saveVersion)
        {
            saveVersion = new string[50];
            for (int i = 0; i < saveVersion.Length; i++)
            {
                if (saveVersion[i] != null)
                {
                    saveVersion[i] = version[i];

                    
                }
                else break;                
            }
            return saveVersion[saveVersion.Length];
            
        }
    }
}
