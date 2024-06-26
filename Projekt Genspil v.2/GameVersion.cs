﻿using System.Collections.Generic;

namespace Projekt_Genspil_v._2
{
    public class GameVersion
    {
        public GameCopy tempCopy = new GameCopy();
        public List<GameCopy> copyList = new List<GameCopy>();
        private string _version;
        public string tempCondition;
        public int tempPrice;
        public string tempNotes;
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
        public GameVersion(string version, string condition, int price) : this(version, condition, price, null) { }
        public GameVersion(string version, string Condition, int price, string notes)
        {
            Version = version;
            tempCondition = Condition;
            tempPrice = price;
            tempNotes = notes;
            tempCopy = new GameCopy(tempCondition, tempPrice, tempNotes);
            copyList.Add(tempCopy);
            SortListByCondition(copyList);
        }
        public string GetVersion()
        {
            return $"Version: {Version}";
        }

        public void ShowVersion()
        {
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

            return list;
        }

        public void AddCopy(string condition, int price, string notes)
        {
            GameCopy copy = new GameCopy(condition, price, notes);
            copyList.Add(copy);
            SortListByCondition(copyList);
        }

        public void AddCopy(string condition, int price)
        {
            GameCopy copy = new GameCopy(condition, price);
            copyList.Add(copy);
            SortListByCondition(copyList);
        }

        public void SearchMenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine($"Du har valgt {Version}");
                Console.WriteLine("Spil sæt: ");
                for (int i = 0; i < copyList.Count; i++)
                {
                    GameCopy copy = copyList[i];
                    Console.WriteLine($"  ID: {i} | Stand: {copy.Condition} -- Pris: {copy.Price} -- Noter: {copy.Notes}");
                    copyList[i].GetCopy();
                }
                Console.WriteLine("=================");
                Console.WriteLine("(O) Opdater version");
                Console.WriteLine("(T) Tilgå sæt");
                Console.WriteLine("(N) Nyt sæt");
                Console.WriteLine("(S) Slet sæt");
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
                        Console.Write("Vælg ID for sæt at tilgå: ");
                        if (int.TryParse(Console.ReadLine(), out idx))
                            if (idx > -1 && idx < copyList.Count)
                                copyList[idx].UpdateItem();
                        break;
                    case "N":
                        CreateCopy();
                        break;
                    case "S":
                        Console.Write("Vælg ID for sæt at slette: ");
                        if (int.TryParse(Console.ReadLine(), out idx))
                            if (idx > -1 && idx < copyList.Count)
                                RemoveItem(idx);
                        break;
                }
            } while (true);
        }

        public void UpdateItem()
        {
            Console.WriteLine(GetVersion());
            Console.WriteLine("\nOpdater version");
            Console.WriteLine("=================");
            Console.Write("Indtast ny titel: ");
            Version = UpdateNullcheck();
        }

        void CreateCopy()
        {
            Console.WriteLine(" - Information til nyt sæt - ");
            Console.Write("Stand: ");
            string tempCondition = Console.ReadLine();
            int tempPrice;
            Console.Write("Pris: ");
            while (true)
            {
                Console.Write("Minimum spillere: ");
                if (int.TryParse(Console.ReadLine(), out tempPrice))
                    break;
            }
            Console.Write("Noter: ");
            string tempNotes = Console.ReadLine();
            copyList.Add(new GameCopy(tempCondition, tempPrice, tempNotes));
        }

        void RemoveItem(int idx)
        {
            if (idx >= 0 && idx < copyList.Count)
            {
                Console.WriteLine("Du har slettet følgende: ");
                copyList[idx].ShowCopy();

                copyList.RemoveAt(idx);

                Console.WriteLine("Tryk Enter for at fortsætte...");
                Console.ReadKey();
            }

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
