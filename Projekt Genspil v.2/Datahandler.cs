using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Genspil_v._2
{
    internal class Datahandler
    {
        
        Game game = new Game();
        private string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private int item = 0;
        public int Item 
        { 
            get { return item; } 
            set { item = value; }
        }

        
        public void PrintList(Game[] game)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(path, "GenspilLagerliste.txt"), false, Encoding.ASCII))
            {
                DateTime date = DateTime.Now;
                string dato = date.ToShortDateString();

                sw.WriteLine($"Lagerliste Genspil - {dato}");
                for (int i = 0; i < 50; i++)
                {
                    // Tester om der er en tittel i arrayet, og er der det, skrives tittel, genre, og antal spillere i samme string.
                    if (game[i] != null)
                    {
                        string title = "";
                        sw.WriteLine(game[i].GetTitle(title));
                        //saveTitle[i] = $"Tittel: {gameTitle[i].title} : Genre: {gameTitle[i].genre} : Minimum spillere: {gameTitle[i].players[0]} : maximum: {gameTitle[i].players[1]} ";
                        for (int j = 0; j < 10; j++)
                        {
                            // Tester om der er en version, og i så fald, gemmer versionen i en string
                            if (game[i].version[j] != null)
                            {
                                sw.WriteLine("Version: " + game[i].version[j]);
                                for (int k = 0; k < 20; k++)
                                {
                                    // Tester om der er en pris, og gemmer i så fald udgavens stand, pris, og evt. noter i samme string
                                    if (game[i].price[j, k] != 0)
                                    {
                                        sw.WriteLine($"Stand: {game[i].gameCondiditon[j, k]} : Pris: {game[i].price[j, k]} : Note: {game[i].notes[j, k]}");
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
            }
            
        }
        public Game ReadList(Game[] game, int gameItem)
        {
            string line = "123";
            string[] save;
            try
            {
                int t = -1, v = -1, c = -1;
                // Pass the file path and file to the StreamReader constructor.
                StreamReader sr = new StreamReader(Path.Combine(path, "GenspilLagerliste.txt"));

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
                        game[gameItem] = new Game();
                        gameItem++;
                        t++;
                        // increment "t" for title, and reset "v" for version.
                        v = -1;
                        // Write the title index, split the line to save array, for each " : "
                        Console.WriteLine($"index: {t} : {line}");
                        save = line.Split(":");
                        // save title, genre, and num. players in corresponding places in the game class.
                        game[t].SetTitle(save[1]);
                        game[t].SetGenre(save[3]);
                        Int32.TryParse(save[5], out game[t].players[0]);
                        Int32.TryParse(save[7], out game[t].players[1]);
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
                        game[t].SetVersion(save[1]);
                        game[t].i = v;
                        Console.WriteLine($"Index: {t}, {v} : {line}");
                    }
                    // if line contains "pris:" write the corresponding index and line.
                    else if (line.Contains("Pris: "))
                    {
                        // increment "c" for copy, and split the line in the save array.
                        c++;
                        game[t].j[v] = c;
                        save = line.Split(":");
                        // trim save[1], so that it contains one character, convert to char,
                        // and save in corresponding var in game class.
                        save[1].Trim();
                        char condition = Convert.ToChar(save[1].Trim());
                        game[t].gameCondiditon[v, c] = condition;
                        Int32.TryParse(save[3], out game[t].price[v, game[t].j[v]]);
                        if (save.Contains(save[5]) && save[5] != null)
                            game[t].notes[v, game[t].j[v]] = save[5];
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
                //menu.GameItem = gameItem;
                this.Item = gameItem;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally { Console.WriteLine("Executing finally block"); }

            return this.game; 
        }
    }
}
