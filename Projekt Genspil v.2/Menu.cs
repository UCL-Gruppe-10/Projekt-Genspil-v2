using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Projekt_Genspil_v._2
{
    internal class Menu
    {
        public Game[] gameTitle = new Game[50];
        int gameItem = 0;
        string[] saveTitle = new string[50];
        string[,] saveVersion = new string[10, 20];
        string[,,] saveCopy = new string[50, 10, 20];

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
                        ShowInventory();
                        ShowMenu();
                        break;
                    case 0:
                        PrinttxtFile();
                        Console.WriteLine("Farvel");
                        break;
                    default:
                        Console.Write("Fejlinput, prøv igen:");
                        break;
                }
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
        public void PrinttxtFile()
        {
            string[] version = new string[50];
            DateTime Dato = new DateTime();
            string dato = Dato.ToString();
            for(int i = 0; i < 50;i++)
            {
                // Tester om der er en tittel i arrayet, og er der det, skrives tittel, genre, og antal spillere i samme string.
                if (gameTitle[i] != null)
                {
                    saveTitle[i] = gameTitle[i].GetTitle(saveTitle[i]);
                    //saveTitle[i] = $"Tittel: {gameTitle[i].title} : Genre: {gameTitle[i].genre} : Minimum spillere: {gameTitle[i].players[0]} : maximum: {gameTitle[i].players[1]} ";
                    for (int j = 0; j < 10; j++)
                    {
                        // Tester om der er en version, og i så fald, gemmer versionen i en string
                        if (gameTitle[i].version[j] != null)
                        {
                            saveVersion[i, j] = "Version: " + gameTitle[i].version[j];
                            for (int k = 0; k < 20; k++)
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
                StreamWriter sw = new StreamWriter("C:\\Users\\pibm9\\OneDrive - UCL Erhvervsakademi og Professionshøjskole\\Dokumenter\\Datamatiker\\Programmering\\Projekt Genspil v.2\\GenspilLAgerListe3.txt", true, Encoding.ASCII);

                sw.WriteLine("Lagerliste Genspil");               
                for(int a = 0; a < 50; a++)
                {
                    // Hvis der er et input i arrayet, bliver dette printet.
                    if (saveTitle[a] != null)
                    {
                        sw.WriteLine(saveTitle[a]);
                        for (int b = 0; b < 10; b++)
                        {
                            // Hvis der er input i version - arrayet, bliver dette printet.
                            if (saveVersion[a, b] != null)
                            {
                                sw.WriteLine("    " + saveVersion[a, b]);
                                for (int c = 0; c < 20; c++)
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
                Console.WriteLine("Filer gemt til fil.\nFarvel.");
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
        public void ReadtxtFile()
        {
            string line = "123";
            try
            {
                int d = -1, e = -1, f = -1;
                // Pass the file path and file to the StreamReader constructor.
                StreamReader sr = new StreamReader("C:\\Users\\pibm9\\OneDrive - UCL Erhvervsakademi og Professionshøjskole\\Dokumenter\\Datamatiker\\Programmering\\Projekt Genspil v.2\\GenspilLAgerListe.txt");
                
                while (line != null)
                {

                    // Read the first line of text.
                    line = sr.ReadLine();
                    // Cpmtomie tp read, until end of file.
                    // save  line in Title Array
                    if (line == null)
                        break;
                    if (line.Contains("Tittel: "))
                    {
                        d++;
                        e = -1;
                        saveTitle[d] = line;
                        Console.WriteLine($"index: {d} : {saveTitle[d]}");
                        
                    }
                    else if (line.Contains("Version:"))
                    {
                        e++;
                        f = -1;
                        saveVersion[d, e] = line;
                        Console.WriteLine($"Index: {d},{e} : {saveVersion[d, e]}");
                    }
                    else if (line.Contains("Pris: "))
                    {
                        f++;
                        saveCopy[d, e, f] = line;
                        Console.WriteLine($"Index: {d}, {e}, {f} : {saveCopy[d, e, f]}");
                    }
                    else { Console.WriteLine("Fejl"); }
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally { Console.WriteLine("Executing finally block"); }
        }
        public void SaveIndex()
        {
            string _title;
            string[] _version = new string[10];
            string _genre;
            int _amount;
            int[] _players = new int[2];  // Minimum til maksimum spillere
            char[,] _gameCondiditon = new char[10, 20];
            int _price;
            string[,] _notes = new string[10, 20];

            string[] title = new string[50];
            string[] version = new string[10];
            string[,] copy = new string[10, 20];
            for (int i = 0; i < title.Length; i++)
            {
                if (saveTitle[i] != null)
                {
                    gameTitle[gameItem] = new Game();
                    gameItem++;
                    title[i] = saveTitle[i];
                    string[] save = title[i].Split(":", StringSplitOptions.TrimEntries);
                    _title = save[1];
                    gameTitle[i].SetTitle(_title);
                    _genre = save[3];
                    gameTitle[i].genre = _genre;                    
                    Int32.TryParse(save[5], out _players[0]);
                    Int32.TryParse(save[7], out _players[1]);
                    gameTitle[i].SetPlayers(_players);
                    for (int j = 0; j < version.Length; j++)
                    {
                        if (saveVersion[i, j] != null)
                        {
                            string[] Version = saveVersion[i, j].Split(":", StringSplitOptions.TrimEntries);
                            gameTitle[i].SetVersion(Version[1]);
                            for (int k = 0; k < copy.Length; k++)
                            {
                                if (saveCopy[i, j, k] != null)
                                {
                                    string _copy = saveCopy[i, j, k];
                                    string[] Copy = _copy.Split(":", StringSplitOptions.TrimEntries);
                                    if (Copy[5] != null)
                                        gameTitle[i].SetNotes(Copy[5]);
                                    else gameTitle[i].SetNotes(null);
                                    Int32.TryParse(Copy[3], out _price);
                                    gameTitle[i].SetPrice(_price);
                                    gameTitle[i].SetGameCondition(Convert.ToChar(Copy[1]));
                                }
                                else
                                    break;
                            }
                        }
                        else
                        {
                            break;
                        }                        
                    }
                }
                else
                    break;
            }
        }
    }
}
