using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Genspil_v._2
{
    public class GameVersion
    {
        public GameCopy copy = new GameCopy();
        public List<GameCopy> copyList = new List<GameCopy>();
        private string _version;
        public string Condition;
        public int Price;
        public string Notes;
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
            copy = new GameCopy(condition, price, notes);
            copyList.Add(copy);
        }

        public GameVersion(string version, string condition, int price)
        {
            Version = version;
            copy = new GameCopy(condition, price);
            copyList.Add(copy);
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

        //public string GetCopy(List<GameCopy> copyList)
        //{
        //    foreach (GameCopy copy in copyList)
        //    { 
        //        return copy.GetCopy();
        //    }
        //    return copy.GetCopy();
        //}

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

    }
}
