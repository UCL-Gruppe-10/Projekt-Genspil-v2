using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Genspil_v._2
{
    public class GameVersion
    {
        public GameCopy tempCopy = new GameCopy();
        public List<GameCopy> copyList = new List<GameCopy>();
        private string _version;
        //public string Condition;
        //public int Price;
        //public string Notes;
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
        public GameVersion() { }

        public GameVersion(string version)
        {
            Version = version;
        }

        public GameVersion(string version, string condition, int price, string notes)
        {
            Version = version;
            tempCopy = new GameCopy(condition, price, notes);
            copyList.Add(tempCopy);
        }

        public GameVersion(string version, string condition, int price)
        {
            Version = version;
            tempCopy = new GameCopy(condition, price);
            copyList.Add(tempCopy);
        }

        public string GetVersion()
        {
            return $"Version: {Version}";
        }

        public void ShowVersion()
        {
            //Console.WriteLine($"{Title}, {Version}, {Genre}, {MinPlayers} til {MaxPlayers}, {Condition}, {Price}, {Notes}");
            Console.WriteLine($"    {_version}");
            foreach (GameCopy copy in copyList)
            {
                copy.ShowCopy();
            }
        }

        public void ListVersion()
        {
            for (int i = 0; i < copyList.Count; i++)
            {
                GameCopy copy = copyList[i];
                if (i == 0)
                    Console.WriteLine($"    Sæt ID: {i} | {copy.GetCopy()}");
                else
                    Console.WriteLine($"            {i} | {copy.GetCopy()}");
            }
        }

        public List<GameCopy> SortListByCondition(List<GameCopy> list)
        {
            
            list.Sort((x, y) =>
            {
                int ret = string.Compare(x.Condition, y.Condition);
                return ret;
            });
            
            return list ;
        }

        public void AddCopy(string condition, int price, string notes)
        {
            GameCopy copy = new GameCopy(condition, price, notes);
            copyList.Add(copy);
        }

        public void AddCopy(string condition, int price)
        {
            GameCopy copy = new GameCopy(condition, price);
            copyList.Add(copy);
        }
        public void UpdateCopy(int idx)
        {
            bool Continue = true;
            int valg;
            string Input;
            int nyPris;
            while (Continue == true)
            {
                Input = null;
                nyPris = 0;
                valg = 0;
                Console.Clear();
                Console.WriteLine($"{Version} \n{idx}: {copyList[idx].GetCopy()}");
                Console.WriteLine("Hvad vil du opdatere?");
                Console.WriteLine("1. Condition");
                Console.WriteLine("2. Pris");
                Console.WriteLine("3. Noter");
                Console.WriteLine("0. Afslut");
                Int32.TryParse(Console.ReadLine(), out valg);
                switch (valg)
                {
                    case 1:
                        Console.Write($"Condition: {copyList[idx].Condition} | Ny condition: ");
                        Input = Console.ReadLine();
                        if (Input != null)
                        {
                            copyList[idx].Condition = Input;
                            Console.WriteLine($"Ny condition {copyList[idx].Condition} gemt");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Forkert input");
                            Console.ReadKey();
                        }
                        break;
                    case 2:
                        Console.Write($"Pris: {copyList[idx].Price} | Ny pris: ");
                        Int32.TryParse(Console.ReadLine(), out nyPris);
                        if (nyPris != 0)
                        {
                            copyList[idx].Price = nyPris;
                            Console.WriteLine($"Ny pris {copyList[idx].Price} gemt");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Forkert input");
                            Console.ReadKey();
                        }
                        break;
                    case 3:
                        if (copyList[idx].Notes != null)
                            Console.Write($"Note: {copyList[idx].Notes} | ");
                        Console.Write("Ny note: ");
                        Input = Console.ReadLine();
                        copyList[idx].Notes = Input;
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.WriteLine("Rettelser gemt");
                        Continue = false;
                        break;

                }
            }
        }
        void SearchGamesNotes(string searchWord)
        {
            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;
            int count = -1;
            foreach (GameCopy game in copyList)
            {
                count++;
                if (game != null && game.Notes.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine($"I: {count}, {Version}, {game.Notes}");
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
            int count = -1;
            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false;
            foreach (GameCopy game in copyList)
            {
                count++;
                if (game != null && game.Price < searchWord)
                {
                    Console.WriteLine($"i: {count}, {Version}, {game.Price}");
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
            foreach (GameCopy game in copyList)
            {
                if (game != null && game.Condition.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine($"{Version}, {game.Condition}");
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
        public string ExportVersion()
        {
            string export = " ";
            for (int i = -1; i < copyList.Count; i++)
            {
                if (i == -1)
                    export = GetVersion();
                else if (i >= 0)
                    export = copyList[i].GetCopy();
                return export;
            }
            return export;
        }
    }
}
