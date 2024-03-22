namespace Projekt_Genspil_v._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game spil = new Game();
            Menu menu = new Menu();

            menu.ShowMenu();
            menu.SelectMenuItem();
            menu.ShowInventory();
        }
    }
}
