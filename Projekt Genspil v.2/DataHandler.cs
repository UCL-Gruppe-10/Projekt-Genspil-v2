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
                    // -- Update from late Sunday: Yup, I could've just used the first array to make a second array/list to contain the information...
                    // -- In my defense, it was written late Friday, and after a long week of many hours of coding. 
                    // -- No time to refactor and test it now, but that's okay. It works. By some miracle. 

                    switch (partType)
                    {
                        case "Spil":
                            gameToBeAdded = new Game();
                            gameToBeAdded.Title = parts[0].Trim().Substring(parts[0].Trim().IndexOf(":") + 1).Trim();
                            gameToBeAdded.Genre = parts[1].Trim().Substring(parts[1].Trim().IndexOf(":") + 1).Trim();
                            int minPlayers = 0, maxPlayers = 0; // In case of something messing up, init to 0
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
}

