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
        private string _title;
        private string _version;
        private string _genre;
        private int _minPlayers;
        private int _maxPlayers;
        private string _condition;
        private int _price;
        private string _notes;

        public Game(string title, string version, string genre, int minPlayers, int maxplayers, string condition, int price, string notes)
        {
            Title = title;
            Version = version;
            Genre = genre;
            MinPlayers = minPlayers;
            MaxPlayers = maxplayers;
            Condition = condition;
            Price = price;
            Notes = notes;
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
        public string Version
        {
            get { return _version; }
            set
            {
                if (value != null && value.Length > 0)
                {
                    _version = value;
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
        public string Condition
        {
            get { return _condition; }
            set
            {
                if (value != null && value.Length > 0)
                {
                    _condition = value;
                }
            }
        }
        public int Price
        {
            get { return _price; }
            set
            {
                if (value > 0)
                {
                    _price = value;
                }
            }
        }
        public string Notes
        {
            get { return _notes; }
            set
            {
                if (value != null && value.Length > 0)
                {
                    _notes = value;
                }
            }
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
            Console.WriteLine($"{_title}, {_version}, {_genre}, {_minPlayers} til {_maxPlayers}, {_condition}, {_price}, {_notes}");
        }

        public string ExportGame()
        {
            return $"{Title};{Version};{Genre};{MinPlayers};{MaxPlayers};{Condition};{Price};{Notes}";
        }

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
                        Version = UpdateNullcheck();
                        break;
                    case "5":
                    case "stand":
                        Console.Write("Indtast ny stand: ");
                        Condition = UpdateNullcheck();
                        break;
                    case "6":
                    case "pris":
                        Console.Write("Indtast ny pris: ");
                        while (Price == 0) Int32.TryParse(Console.ReadLine(), out _price);
                        break;
                    case "7":
                    case "noter":
                        Console.Write("Indtast ny noter: ");
                        Notes = UpdateNullcheck();
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
    }
}
