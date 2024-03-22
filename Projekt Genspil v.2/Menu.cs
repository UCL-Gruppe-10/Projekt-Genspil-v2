using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Genspil_v._2
{
    internal class Menu
    {
        public Game[] gameTitle = new Game[50];
        int gameItem = 0;
        string[] saveTitle = new string[50];
        string[,] saveVersion = new string[50, 50];
        string[,,] saveCopy = new string[50, 50, 50];

        public void ShowMenu() // Printer den primære menu
        {
            //Console.Clear();
            Console.WriteLine("Genspil");
            Console.WriteLine("-----------------------------------\n\n");
            Console.WriteLine("(1) Soeg spil");
            Console.WriteLine("(2) Opret spil");
            Console.WriteLine("\n(Afslut programmet - tast 0)");
        }
        public void SelectMenuItem()
        {
            int menuItem = -1;
            do
            {
                string valg = Console.ReadLine();
                Int32.TryParse(valg, out menuItem);
                switch (menuItem)
                {
                    case 1:
                        Console.WriteLine("Søg spil");
                        break;
                    case 2:
                        Console.Clear();
                        gameTitle[gameItem] = new Game();
                        gameTitle[gameItem].CreateGame();
                        gameItem++;
                        break;
                    case 0:
                        PrintGamesToFile();
                        Console.WriteLine("Farvel");
                        break;
                    default:
                        Console.Write("Fejlinput, prøv igen:");
                        break;
                }
                ShowInventory();
                ShowMenu();
            } while (menuItem != 0);
        }
        public void ShowInventory()
        {
            Console.Clear();
            for (int i = 0; i < 50; i++)
            {
                if (gameTitle[i] != null)
                {
                    gameTitle[i].GetGame();
                }
                else
                    break;
            }
        }
        public void PrintGamesToFile()
        {
            for(int i = 0; i < 50;i++)
            {
                // Tester om der er en tittel i arrayet, og er der det, skrives tittel, genre, og antal spillere i samme string.
                if (gameTitle[i] != null)
                {
                    saveTitle[i] = $"Tittel: {gameTitle[i].title} : Genre: {gameTitle[i].genre} : Minimum spillere: {gameTitle[i].players[0]} : maximum: {gameTitle[i].players[1]} ";
                    for (int j = 0; j < 10; j++)
                    {
                        // Tester om der er en version, og i så fald, gemmer versionen i en string
                        if (gameTitle[i].version != null)
                        {
                            saveVersion[i, j] = gameTitle[i].version[j];
                            for (int k = 0; k < 50; k++)
                            {
                                // Tester om der er en pris, og gemmer i så fald udgavens stand, pris, og evt. noter i samme string
                                if (gameTitle[i].price[j, k] != 0)
                                {
                                    saveCopy[i, j, k] = $"Stand: {gameTitle[i].gameCondiditon[j, k]} : Pris: {gameTitle[i].price[j, k]} : Note: {gameTitle[i].notes[j, k]}";
                                }
                                else
                                    break;
                            }
                        }
                        else
                            break;
                    }
                }
                else
                    break;
            }
            try
            {
                //Open the File
                StreamWriter sw = new StreamWriter("C:\\Users\\pibm9\\OneDrive - UCL Erhvervsakademi og Professionshøjskole\\Dokumenter\\Datamatiker\\Programmering\\Projekt Genspil v.2\\GenspilLAgerListe.txt", false, Encoding.ASCII);

                for(int a = 0; a < 50; a++)
                {
                    // Hvis der er et input i arrayet, bliver dette printet.
                    if (saveTitle[a] != null)
                    {
                        sw.WriteLine(saveTitle[a]);
                        for (int b = 0; b < 50; b++)
                        {
                            // Hvis der er input i version - arrayet, bliver dette printet.
                            if (saveVersion[a, b] != null)
                            {
                                sw.WriteLine("    " + saveVersion[a, b]);
                                for (int c = 0; c < 50; c++)
                                {
                                    // Hvis der er et input i udgave - arrayet, bliver dette printet.
                                    if (saveCopy[a, b, c] != null)
                                    {
                                        sw.WriteLine("        " + saveCopy[a, b, c]);
                                    }
                                    else
                                        break;
                                }
                            }
                            else
                                break;
                        }
                    }
                    else
                        break;
                }

                //close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
