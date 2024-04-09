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
        public List<Game> gameInstance = new List<Game>();
        DataHandler saveGameInstance = new DataHandler("GenspilLagerliste.txt");
        int gameItem = 0;
        public int GameItem
        {
            set { gameItem = value; }
            get { return gameItem; }
        }


        public Menu()
        {
            //Game[] gameInstance = new Game[99];
            DataHandler loadGameInstance = new DataHandler("GenspilLagerliste.txt");
            gameInstance = loadGameInstance.LoadGames();

            GameItem = loadGameInstance.Item;
        }

        public void ShowMainMenu()
        {
            Console.WriteLine($"Genspil\n= = = = = = = = = =");

            // Multi-kriterie søg?
            Console.WriteLine("(1) Tilgå spil - Søg, rediger"); // Salg, forespørgsel, venteliste? // rediger funktion herunder

            // Mulighed for at søge efter spil baseret på forskellige kriterier, såsom genre, antal spillere, stand, pris og titel.
            // Skal evt kunne opdatere forspørgsel og salg
            Console.WriteLine("(2) Opret spil"); // opdater koder til arbejde med persistens. 

            // Mulighed for at se, hvilke spil der er tilgængelige i lageret, og hvilke der er reserveret eller bestilt, og mulighed for en udskrift.
            // Sorteringsmuligheder efter spilnavn, genre og andre relevante kriterier.
            Console.WriteLine("(3) Lagerliste");

            Console.WriteLine("(4) Gem og udskriv lagerliste");
            Console.WriteLine("(5) Print aktivt gameInstance til konsol");

            Console.WriteLine("\n(0) for at afslutte");
        }

        public void SelectMainMenu()
        {
            int menuItem = -1;
            do
            {
                ShowMainMenu();
                menuItem = SelectMenuItem();
                switch (menuItem)
                {
                    case 1:
                        SearchGames();
                        break;
                    case 2:
                        CreateGame();
                        break;
                    case 3:
                        ShowInventory();
                        break;
                    case 4:
                        saveGameInstance.SaveGames(gameInstance);
                        break;
                    case 5:
                        ShowInventory();
                        break;
                    case 0:
                        saveGameInstance.SaveGames(gameInstance);
                        Console.WriteLine("Farvel");
                        break;
                    default:
                        Console.Write("Fejlinput, prøv igen:");
                        break;
                }
            } while (menuItem != 0);
        }

        public int SelectMenuItem()
        {
            int itemId = -1;
            bool isItemIdValid = false;
            do
            {
                if (Int32.TryParse(Console.ReadLine(), out itemId))
                {
                    if (itemId >= 0)
                    {
                        isItemIdValid = true;
                    }
                }
                else
                {
                    Console.WriteLine("Indtast venligst et brugbart tal. ");
                }
            } while (!isItemIdValid);
            return itemId;
        }


        void CreateGame()
        {
            if (gameItem < 99)
            {
                Console.WriteLine(" - Information til nyt spil - ");
                Console.Write("Navn: ");
                string tempTitle = Console.ReadLine();
                Console.Write("Version: ");
                string tempVersion = Console.ReadLine();
                Console.Write("Genre: ");
                string tempGenre = Console.ReadLine();
                int tempMinPlayers;
                while (true)
                {
                    Console.Write("Minimum spillere: ");
                    if (int.TryParse(Console.ReadLine(), out tempMinPlayers))
                        break;
                }
                int tempMaxPlayers;
                while (true)
                {
                    Console.Write("Max antal spillere: ");
                    if (int.TryParse(Console.ReadLine(), out tempMaxPlayers))
                        break;
                }
                Console.Write("Stand: ");
                string tempCondition = Console.ReadLine();
                int tempPrice;
                while (true)
                {
                    Console.Write("Pris: ");
                    if (int.TryParse(Console.ReadLine(), out tempPrice))
                        break;
                }
                Console.Write("Noter: ");
                string tempNotes = Console.ReadLine();
                gameInstance.Add(new Game(tempTitle, tempVersion, tempGenre, tempMinPlayers, tempMaxPlayers, tempCondition, tempPrice, tempNotes));
            }
            else
            {
                Console.WriteLine("Maximum antal er nået");
            }
        }

        /*public void ReadStorage()
        {
            DataHandler loadGameInstance = new DataHandler("GenspilLagerliste_Backup.txt");
            gameInstance = loadGameInstance.LoadGames();

            GameItem = loadGameInstance.Item;
        }*/

        public void ShowInventory()
        {
            //Console.Clear();
            /*for (int i = 0; i < 99; i++)
            {
                if (gameInstance[i] != null)
                {
                    gameInstance[i].ShowGame();
                    Console.WriteLine();
                }
                else
                    break;
            }*/
            foreach (Game game in gameInstance)
            {
                game.ShowGame();
            }
        }


        void SearchGames()
        {
            string searchCriteria;
            while (true)
            {
                //Console.Clear();
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

                int searchMenu = SelectMenuItem();
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
                            SearchGamesCondition(searchCriteria);
                        }
                        break;
                    case 5:
                        Console.Write("Søg efter pris: ");
                        searchCriteria = Console.ReadLine();
                        if (searchCriteria != null && int.Parse(searchCriteria) > 0)
                        {
                            SearchGamesPrice(int.Parse(searchCriteria));
                        }
                        break;
                    case 6:
                        Console.Write("Søg efter noter: ");
                        searchCriteria = Console.ReadLine();
                        if (searchCriteria != null && searchCriteria.Length > 0)
                        {
                            SearchGamesNotes(searchCriteria);
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
            foreach (Game game in gameInstance) // Her gennemgår den Array'et efter søgeordet, hvor den så vil sortere dem efter kriteriet
            {
                if (game != null && game.Title.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine($"{game.Title}");
                    found = true;
                }
            }
            // Vis en besked, hvis der ikke er nogen søgeresultater
            if (!found)
            {
                Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
            }

            Console.WriteLine("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }

        void SearchGamesGenre(string searchWord)
        {
            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;
            foreach (Game game in gameInstance)
            {
                if (game != null && game.Genre.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine($"{game.Title}, {game.Genre}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
            }

            Console.WriteLine("Tryk Enter for at fortsætte...");
            Console.ReadLine();

        }

        void SearchGamesPlayers(int searchWord)
        {
            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;
            foreach (Game game in gameInstance)
            {
                if (game != null && game.MinPlayers < searchWord && game.MaxPlayers > searchWord)
                {
                    Console.WriteLine($"{game.Title}, {game.MinPlayers} til {game.MaxPlayers}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
            }

            Console.WriteLine("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }


        void SearchGamesCondition(string searchWord)
        {
            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;
            foreach (Game game in gameInstance)
            {
                if (game != null && game.Condition.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine($"{game.Title}, {game.Condition}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
            }

            Console.WriteLine("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }

        void SearchGamesPrice(int searchWord)
        {

            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;
            foreach (Game game in gameInstance)
            {
                if (game != null && game.Price < searchWord)
                {
                    Console.WriteLine($"{game.Title}, {game.Price}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
            }

            Console.WriteLine("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }


        void SearchGamesNotes(string searchWord)
        {
            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;
            foreach (Game game in gameInstance)
            {
                if (game != null && game.Notes.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine($"{game.Title}, {game.Notes}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
            }

            Console.WriteLine("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }
    }
}
