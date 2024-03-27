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
                        Console.WriteLine("Søg spil");
                        break;
                    case 2:
                        Console.Clear();
                        gameTitle[gameItem] = new Game();
                        gameTitle[gameItem].CreateGame();
                        gameItem++;
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
