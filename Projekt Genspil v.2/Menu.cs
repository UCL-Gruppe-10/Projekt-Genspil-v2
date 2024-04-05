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
        
        
        public void ShowMenu() // Printer den primære menu
        {
            //Console.Clear();
            Console.WriteLine("Genspil");
            Console.WriteLine("-----------------------------------\n\n");
            // Sorteringsmuligheder efter spilnavn, genre og andre relevante kriterier.
            Console.Write("(1) Tilgå spil"); // rediger funktion herunder
            // Mulighed for at søge efter spil baseret på forskellige kriterier, såsom genre, antal spillere, stand, pris og navn.
            // Skal evt kunne opdatere forspørgsel og salg
            Console.WriteLine(" - Søg, Rediger, Salg og forspørgsel"); 
            Console.WriteLine("(2) Opret spil"); // opdater koder til arbejde med persistens.
            // Mulighed for at se, hvilke spil der er tilgængelige i lageret, og hvilke der er reserveret eller bestilt, og mulighed for en udskrift.
            // Sorteringsmuligheder efter spilnavn, genre og andre relevante kriterier.
            Console.WriteLine("(3) Lagerliste"); 
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
                        SearchGames();
                        break;
                    case 2:

                        CreateGame();
                        break;
                    case 3:
                        

                        Console.Clear();
                        gameTitle[gameItem] = new Game();
                        gameTitle[gameItem].CreateGame();
                        gameItem++;
                        ShowInventory();
                        ShowMenu();

                        break;
                    case 4:
                        gameTitle[gameItem].UpdateGame();   // Mangler ordenlig tilgang til specifike spil - Lægges i forlængelse af SØG SPIL!
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

        void SearchGames()
        { 
            Console.WriteLine("Indstast titlen på det spil du søger efter:");
            string searchWord = Console.ReadLine();

            Console.WriteLine($"Resultatet af din søgning: '{searchWord}'");
            bool found = false; 
            foreach (Game game in gameTitle) // Her gennemgår den Array'et efter søgeordet, hvor den så vil sortere dem efter kriteriet
            {
                if (game != null && game.title.IndexOf(searchWord, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine(game.title);
                    found = true;
                }
            }
            // Vis en besked, hvis der ikke er nogen søgeresultater
            if (!found)
            {
                Console.WriteLine($"Ingen søgeresultater fundet for '{searchWord}'");
            }

            Console.ReadLine();

        }

        void CreateGame()
        {
            if (gameItem < 50)
            {
                Console.Clear();
                gameTitle[gameItem] = new Game();
                gameTitle[gameItem].CreateGame();
                gameItem++;
            }
            else 
            {
                Console.WriteLine("Maximum antal er nået");
            }
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
            DateTime Dato = DateTime.Now;
            string dato = Dato.ToShortDateString();
            string[] saveTitle = new string[50];
            string[,] saveVersion = new string[10, 20];
            string[,,] saveCopy = new string[50, 10, 20];
            for (int i = 0; i < 50;i++)
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
                // Instantiate the StreamWriter object, and create/set it to write/overwrite the variables to .txt
                StreamWriter sw = new StreamWriter($"GenspilLAgerListe.txt", false, Encoding.ASCII);
                //write the Title and date of the list/backup
                sw.WriteLine($"Lagerliste Genspil - {dato}");               
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
                Console.ReadKey();
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
        // Method to read the text file, and extract each var, and save them in their corresponding place.
        public void ReadtxtFile()
        {
            string line = "123";
            string[] save;
            try
            {
                int t = -1, v = -1, c = -1;
                // Pass the file path and file to the StreamReader constructor.
                StreamReader sr = new StreamReader("C:\\Users\\pibm9\\OneDrive - UCL Erhvervsakademi og Professionshøjskole\\Dokumenter\\Datamatiker\\Programmering\\Projekt Genspil v.2\\GenspilLAgerListe.txt");
                
                while (line != null)
                {
                    
                    // Read the first line of text.
                    line = sr.ReadLine();
                    // Continue to read, until end of file.
                    if (line == null)
                        break;
                    // If line contains "Title", create new instance of Game, and increment gameItem.
                    if (line.Contains("Tittel: "))
                    {
                        gameTitle[gameItem] = new Game();
                        gameItem++;
                        t++;
                        // increment "t" for title, and reset "v" for version.
                        v = -1;
                        // Write the title index, split the line to save array, for each " : "
                        Console.WriteLine($"index: {t} : {line}");
                        save = line.Split(":");
                        // save title, genre, and num. players in corresponding places in the game class.
                        gameTitle[t].SetTitle(save[1]);
                        gameTitle[t].SetGenre(save[3]);
                        Int32.TryParse(save[5], out gameTitle[t].players[0]);
                        Int32.TryParse(save[7], out gameTitle[t].players[1]);
                    }
                    // If line contains "version:" write the corresponding index number.
                    else if (line.Contains("Version:"))
                    {
                        // Split the line into the save array, and assign the value of
                        // "version" on its corresponding place in the game class.
                        v++;
                        // Increment "E", and reset "c" for copy 
                        c = -1;
                        save = line.Split(":");
                        gameTitle[t].SetVersion(save[1]);
                        gameTitle[t].i = v;
                        Console.WriteLine($"Index: {t}, {v} : {line}");
                    }
                    // if line contains "pris:" write the corresponding index and line.
                    else if (line.Contains("Pris: "))
                    {
                        // increment "c" for copy, and split the line in the save array.
                        c++;
                        gameTitle[t].j[v] = c;
                        save = line.Split(":");
                        // trim save[1], so that it contains one character, convert to char,
                        // and save in corresponding var in game class.
                        save[1].Trim();
                        char condition = Convert.ToChar(save[1].Trim());
                        gameTitle[t].gameCondiditon[v, c] = condition;
                        Int32.TryParse(save[3], out gameTitle[t].price[v, gameTitle[t].j[v]]);
                        if (save.Contains(save[5]) && save[5] != null)
                                gameTitle[t].notes[v, gameTitle[t].j[v]] = save[5];
                        //saveCopy[d, e, f] = line;
                        Console.WriteLine($"Index: {t}, {v}, {c} : {line}");
                    }
                    // if nothing else fits, write the line, close the file, and ask for reaction from user. 
                    else 
                    {
                        Console.WriteLine(line);
                    }
                }
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally { Console.WriteLine("Executing finally block"); }
        }

    }
}
