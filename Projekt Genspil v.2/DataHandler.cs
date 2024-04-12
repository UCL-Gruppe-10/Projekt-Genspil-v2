using System.Reflection;

namespace Projekt_Genspil_v._2
{
    internal class DataHandler
    {
        private string docPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private int _item = -1;
        public int Item
        {
            get { return _item; }
            set { _item = value; }
        }

        private string _dataFileName;
        public string DataFileName
        {
            get { return _dataFileName; }
        }

        public DataHandler(string dataFileName)
        {
            _dataFileName = dataFileName;
        }

        public List<Game> LoadGames()
        {
            List<Game> gameList = new List<Game>();
            using (StreamReader sr = new StreamReader(Path.Combine(docPath, DataFileName)))
            {
                Game gameToBeAdded = null;

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(new string[] { " -- " }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 0)
                        continue;   // Continues the loop, even if an empty line is found. Should never happen, but still!
                    else if (parts[0] == "-")
                    {
                        // Using specific spacing to signify new game
                        if (gameToBeAdded != null)
                        {
                            gameToBeAdded.ShowGame();
                            gameList.Add(gameToBeAdded);
                        }
                        continue;
                    }
                    else if (parts[0].Length > 17)
                    {
                        if (parts[0].Substring(0, 18) == "Lagerliste Genspil")
                            continue;
                    }

                    // Pull out the type of class we want to add to
                    string partType = parts[0].Trim().Substring(0, parts[0].Trim().IndexOf(":"));

                    // There is DEFINITELY a better way of doing this
                    // Our old way of doing it was more elegant, but left the stock list messy, and with weird spacings everywhere
                    switch (partType)
                    {
                        case "Spil":
                            gameToBeAdded = new Game();
                            gameToBeAdded.Title = parts[0].Trim().Substring(parts[0].Trim().IndexOf(":") + 1).Trim();
                            gameToBeAdded.Genre = parts[1].Trim().Substring(parts[1].Trim().IndexOf(":") + 1).Trim();
                            int minPlayers = 0, maxPlayers = 0;
                            string[] playerRange = parts[2].Trim().Substring(parts[2].Trim().IndexOf(":") + 1).Split(new string[] { "til" }, StringSplitOptions.RemoveEmptyEntries);
                            int.TryParse(playerRange[0].Trim(), out minPlayers);
                            gameToBeAdded.MinPlayers = minPlayers;
                            int.TryParse(playerRange[1].Trim(), out maxPlayers);
                            gameToBeAdded.MaxPlayers = maxPlayers;
                            break;
                        case "Version":
                            string versionToBeAdded = parts[0].Trim().Substring(parts[0].Trim().IndexOf(":") + 1).Trim();
                            gameToBeAdded.GenerateVersion(versionToBeAdded);    // Each version needs its own list within a game list
                            break;
                        case "Stand":
                            string condition = parts[0].Trim().Substring(parts[0].Trim().IndexOf(":") + 1).Trim();
                            int price = 0;
                            int.TryParse(parts[1].Trim().Substring(parts[1].Trim().IndexOf(":") + 1).Trim(), out price);
                            string notes = parts[2].Trim().Substring(parts[2].Trim().IndexOf(":") + 1).Trim();
                            gameToBeAdded.AddCopy(condition, price, notes); // Each copy of a game needs its own list within the version list, within the game list
                            break;
                        default:
                            // Catch-all for some cases we use, namely stock list intro, and '-' line spacings
                            break;


                    }


                }
            }
            Console.Clear();    // I've been using 'Console.WriteLine's to assist debugging, and it general it seems reasonable to print while loading
            return gameList;
        }

