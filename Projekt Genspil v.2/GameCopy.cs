using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Genspil_v._2
{
    public class GameCopy
    {
        private string _condition;
        private int _price;
        private string _notes;
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
        public GameCopy() { }
        public GameCopy(string a, int b)
        {
            Condition = a;
            Price = b;
        }
        public GameCopy(string a, int b, string c)
        {
            Condition = a;
            Price = b;
            Notes = c;
        }
        public string GetCopy()
        {
            return $"Stand: {Condition} -- Pris: {Price} -- Noter: {Notes}";
        }

        public void ShowCopy()
        {
            //Console.WriteLine($"{Title}, {Version}, {Genre}, {MinPlayers} til {MaxPlayers}, {Condition}, {Price}, {Notes}");
            Console.WriteLine($"        Stand: {Condition} -- Pris: {Price} -- Noter: {Notes}");
        }
    }
}
