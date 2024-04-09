using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Genspil_v._2
{
    public class Game
    {
        public GameVersion tempVersion = new GameVersion();
        public List<GameVersion> versions = new List<GameVersion>();
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
            GameVersion tempVersion = new GameVersion() { Version = version, Condition = condition, Price = price, Notes = notes };
            iVersion++;
        }
        public Game(string title, string version, string genre, int minPlayers, int maxPlayers, string condition, int price)
        {
            Title = title;
            Genre = genre;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            GameVersion tempVersion = new GameVersion() { Version = version, Condition = condition, Price = price };
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
        public List<GameVersion> CreateVersion(string _version, string _condition, int _price, string _notes)
        {
            GameVersion version = new GameVersion(_version, _condition, _price, _notes);
            versions.Add(version);
            iVersion++;
            return versions;
        }
        public List<GameVersion> CreateVersion(string _version, string _condition, int _price)
        {
            GameVersion version = new GameVersion() { Version = _version, Condition = _condition, Price = _price };
            versions.Add(version);
            iVersion++;
            return versions;
        }
        public List<GameVersion> CreateVersion(string _version)
        {
            GameVersion version = new GameVersion() { Version = _version };
            versions.Add(version);
            iVersion++;
            return versions;
        }

        /*public void CreateGame()  // Ligger nu i Menu.cs
        {
            Console.WriteLine(" - Information til nyt spil - ");
            Console.Write("Navn: ");
            Title = Console.ReadLine();
            Console.Write("Version: ");
            Version = Console.ReadLine();
            Console.Write("Genre: ");
            Genre = Console.ReadLine();
            Console.Write("Minimum spillere: ");
            MinPlayers = int.Parse(Console.ReadLine());
            Console.Write("Maximum spillere: ");
            MaxPlayers = int.Parse(Console.ReadLine());
            Console.Write("Stand: ");
            Condition = Console.ReadLine();
            Console.Write("Pris: ");
            Price = int.Parse(Console.ReadLine());
            Console.Write("Notes: ");
            Notes = Console.ReadLine();
        }*/

        public void ShowGame()
        {
            //Console.WriteLine($"{Title}, {Version}, {Genre}, {MinPlayers} til {MaxPlayers}, {Condition}, {Price}, {Notes}");
            Console.WriteLine($"{_title}, {_genre}, {_minPlayers} til {_maxPlayers}");
        }

        //public string ExportGame(List<GameVersion> versions)
        //{
            
        //    foreach(List<GameVersion>version in versions)
        //    {
        //        versions.GetVersion();
        //        versions.ExportGame(List < GameVersion > versions);
        //    }
        //    return $"{Title};{Genre};{MinPlayers};{MaxPlayers};";
        //}

        public void UpdateGame()
        {
            ShowGame();
            string updateSelector = null;
            bool updateContinue = true;
            do
            {
                Console.WriteLine("Hvad ønsker du at opdatere? 0 for at afslutte.");
                do
                {
                    updateSelector = Console.ReadLine().ToLower();
                } while (updateSelector == null || updateSelector.Length == 0);

                switch (updateSelector)
                {
                    case "0":
                        updateContinue = false;
                        break;
                    case "1":
                    case "titel":
                        Console.Write("Indtast ny titel: ");
                        Title = UpdateNullcheck();
                        break;
                    case "2":
                    case "genre":
                        Console.Write("Indtast ny genre: ");
                        Genre = UpdateNullcheck();
                        break;
                    case "3":
                    case "spillere":
                        Console.Write("Indtast nyt minimum antal spillere: ");
                        while (MinPlayers < 1) Int32.TryParse(Console.ReadLine(), out _minPlayers);
                        Console.Write("Indtast nyt maksimum antal spillere: ");
                        while (MaxPlayers < 1 && MaxPlayers < MinPlayers) Int32.TryParse(Console.ReadLine(), out _maxPlayers);
                        break;
                    case "4":
                    case "version":
                        Console.Write("Indtast ny version: ");
                        //Version = UpdateNullcheck();
                        break;
                    case "5":
                    case "stand":
                        Console.Write("Indtast ny stand: ");
                        //Condition = UpdateNullcheck();
                        break;
                    case "6":
                    case "pris":
                        Console.Write("Indtast ny pris: ");
                        //while (Price == 0) Int32.TryParse(Console.ReadLine(), out _price);
                        break;
                    case "7":
                    case "noter":
                        Console.Write("Indtast ny noter: ");
                        //Notes = UpdateNullcheck();
                        break;
                    default:
                        Console.WriteLine("Ugyldigt redigerings kriterie");
                        break;

                }
            } while (updateContinue == true);
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
            versions[iVersion].AddCopy(Condition, Price, Notes);
        }
        public void AddCopy(int index, string Condition, int Price, string Notes)
        {
            versions[index].AddCopy(Condition, Price, Notes);
        }
    }
}
