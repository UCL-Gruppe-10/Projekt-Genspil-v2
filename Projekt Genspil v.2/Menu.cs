using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Projekt_Genspil_v._2
{
    internal class Menu
    {
        public Game[] gameTitle = new Game[50];
        Datahandler saveTitle = new Datahandler();
        int gameItem = 0;
        int sIndex = 0;
        public int GameItem 
        { 
            set { gameItem = value; } 
            get { return gameItem; }
        }

        public Menu()
        {
            //Game[] gameTitle = new Game[50];
            Datahandler saveTitle = new Datahandler();
            saveTitle.ReadList(gameTitle, gameItem);

            GameItem = saveTitle.Item;
        }

        public void ShowMenu() // Printer den primære menu
        {
            //Console.Clear();
            Console.WriteLine("Genspil");
            Console.WriteLine("-----------------------------------\n\n");
            // Sorteringsmuligheder efter spilnavn, genre og andre relevante kriterier.
            Console.Write("(1) Tilgå spil"); // rediger funktion herunder
            // Mulighed for at søge efter spil baseret på forskellige kriterier, såsom genre, antal spillere, stand, pris og navn.
            // Skal evt kunne opdatere forspørgsel og salg
            Console.WriteLine(" - Søg, Rediger, Salg og forspørgsel"); 
            Console.WriteLine("(2) Opret spil"); // opdater koder til arbejde med persistens.
            // Mulighed for at se, hvilke spil der er tilgængelige i lageret, og hvilke der er reserveret eller bestilt, og mulighed for en udskrift.
            // Sorteringsmuligheder efter spilnavn, genre og andre relevante kriterier.
            Console.WriteLine("(3) Lagerliste"); 
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
                        

                        Console.Clear();
                        gameTitle[gameItem] = new Game();
                        gameTitle[gameItem].CreateGame();
                        gameItem++;
                        ShowInventory();
                        ShowMenu();

                        break;
                    case 4:
                        gameTitle[gameItem].UpdateGame();   // Mangler ordenlig tilgang til specifike spil - Lægges i forlængelse af SØG SPIL!
                        break;
                    case 0:
                        saveTitle.PrintList(gameTitle);
                        Console.WriteLine("Farvel");
                        break;
                    default:
                        Console.Write("Fejlinput, prøv igen:");
                        break;
                }
            } while (menuItem != 0);
        }

        void SearchGames()
        {
            string searchCriteria;
            while (true)
            {
                Console.Clear(); 
                Console.WriteLine("Du har valgt søgefunktionen");
                Console.WriteLine("=================");
                Console.WriteLine("Søg efter:");
                Console.WriteLine("(1) Titel");
                Console.WriteLine("(2) Genre");
                Console.WriteLine("(3) Spillere");
                Console.WriteLine("(4) Stand");
                Console.WriteLine("(5) Pris");
                Console.WriteLine("(6) Noter");
                Console.WriteLine("\n(0) Tilbage");
                Console.WriteLine("=================");

                int searchMenu = int.Parse(Console.ReadLine());
                switch (searchMenu)
                {
                    case 0:
                        return;
                    case 1:
                        Console.Write("Søg efter titel: ");
                        searchCriteria = Console.ReadLine();
                        if (searchCriteria != null && searchCriteria.Length > 0)
                        {
                            SearchGamesTitle(searchCriteria);
                        }
                        break;
                    case 2:
                        Console.Write("Søg efter genre: ");
                        searchCriteria = Console.ReadLine();
                        if (searchCriteria != null && searchCriteria.Length > 0)
                        {
                            SearchGamesGenre(searchCriteria);
                        }
                        break;
                    case 3:
                        Console.Write("Søg efter spillere: ");
                        searchCriteria = Console.ReadLine();
                        if (searchCriteria != null && int.Parse(searchCriteria) > 0)
                        {
                            SearchGamesPlayers(int.Parse(searchCriteria));
                        }
                        break;
                    case 4:
                        Console.Write("Søg efter stand: ");
                        searchCriteria = Console.ReadLine();
                        if (searchCriteria != null && searchCriteria.Length == 1)
                        {
                            //SearchGamesCondition(Convert.ToChar(searchCriteria));
                        }
                        break;
                    case 5:
                        Console.Write("Søg efter pris: ");
                        searchCriteria = Console.ReadLine();
                        if (searchCriteria != null && int.Parse(searchCriteria) > 0)
                        {
                            //SearchGamesPrice(int.Parse(searchCriteria));
                        }
                        break;
                    case 6:
                        Console.Write("Søg efter noter: ");
                        searchCriteria = Console.ReadLine();
                        if (searchCriteria != null && searchCriteria.Length > 0)
                        {
                            //SearchGamesNotes(searchCriteria);
                        }
                        break;
                    default:
                        Console.WriteLine("Søgekriterie ikke brugbart.\nTryk Enter for at prøve igen.");
                        Console.ReadLine();
                        break;

                }
            }

        }

        void SearchGamesTitle(string searchWord)
        {

            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;
            //int indexerTitle;
            foreach (Game game in gameTitle) // Her gennemgår den Array'et efter søgeordet, hvor den så vil sortere dem efter kriteriet
            {
                //indexerTitle++;
                if (game != null && game.title.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine($"{game.title}");
                    //Console.WriteLine($"{game.title} ({indexerTitle})");
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

        void SearchGamesGenre(string searchWord)
        {
            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;
            foreach(Game game in gameTitle)
            {
                if(game != null && game.title.IndexOf(searchWord,StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine($"{game.title}, {game.genre}");
                    found = true;
                }
            }

        if(!found)
            {
                Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
            }
            Console.ReadLine();

        }

        void SearchGamesPlayers(int searchWord)
        {

            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;
            foreach (Game game in gameTitle) // Her gennemgår den Array'et efter søgeordet, hvor den så vil sortere dem efter kriteriet
            {
                if (game != null && game.players[0] < searchWord && game.players[1] > searchWord)
                {
                    Console.WriteLine($"{game.title}, {game.players[0]} til {game.players[1]}");
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


        //void SearchGamesCondition(string searchWord)
        //{
        //    Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
        //    bool found = false;
        //    foreach (Game game in gameTitle)
        //    {
        //        if (game != null && game.title.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
        //        {
        //            Console.WriteLine($"{game.title}, {game.gameCondiditon}");
        //            found = true;
        //        }
        //    }

        //    if (!found)
        //    {
        //        Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
        //    }
        //    Console.ReadLine();

        //}

        //void SearchGamesPrice(int searchWord)
        //{

        //    Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
        //    bool found = false;
        //    foreach (Game game in gameTitle) // Her gennemgår den Array'et efter søgeordet, hvor den så vil sortere dem efter kriteriet
        //    {
        //        if (game != null && game.price[i] < searchWord)
        //        {
        //            Console.WriteLine($"{game.title}, {game.players[0]} til {game.players[1]}");
        //            found = true;
        //        }
        //    }
        //    // Vis en besked, hvis der ikke er nogen søgeresultater
        //    if (!found)
        //    {
        //        Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
        //    }

        //    Console.ReadLine();
        //}








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
                    gameTitle[i].PrintListe();
                    Console.WriteLine();
                }
                else
                    break;
            }
        }
    }
}