        public void SaveGames(List<Game> inputGame)
        {
            DateTime date = DateTime.Now;

            using (StreamWriter sw = new StreamWriter(Path.Combine(docPath, DataFileName), false))
            {
                sw.WriteLine($"Lagerliste Genspil - {date}");   // Using full to check saving functionality, but ToShortDateString is good too
                //sw.WriteLine($"Lagerliste Genspil - {date.ToShortDateString()}");
                foreach (Game game in inputGame)
                {
                    sw.WriteLine(game.GetGame());
                    for (int i = 0; i < game.versionList.Count; i++)
                    {
                        sw.WriteLine(game.versionList[i].GetVersion());
                        for (int j = 0; j < game.versionList[i].copyList.Count; j++)
                        {
                            sw.WriteLine(game.versionList[i].copyList[j].GetCopy());
                        }
                    }
                    sw.WriteLine("-");  // Special spacing used for loading. Also is more pleasing within the stock list
                }
            }
        }
    }










































    //public void PrintList(Game[] game)
    //{
    //    using (StreamWriter sw = new StreamWriter(Path.Combine(path, "GenspilLagerliste.txt"), false, Encoding.ASCII))
    //    {
    //        DateTime date = DateTime.Now;
    //        string dato = date.ToShortDateString();

    //        sw.WriteLine($"Lagerliste Genspil - {dato}");
    //        for (int i = 0; i < 50; i++)
    //        {
    //            // Tester om der er en titel i arrayet, og er der det, skrives titel, genre, og antal spillere i samme string.
    //            if (game[i] != null)
    //            {
    //                string title = "";
    //                sw.WriteLine(game[i].GetTitle(Title));
    //                //saveTitle[i] = $"Titel: {gameTitle[i].title} : Genre: {gameTitle[i].genre} : Minimum spillere: {gameTitle[i].players[0]} : maximum: {gameTitle[i].players[1]} ";
    //                for (int j = 0; j < 10; j++)
    //                {
    //                    // Tester om der er en version, og i så fald, gemmer versionen i en string
    //                    if (game[i].version[j] != null)
    //                    {
    //                        sw.WriteLine("Version: " + game[i].version[j]);
    //                        for (int k = 0; k < 20; k++)
    //                        {
    //                            // Tester om der er en pris, og gemmer i så fald udgavens stand, pris, og evt. noter i samme string
    //                            if (game[i].price[j, k] != 0)
    //                            {
    //                                sw.WriteLine($"Stand: {game.Condiditon} : Pris: {game[i].price[j, k]} : Note: {game[i].notes[j, k]}");
    //                            }
    //                            else
    //                                break;
    //                        }
    //                    }
    //                    else
    //                        break;
    //                }
    //            }
    //            else
    //                break;
    //        }
    //        Console.WriteLine("Filer gemt til fil.\nFarvel.");
    //    }

    //}
    //public Game ReadList(Game[] game, int gameItem)
    //{
    //    string line = "123";
    //    string[] save;
    //    try
    //    {
    //        int t = -1, v = -1, c = -1;
    //        // Pass the file path and file to the StreamReader constructor.
    //        StreamReader sr = new StreamReader(Path.Combine(path, "GenspilLagerliste.txt"));

    //        while (line != null)
    //        {

    //            // Read the first line of text.
    //            line = sr.ReadLine();
    //            // Continue to read, until end of file.
    //            if (line == null)
    //                break;
    //            // If line contains "Title", create new instance of Game, and increment gameItem.
    //            if (line.Contains("Titel: "))
    //            {
    //                game[gameItem] = new Game();
    //                gameItem++;
    //                t++;
    //                // increment "t" for title, and reset "v" for version.
    //                v = -1;
    //                // Write the title index, split the line to save array, for each " : "
    //                Console.WriteLine($"index: {t} : {line}");
    //                save = line.Split(":");
    //                // save title, genre, and num. players in corresponding places in the game class.
    //                game.Title(save[1]);
    //                game.Genre(save[3]);
    //                Int32.TryParse(save[5], out game.MinPlayers);
    //                Int32.TryParse(save[7], out game.MaxPlayers);
    //            }
    //            // If line contains "version:" write the corresponding index number.
    //            else if (line.Contains("Version:"))
    //            {
    //                // Split the line into the save array, and assign the value of
    //                // "version" on its corresponding place in the game class.
    //                v++;
    //                // Increment "E", and reset "c" for copy 
    //                c = -1;
    //                save = line.Split(":");
    //                game[t].SetVersion(save[1]);
    //                game[t].i = v;
    //                Console.WriteLine($"Index: {t}, {v} : {line}");
    //            }
    //            // if line contains "pris:" write the corresponding index and line.
    //            else if (line.Contains("Pris: "))
    //            {
    //                // increment "c" for copy, and split the line in the save array.
    //                c++;
    //                game[t].j[v] = c;
    //                save = line.Split(":");
    //                // trim save[1], so that it contains one character, convert to char,
    //                // and save in corresponding var in game class.
    //                save[1].Trim();
    //                char condition = Convert.ToChar(save[1].Trim());
    //                game[t].gameCondiditon[v, c] = condition;
    //                Int32.TryParse(save[3], out game[t].price[v, game[t].j[v]]);
    //                if (save.Contains(save[5]) && save[5] != null)
    //                    game[t].notes[v, game[t].j[v]] = save[5];
    //                //saveCopy[d, e, f] = line;
    //                Console.WriteLine($"Index: {t}, {v}, {c} : {line}");
    //            }
    //            // if nothing else fits, write the line, close the file, and ask for reaction from user. 
    //            else
    //            {
    //                Console.WriteLine(line);
    //            }
    //        }
    //        sr.Close();
    //        Console.ReadLine();
    //        //menu.GameItem = gameItem;
    //        this.Item = gameItem;
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine("Exception: " + e.Message);
    //    }
    //    finally { Console.WriteLine("Executing finally block"); }

    //    return this.game;
    //}
}

