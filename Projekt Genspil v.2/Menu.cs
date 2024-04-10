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
        public List<Game> gameList = new List<Game>();
        DataHandler saveGameList = new DataHandler("GenspilLagerliste.txt");
        int gameItem = 0;
        public int GameItem
        {
            set { gameItem = value; }
            get { return gameItem; }
        }

        public Menu()
        {
            DataHandler loadGameList = new DataHandler("GenspilLagerliste.txt");
            gameList = loadGameList.LoadGames();

            GameItem = loadGameList.Item;
        }

        public void ShowMainMenu()
        {
            Console.WriteLine($"Genspil\n= = = = = = = = = =");

            // Multi-kriterie søg?
            Console.WriteLine("(1) Tilgå spil - Søg, rediger"); // Salg, forespørgsel, venteliste? // rediger funktion herunder

            // Mulighed for at søge efter spil baseret på forskellige kriterier, såsom genre, antal spillere, stand, pris og titel.
            // Skal evt kunne opdatere forspørgsel 
            Console.WriteLine("(2) Opret spil"); // opdater koder til arbejde med persistens. 

            // Mulighed for at se, hvilke spil der er tilgængelige i lageret, og hvilke der er reserveret eller bestilt, og mulighed for en udskrift.
            // Sorteringsmuligheder efter spilnavn, genre og andre relevante kriterier.
            Console.WriteLine("(3) Lagerliste");

            Console.WriteLine("(4) Gem og udskriv lagerliste");
            Console.WriteLine("(5) Print aktivt gameList til konsol");
            Console.WriteLine("(6) Index aktivt gameList til konsol");
            Console.WriteLine("(7) Tjek index 1, 1.1, og 1.1.1");
            Console.WriteLine("(8) Slet Spil m. versioner + eksemplarer");

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
                        saveGameList.SaveGames(gameList);
                        break;
                    case 5:
                        ShowInventory();
                        break;
                    case 6:
                        ListInventory();
                        break;
                    case 7: // Ikke tjekket om virker. Virker ikke uden 'string txt' af en eller anden grund
                        string txt = gameList[1].GetGame();
                        Console.WriteLine(txt); // .GetGame 
                        txt = gameList[1].versionList[1].GetVersion();
                        Console.WriteLine(txt); // .GetVersion
                        txt = gameList[1].versionList[1].copyList[1].GetCopy();
                        Console.WriteLine(txt); // .GetCopy
                        break;
                    case 8:
                        RemoveGame();
                        break;
                    case 0:
                        saveGameList.SaveGames(gameList);
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
                        isItemIdValid = true;
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
            gameList.Add(new Game(tempTitle, tempVersion, tempGenre, tempMinPlayers, tempMaxPlayers, tempCondition, tempPrice, tempNotes));
        }

        public void ShowInventory()
        {
            foreach (Game game in gameList)
            {
                game.ShowGame();
            }
        }



        public void ListInventory()
        {
            for (int i = 0; i < gameList.Count; i++)
            {
                Game game = gameList[i];
                Console.WriteLine($"   Spil ID: {i} | {game.GetGame()}");
                game.ListGame();
                Console.WriteLine();
            }
        }


        void SearchGames() //Mangler oprydning
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
                            //SearchGamesCondition(searchCriteria);
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
            foreach (Game game in gameList) // Her gennemgår den Array'et efter søgeordet, hvor den så vil sortere dem efter kriteriet
            {
                if (game != null && game.Title.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine($"{game.Title}");
                    found = true;
                    RemoveGame();
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
            foreach (Game game in gameList)
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
            foreach (Game game in gameList)
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


        //void SearchGamesCondition(string searchWord)
        //{
        //    Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
        //    bool found = false;
        //    foreach (Game game in gameList)
        //    {
        //        if (game != null && game.Condition.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
        //        {
        //            Console.WriteLine($"{game.Title}, {game.Condition}");
        //            found = true;
        //        }
        //    }

        //    if (!found)
        //    {
        //        Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
        //    }

        //    Console.WriteLine("Tryk Enter for at fortsætte...");
        //    Console.ReadLine();
        //}

        //void SearchGamesPrice(int searchWord)
        //{

        //    Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
        //    bool found = false;
        //    foreach (Game game in gameList)
        //    {
        //        if (game != null && game.Price < searchWord)
        //        {
        //            Console.WriteLine($"{game.Title}, {game.Price}");
        //            found = true;
        //        }
        //    }

        //    if (!found)
        //    {
        //        Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
        //    }

        //    Console.WriteLine("Tryk Enter for at fortsætte...");
        //    Console.ReadLine();
        //}


        void SearchGamesNotes(string searchWord)
        {
            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;
            foreach (Game game in gameList)
            {
                for(int i = 0; i < game.versionList.Count; i++)
                {
                    for (int j = 0; j < game.versionList[i].copyList.Count; j++)
                    {
                        if (game != null && game.versionList[i].copyList[j].Notes.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            Console.WriteLine($"{game.Title}, {game.versionList[i].Version}, {game.versionList[i].copyList[j].GetCopy()}");
                            found = true;
                        }
                    }
                }
                
            }

            if (!found)
            {
                Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
            }

            Console.WriteLine("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }




        void RemoveGame()
        {

            bool isGameIdValid = false;
            {
                while (!isGameIdValid)
                {
                    Console.WriteLine("Vælge spil ved hjælp af ID");
                    var removeGameUserInput = Console.ReadLine();
                    if (removeGameUserInput == "")
                    {
                        Console.WriteLine("Feltet er tomt, prøv igen");
                        continue;
                    }
                    if (int.TryParse(removeGameUserInput, out int gameId) &&
                        gameId >= 0 && gameId < gameList.Count)
                    {

                        gameList.RemoveAt(gameId);                     
                        isGameIdValid = true;  
                        Console.WriteLine($"Spillet er fjernet fra systemet");

                        

                        Console.WriteLine("Tryk på enter for at vende tilbage til Søg spil");
                        Console.ReadKey();
                        SearchGames();

                    }




                }

            }

        }
        
    }
}
