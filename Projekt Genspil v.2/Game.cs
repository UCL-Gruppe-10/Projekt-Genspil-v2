using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Genspil_v._2
{
    public class Game
    {
        int antal;
        int i = 0;
        int j = 0;
        public string title { get; set; }
        private string[] version = new string[10];
        private string genre { get; set; }
        private int amount;
        private int[] players = new int[2];  // Minimum til maksimum spillere
        public char[,] gameCondiditon = new char[10, 50];   // Bør måske være tekst i stedet, så man kan specificere hvad der mangler?
        public int[,] price = new int[10, 50];
        public string[,] notes = new string[10, 50];

        public void CreateGame()
        {
            Console.Write("Hvad hedder spillet: ");
            title = Console.ReadLine();
            Console.Write("Hvilken genre er spillet: ");
            genre = Console.ReadLine();
            Console.Write("Minimum antal spiller? ");
            while (players[0] < 1) Int32.TryParse(Console.ReadLine(), out players[0]);
            Console.Write("Maksimum antal spiller: ");
            while (players[1] < 1 && players[1] < players[0]) Int32.TryParse(Console.ReadLine(), out players[1]);
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
            Console.Clear();
            Console.Write("Tittel        : " + title);
            Console.Write(" | Genre         : " + genre);
            Console.Write(" | Antal spillere: {0} - {1}", players[0], players[1]);
            for (int g = 0; g < version.Length; g++)
            {
                if (version[i] != null)
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
        public void UpdateGame()
        {
            GetGame();
            string updateInfo = null;
            string updateSelector = null;
            bool updateContinue = true;
            do
            {
                Console.WriteLine("Hvad ønsker du at opdatere? 0 for at afslutte.");
                do
                {
                    updateSelector = Console.ReadLine();
                } while (updateSelector == null || updateSelector.Length == 0);

                switch (updateSelector)
                {
                    case "0":
                        updateContinue = false;
                        break;
                    case "1":
                    case "a":
                    case "titel":
                    case "Titel":
                    case "TITEL":
                        Console.Write("Indtast ny titel: ");
                        do
                        {
                            updateInfo = Console.ReadLine();
                        } while (updateInfo == null || updateInfo.Length == 0);
                        title = updateInfo;
                        break;
                    case "2":
                    case "b":
                    case "genre":
                    case "Genre":
                    case "GENRE":
                        Console.Write("Indtast ny genre: ");
                        do
                        {
                            updateInfo = Console.ReadLine();
                        } while (updateInfo == null || updateInfo.Length == 0);
                        genre = updateInfo;
                        break;
                    case "3":
                    case "c":
                    case "spillere":
                    case "Spillere":
                    case "SPILLERE":
                        Console.Write("Indtast nyt minimum antal spillere: ");
                        while (players[0] < 1) Int32.TryParse(Console.ReadLine(), out players[0]);
                        Console.Write("Indtast nyt maksimum antal spillere: ");
                        while (players[1] < 1 && players[1] < players[0]) Int32.TryParse(Console.ReadLine(), out players[1]);
                        break;
                    case "4":
                    case "d":
                    case "version":
                    case "Version":
                    case "VERSION":
                        Console.Write("Indtast ny version: ");
                        do
                        {
                            updateInfo = Console.ReadLine();
                        } while (updateInfo == null || updateInfo.Length == 0);
                        version[i] = updateInfo;
                        break;
                    case "5":
                    case "e":
                    case "stand":
                    case "Stand":
                    case "STAND":
                        Console.Write("Indtast ny stand: ");
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
                        break;
                    case "6":
                    case "f":
                    case "pris":
                    case "Pris":
                    case "PRIS":
                        Console.Write("Indtast ny pris: ");
                        while (price[i, j] == 0) Int32.TryParse(Console.ReadLine(), out price[i, j]);
                        break;
                    case "7":
                    case "g":
                    case "note":
                    case "Note":
                    case "NOTE":
                        Console.Write("Indtast ny note: ");
                        do
                        {
                            updateInfo = Console.ReadLine();
                        } while (updateInfo == null || updateInfo.Length == 0);
                        notes[i, j] = updateInfo;
                        break;
                    default:
                        Console.WriteLine("Ugyldigt redigerings kriterie");
                        break;

                }
            } while (updateContinue == true);
        }
    }
}
