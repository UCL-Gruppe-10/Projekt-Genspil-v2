namespace Projekt_Genspil_v._2
{
    public class Game
    {
        public GameVersion tempVersion = new GameVersion();
        public List<GameVersion> versionList = new List<GameVersion>();
        private string _title;
        private string _genre;
        private int _minPlayers;
        private int _maxPlayers;
        int iVersion = -1;

        public Game(string title, string version, string genre, int minPlayers, int maxPlayers, string condition, int price, string notes)
        {
            Title = title;
            Genre = genre;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            GameVersion tempVersion = new GameVersion(version, condition, price, notes);
            iVersion++;
        }
        public Game(string title, string version, string genre, int minPlayers, int maxPlayers, string condition, int price) : this(title, version, genre, minPlayers, maxPlayers, condition, price, null)
        {
            Title = title;
            Genre = genre;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            GameVersion tempVersion = new GameVersion(version, condition, price);
            iVersion++;
        }
        public Game(string title, string genre, int minPlayers, int maxPlayers)
        {
            Title = title;
            Genre = genre;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
        }
        public Game()
        {
        }
        public string Title
        {
            get { return _title; }
            set
            {
                if (value != null && value.Length > 0)
                {
                    _title = value;
                }
            }
        }

        public string Genre
        {
            get { return _genre; }
            set
            {
                if (value != null && value.Length > 0)
                {
                    _genre = value;
                }
            }
        }
        public int MinPlayers
        {
            get { return _minPlayers; }
            set
            {
                if (value > 0)
                {
                    _minPlayers = value;
                }
            }
        }
        public int MaxPlayers
        {
            get { return _maxPlayers; }
            set
            {
                if (value > 0)
                {
                    _maxPlayers = value;
                }
            }
        }
        public List<GameVersion> GenerateVersion(string _version, string _condition, int _price, string _notes)
        {
            GameVersion version = new GameVersion(_version, _condition, _price, _notes);
            versionList.Add(version);
            iVersion++;
            return versionList;
        }
        public List<GameVersion> GenerateVersion(string _version, string _condition, int _price)
        {
            GameVersion version = new GameVersion(_version, _condition, _price);
            versionList.Add(version);
            iVersion++;
            return versionList;
        }
        public List<GameVersion> GenerateVersion(string _version)
        {
            GameVersion version = new GameVersion() { Version = _version };
            versionList.Add(version);
            iVersion++;
            return versionList;
        }

        public void SearchMenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine($"Titel: {Title}");
                Console.WriteLine($"Genre: {Genre}");
                Console.WriteLine($"Spillere: {MinPlayers} til {MaxPlayers}");
                Console.WriteLine("Versioner: ");
                for (int i = 0; i < versionList.Count; i++)
                {
                    Console.WriteLine($"  ID: {i} | {versionList[i].Version} -- Antal sæt: {versionList[i].copyList.Count()}");
                    versionList[i].GetVersion();
                }
                Console.WriteLine("=================");
                Console.WriteLine("(O) Opdater spil");
                Console.WriteLine("(T) Tilgå version");
                Console.WriteLine("(N) Ny version");
                Console.WriteLine("(S) Slet version");
                Console.WriteLine("\n(0) Tilbage");
                Console.WriteLine("=================");
                int idx;
                switch (Console.ReadLine().ToUpper())
                {
                    case "0":
                        return;
                    case "O":
                        UpdateItem();
                        break;
                    case "T":
                        Console.Write("Vælg ID for version at tilgå: ");
                        if (int.TryParse(Console.ReadLine(), out idx))
                            if (idx > -1 && idx < versionList.Count)
                                versionList[idx].SearchMenu();
                        break;
                    case "N":
                        CreateVersion();
                        break;
                    case "S":
                        Console.Write("Vælg ID for version at slette: ");
                        if (int.TryParse(Console.ReadLine(), out idx))
                            if (idx >-1 && idx < versionList.Count)
                                RemoveItem(idx);
                        break;
                }
            } while (true);
        }

        public void UpdateItem()
        {
            string updateSelector = null;
            do
            {
                Console.Clear();
                Console.WriteLine(GetGame());
                Console.WriteLine("\nHvad ønsker du at opdatere?");
                Console.WriteLine("=================");
                Console.WriteLine("(1) Titel");
                Console.WriteLine("(2) Genre");
                Console.WriteLine("(3) Minimum spillere");
                Console.WriteLine("(4) Maximum spillere");
                Console.WriteLine("\n(0) Tilbage");
                do
                {
                    updateSelector = Console.ReadLine().ToUpper();
                } while (updateSelector == null || updateSelector.Length == 0);

                switch (updateSelector)
                {
                    case "0":
                        return;
                    case "1":
                    case "TITEL":
                        Console.Write("Indtast ny titel: ");
                        Title = UpdateNullcheck();
                        break;
                    case "2":
                    case "GENRE":
                        Console.Write("Indtast ny genre: ");
                        Genre = UpdateNullcheck();
                        break;
                    case "3":
                    case "MIN":
                    case "MINIMUM":
                    case "MINIMUM SPILLERE":
                        Console.Write("Indtast nyt minimum antal spillere: ");
                        while (MinPlayers < 1) Int32.TryParse(Console.ReadLine(), out _minPlayers);
                        break;
                    case "4":
                    case "MAX":
                    case "MAXIMUM":
                    case "MAXIMUM SPILLERE":
                        Console.Write("Indtast nyt maksimum antal spillere: ");
                        while (MaxPlayers < 1 && MaxPlayers <= MinPlayers) Int32.TryParse(Console.ReadLine(), out _maxPlayers);
                        break;
                    default:
                        Console.WriteLine("Ugyldigt redigerings kriterie");
                        break;

                }
            } while (true);
        }

        void CreateVersion()
        {
            Console.WriteLine(" - Information til ny version - ");
            Console.Write("Versionens navn: ");
            string tempVersion = Console.ReadLine();
            versionList.Add(new GameVersion(tempVersion));
        }

        void RemoveItem(int idx)
        {
            if (idx >= 0 && idx < versionList.Count)
            {
                Console.WriteLine("Du har slettet følgende: ");
                versionList[idx].ShowVersion();

                versionList.RemoveAt(idx);

                Console.WriteLine("Tryk Enter for at fortsætte...");
                Console.ReadKey();
            }

        }

        public string GetGame()
        {
            return $"Spil: {Title} -- Genre: {Genre} -- Spillere: {MinPlayers} til {MaxPlayers}";
        }

        public void ShowGame()
        {
            Console.WriteLine($"{Title} -- {Genre} -- {MinPlayers} til {MaxPlayers}");
            foreach (GameVersion version in versionList)
            {
                version.ShowVersion();
            }
        }

        public void ListGame()
        {
            for (int i = 0; i < versionList.Count; i++)
            {
                GameVersion version = versionList[i];
                Console.WriteLine($"Version ID: {i} | {version.GetVersion()}");
                version.ListVersion();
            }
        }

        private string UpdateNullcheck()
        {
            string updateInfo = null;
            do
            {
                updateInfo = Console.ReadLine();
            } while (updateInfo == null || updateInfo.Length == 0);

            return updateInfo;
        }
        public void AddCopy(string Condition, int Price, string Notes)
        {
            versionList[iVersion].AddCopy(Condition, Price, Notes);
        }
        public void AddCopy(int index, string Condition, int Price, string Notes)
        {
            versionList[index].AddCopy(Condition, Price, Notes);
        }
    }
}
