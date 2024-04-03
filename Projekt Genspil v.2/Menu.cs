using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Genspil_v._2
{
    internal class Menu
    {
        public Game[] gameTitle = new Game[50];       
        int gameItem = 0;

        public void ShowMenu() // Printer den primære menu
        {
            //Console.Clear();
            Console.WriteLine("Genspil");
            Console.WriteLine("-----------------------------------\n\n");
            Console.WriteLine("(1) Soeg spil");
            Console.WriteLine("(2) Opret spil");
            Console.WriteLine("(3) Opdater spil");
            Console.WriteLine("\n(Afslut programmet - tast 0)");
        }
        public void SelectMenuItem()
        {
            int menuItem = -1;
            do
            {
                string valg = Console.ReadLine();
                Int32.TryParse(valg, out menuItem);
                switch (menuItem)
                {
                    case 1:
                        SearchGames();
                        break;
                    case 2:
                        CreateGame();
                        break;
                    case 3:
                        gameTitle[gameItem].UpdateGame();   // Mangler ordenlig tilgang til specifike spil - Lægges i forlængelse af SØG SPIL!
                        break;
                    case 0:
                        Console.WriteLine("Farvel");
                        break;
                    default:
                        Console.Write("Fejlinput, prøv igen:");
                        break;
                }
                ShowInventory();
                ShowMenu();
            } while (menuItem != 0);
        }

        void SearchGames()
        { 
            Console.WriteLine("Indstast titlen på det spil du søger efter:");
            string searchWord = Console.ReadLine();

            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false; 
            foreach (Game game in gameTitle) // Her gennemgår den Array'et efter søgeordet, hvor den så vil sortere dem efter kriteriet
            {
                if (game != null && game.title.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine(game.title);
                    found = true;
                }
            }
            // Vis en besked, hvis der ikke er nogen søgeresultater
            if (!found)
            {
                Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
            }

            Console.ReadLine();

        }

        void CreateGame()
        {
            if (gameItem < 50)
            {
                Console.Clear();
                gameTitle[gameItem] = new Game();
                gameTitle[gameItem].CreateGame();
                gameItem++;
            }
            else 
            {
                Console.WriteLine("Maximum antal er nået");
            }
        }


        public void ShowInventory()
        {
            Console.Clear();
            for (int i = 0; i < 50; i++)
            {
                if (gameTitle[i] != null)
                {
                    gameTitle[i].GetGame();
                }
                else
                    break;
            }
        }

        
    }
}
