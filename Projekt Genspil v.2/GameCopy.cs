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
        public GameCopy(string a, int b) : this(a, b, null) { }
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
            Console.WriteLine($"        Stand: {Condition} -- Pris: {Price} -- Noter: {Notes}");
        }

        public void UpdateItem()
        {
            string updateSelector = null;
            do
            {
                Console.Clear();
                Console.WriteLine(GetCopy());
                Console.WriteLine("\nHvad ønsker du at opdatere?");
                Console.WriteLine("=================");
                Console.WriteLine("(1) Stand");
                Console.WriteLine("(2) Pris");
                Console.WriteLine("(3) Noter");
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
                    case "STAND":
                        Console.Write("Indtast ny stand: ");
                        Condition = UpdateNullcheck();
                        break;
                    case "2":
                    case "PRIS":
                        Console.Write("Indtast ny pris: ");
                        do Int32.TryParse(Console.ReadLine(), out _price); while (Price < 0);
                        break;
                    case "4":
                    case "NOTER":
                        Console.Write("Indtast ny stand: ");
                        Condition = UpdateNullcheck();
                        break;
                    default:
                        Console.WriteLine("Ugyldigt redigerings kriterie");
                        break;

                }
            } while (true);
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
