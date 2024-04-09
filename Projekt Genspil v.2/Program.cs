namespace Projekt_Genspil_v._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            //Datahandler saveFile = new Datahandler();

            //menu.ReadtxtFile();
            //menu.SaveIndex();
            menu.ShowInventory();
            //menu.ShowMainMenu();
            menu.SelectMainMenu();
        }
    }
}
