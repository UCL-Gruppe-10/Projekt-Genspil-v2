namespace Projekt_Genspil_v._2
{
    internal class Menu
    {
        public List<Game> gameList = new List<Game>();
        DataHandler dataHandler;
        int gameItem = 0;
        public int GameItem
        {
            set { gameItem = value; }
            get { return gameItem; }
        }

        public Menu()
        {
            dataHandler = new DataHandler("GenspilLagerliste.txt");
            gameList = dataHandler.LoadGames();

            GameItem = dataHandler.Item;
        }

        public void ShowMainMenu()
        {
            Console.WriteLine($"Genspil\n= = = = = = = = = =");
            Console.WriteLine("(1) Tilgå spil - Søg, rediger, opret version/sæt"); 
            Console.WriteLine("(2) Opret spil"); 
            Console.WriteLine("(3) Lagerliste");
            Console.WriteLine("(4) Gem lagerliste");
            Console.WriteLine("\n(6) TEST - Index aktivt gameList til konsol");
            Console.WriteLine("(7) TEST - Multi-kriterie søgefunktion");

            Console.WriteLine("\n(0) Gem lager og afslut program");
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
                        ChooseIndex();
                        break;
                    case 2:
                        CreateGame();
                        break;
                    case 3:
                        ShowInventory();
                        break;
                    case 4:
                        Console.WriteLine("Gemmer lagerliste ...");
                        dataHandler.SaveGames(gameList);
                        Console.WriteLine("... Lagerliste gemt!");
                        break;
                    case 6:
                        ListInventory();
                        break;
                    case 7:
                        SearchGamesByMultipleCriterias();
                        break;
                    case 0:
                        dataHandler.SaveGames(gameList);
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

        public void ShowInventory()
        {
            foreach (Game game in gameList)
            {
                game.ShowGame();
                Console.WriteLine();
            }
        }

        public void ListInventory()
        {
            Console.Clear();
            for (int i = 0; i < gameList.Count; i++)
            {
                Game game = gameList[i];
                Console.WriteLine($"   Spil ID: {i} | {game.GetGame()}");
                game.ListGame();
                Console.WriteLine();
            }
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
                        return;
                    case 2:
                        Console.Write("Søg efter genre: ");
                        searchCriteria = Console.ReadLine();
                        if (searchCriteria != null && searchCriteria.Length > 0)
                        {
                            SearchGamesGenre(searchCriteria);
                        }
                        return;
                    case 3:
                        Console.Write("Søg efter spillere: ");
                        searchCriteria = Console.ReadLine();
                        if (searchCriteria != null && int.Parse(searchCriteria) > 0)
                        {
                            SearchGamesPlayers(int.Parse(searchCriteria));
                        }
                        return;
                    case 4:
                        Console.Write("Søg efter stand: ");
                        searchCriteria = Console.ReadLine();
                        if (searchCriteria != null && searchCriteria.Length == 1)
                        {
                            SearchGamesCondition(searchCriteria);
                        }
                        return;
                    case 5:
                        Console.Write("Søg efter pris: ");
                        searchCriteria = Console.ReadLine();
                        if (searchCriteria != null && int.Parse(searchCriteria) > 0)
                        {
                            SearchGamesPrice(int.Parse(searchCriteria));
                        }
                        return;
                    case 6:
                        Console.Write("Søg efter noter: ");
                        searchCriteria = Console.ReadLine();
                        if (searchCriteria != null && searchCriteria.Length > 0)
                        {
                            SearchGamesNotes(searchCriteria);
                        }
                        return;
                    default:
                        Console.WriteLine("Søgekriterie udenfor index.\nTryk Enter for at prøve igen.");
                        Console.ReadLine();
                        break;

                }
            }

        }

        void ChooseIndex()
        {
            do
            {
                Console.WriteLine("=================");
                Console.WriteLine("(T) Tilgå spil");
                Console.WriteLine("(N) Nyt spil");
                Console.WriteLine("(S) Slet spil");
                Console.WriteLine("\n(0) Tilbage");
                Console.WriteLine("=================");
                int idx;
                switch (Console.ReadLine().ToUpper())
                {
                    case "0":
                        return;
                    case "T":
                        Console.Write("Vælg ID for spillet du ønsker at tilgå: ");
                        if (int.TryParse(Console.ReadLine(), out idx))
                            if (idx > -1 && idx < gameList.Count)
                                gameList[idx].SearchMenu();
                        return;
                    case "N":
                        CreateGame();
                        return;
                    case "S":
                        Console.Write("Vælg ID for spillet du ønsker at slette: ");
                        if (int.TryParse(Console.ReadLine(), out idx))
                            if (idx > -1 && idx < gameList.Count)
                                RemoveItem(idx);
                        return;
                    default:
                        break;
                }
            } while (true);
        }

        void CreateGame()
        {
            Console.WriteLine(" - Information til nyt spil - ");
            Console.Write("Navn: ");
            string tempTitle = Console.ReadLine();
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
            gameList.Add(new Game(tempTitle, tempGenre, tempMinPlayers, tempMaxPlayers));
        }

        void RemoveItem(int idx)
        {
            if (idx >= 0 && idx < gameList.Count)
            {
                Console.WriteLine("Du har slettet følgende: ");
                gameList[idx].ShowGame();

                gameList.RemoveAt(idx);

                Console.WriteLine("Tryk Enter for at fortsætte...");
                Console.ReadKey();
            }
        }

        void SearchGamesTitle(string searchWord)
        {
            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;

            for (int i = 0; i < gameList.Count; i++)
            {
                if (gameList[i] != null && gameList[i].Title.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    int copyCount = 0;
                    for (int j = 0; j < gameList[i].versionList.Count; j++) 
                    {
                        for (int k = 0; k < gameList[i].versionList[j].copyList.Count; k++)
                            copyCount++;
                    }
                    Console.WriteLine($"   Spil ID: {i} | Titel: {gameList[i].Title} -- Antal sæt: {copyCount}");
                    found = true;
                }
            }
            // Vis en besked, hvis der ikke er nogen søgeresultater
            if (!found)
            {
                Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
            }
        }

        void SearchGamesGenre(string searchWord)
        {
            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;

            for (int i = 0; i < gameList.Count; i++)
            {
                if (gameList[i] != null && gameList[i].Genre.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    int copyCount = 0;
                    for (int j = 0; j < gameList[i].versionList.Count; j++)
                    {
                        for (int k = 0; k < gameList[i].versionList[j].copyList.Count; k++)
                            copyCount++;
                    }
                    Console.WriteLine($"   Spil ID: {i} | Titel: {gameList[i].Title} -- Genre: {gameList[i].Genre} -- Antal sæt: {copyCount}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
            }
        }

        void SearchGamesPlayers(int searchWord)
        {
            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;

            for (int i = 0; i < gameList.Count; i++)
            {
                if (gameList[i] != null && gameList[i].MinPlayers < searchWord && gameList[i].MaxPlayers > searchWord)
                {
                    int copyCount = 0;
                    for (int j = 0; j < gameList[i].versionList.Count; j++)
                    {
                        for (int k = 0; k < gameList[i].versionList[j].copyList.Count; k++)
                            copyCount++;
                    }
                    Console.WriteLine($"   Spil ID: {i} | Titel: {gameList[i].Title} -- Spillere: {gameList[i].MinPlayers} til {gameList[i].MaxPlayers} -- Antal sæt: {copyCount}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
            }
        }

        void SearchGamesCondition(string searchWord)
        {
            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;

            for (int i = 0; i < gameList.Count; i++)
            {
                for (int j = 0; j < gameList[i].versionList.Count; j++)
                {
                    for (int k = 0; k < gameList[i].versionList[j].copyList.Count; k++)
                    {
                        if (gameList[i].versionList[j].copyList[k] != null && gameList[i].versionList[j].copyList[k].Condition.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            Console.WriteLine($"    Sæt ID: {i}, {j}, {k} | Titel: {gameList[i].Title} -- Version: {gameList[i].versionList[j].Version} -- Stand: {gameList[i].versionList[j].copyList[k].Condition}");
                            found = true;
                        }
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
            }
        }

        void SearchGamesPrice(int searchWord)
        {

            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;

            for (int i = 0; i < gameList.Count; i++)
            {
                for (int j = 0; j < gameList[i].versionList.Count; j++)
                {
                    for (int k = 0; k < gameList[i].versionList[j].copyList.Count; k++)
                    {
                        if (gameList[i].versionList[j].copyList[k] != null && gameList[i].versionList[j].copyList[k].Price < searchWord)
                        {
                            Console.WriteLine($"    Sæt ID: {i}, {j}, {k} | Titel: {gameList[i].Title} -- Version: {gameList[i].versionList[j].Version} -- Pris: {gameList[i].versionList[j].copyList[k].Price}");
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

        void SearchGamesNotes(string searchWord)
        {
            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;

            for (int i = 0; i < gameList.Count; i++)
            {
                for (int j = 0; j < gameList[i].versionList.Count; j++)
                {
                    for (int k = 0; k < gameList[i].versionList[j].copyList.Count; k++)
                    {
                        if (gameList[i].versionList[j].copyList[k] != null && gameList[i].versionList[j].copyList[k].Notes.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            Console.WriteLine($"    Sæt ID: {i}, {j}, {k} | Titel: {gameList[i].Title} -- Version: {gameList[i].versionList[j].Version} -- Stand: {gameList[i].versionList[j].copyList[k].Notes}");
                            found = true;
                        }
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
            }
        }


        void SearchGamesByMultipleCriterias()
        {
            Console.WriteLine("Indtast søgekriterier (Enter for at springe kriteriet over):");

            Console.Write("Titel: ");
            string? title = Console.ReadLine();

            Console.Write("Genre: ");
            string? genre = Console.ReadLine();

            Console.Write("Minimum antal spillere: ");
            int? minPlayers = TryParseNullableInt(Console.ReadLine());

            Console.Write("Maksimum antal spillere: ");
            int? maxPlayers = TryParseNullableInt(Console.ReadLine());

            Console.Write("Stand: ");
            string? condition = Console.ReadLine();

            Console.Write("Pris: ");
            int? price = TryParseNullableInt(Console.ReadLine());

            Console.Write("Noter: ");
            string? notes = Console.ReadLine();

            Console.WriteLine($"Resultatet af din søgning med flere kriterier:");
            bool found = false;

            foreach (Game game in gameList)  // Check imod SearchGamesCondition() -- Lav 'FOR' loops for at finde index -- Min/Max Antal Spillere til 'bare' Antal Spillere
            {
                if (game != null &&
                    (title == null || game.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (genre == null || game.Genre.IndexOf(genre, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (!minPlayers.HasValue || game.MinPlayers >= minPlayers.Value) &&
                    (!maxPlayers.HasValue || game.MaxPlayers <= maxPlayers.Value) &&
                    (condition == null || game.versionList.Any(v => v.copyList.Any(c => c.Condition.IndexOf(condition, StringComparison.OrdinalIgnoreCase) >= 0))) &&
                    (!price.HasValue || game.versionList.Any(v => v.copyList.Any(c => c.Price == price.Value))) &&
                    (notes == null || game.versionList.Any(v => v.copyList.Any(c => c.Notes.IndexOf(notes, StringComparison.OrdinalIgnoreCase) >= 0))))
                {
                    Console.WriteLine($"{game.GetGame()}");
                    foreach (GameVersion version in game.versionList)
                    {
                        foreach (GameCopy copy in version.copyList)
                        {
                            if (condition != null && copy.Condition.IndexOf(condition, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                Console.WriteLine($"Version: {version.Version} -- Stand: {copy.Condition} -- Pris: {copy.Price} -- Noter: {copy.Notes}");
                            }
                        }
                    }
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Ingen søgeresultater fundet.");
            }

            Console.WriteLine("Tryk Enter for at fortsætte...");
            Console.ReadLine();
        }

        int? TryParseNullableInt(string input)
        {
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            return null;
        }
    }
}
