using System;
using System.Collections.Generic;
using System.Timers;
using System.Media;
using System.ComponentModel;
using System.Diagnostics;

namespace KCK_Projekt
{
    class Program
    {
        static int indexMainMenu = 0;
        static int tabulator = 100;
        public static System.Timers.Timer timer = new System.Timers.Timer();
        static int ontick = 1;


        public static void Main()
        {
            Console.Clear();

            //Muzyka
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer(@"\ProjektKCK1\ProjektKCK\pacmanmusic.wav");
            sp.PlayLooping();
            sp.Play();


            List<string> menuItems = new List<string>()
            {
                "New Game",
                "Scores",
                "Settings",
                "About",
                "Exit"
            };

            Console.CursorVisible = false;


            //timer.Interval = 250;
            //timer.Elapsed += printtitle;
            //timer.AutoReset = true;
            //timer.Start();
            //timer.Stop();

            //printtitle();


            while (true)
            {
                //printtitle();
                string selectedMenuItem = drawMainMenu(menuItems);

                if (selectedMenuItem == "New Game")
                {
                    Console.Clear();
                    //Console.WriteLine("New Game chosen");
                    //zakoncz();
                    Map map = new Map();
                    map.showmap();
                    Pacman pacman = new Pacman();
                    pacman.Printpacman();
                    Console.Clear();

                }
                else if (selectedMenuItem == "Scores")
                {
                    Console.WriteLine("Scores chosen");
                }
                else if (selectedMenuItem == "Settings")
                {
                    Console.WriteLine("Settings chosen");
                }
                else if (selectedMenuItem == "About")
                {
                    Console.WriteLine("About chosen");
                }
                else if (selectedMenuItem == "Exit")
                {
                    Environment.Exit(0);
                }
            }
        }
        public class Map
        {
            private string walls;
            private string points;

            public Map ()
            {
                walls = @"
╔═══════════════════╦═══════════════════╗
║                   ║                   ║
║   ╔═╗   ╔═════╗   ║   ╔═════╗   ╔═╗   ║
║   ╚═╝   ╚═════╝   ║   ╚═════╝   ╚═╝   ║
║                                       ║
║   ═══   ║   ══════╦══════   ║   ═══   ║
║         ║         ║         ║         ║
╚═════╗   ╠══════   ║   ══════╣   ╔═════╝
      ║   ║                   ║   ║      
══════╝   ║   ╔════   ════╗   ║   ╚══════
              ║           ║             
══════╗   ║   ║           ║   ║   ╔══════
      ║   ║   ╚═══════════╝   ║   ║      
      ║   ║                   ║   ║      
╔═════╝   ║   ══════╦══════   ║   ╚═════╗
║                   ║                   ║
║   ══╗   ═══════   ║   ═══════   ╔══   ║
║     ║                           ║     ║
╠══   ║   ║   ══════╦══════   ║   ║   ══╣
║         ║         ║         ║         ║
║   ══════╩══════   ║   ══════╩══════   ║
║                                       ║
╚═══════════════════════════════════════╝
".Trim();
                points = @"                                         
  * * * * * * * * *   * * * * * * * * *  
  *     *         *   *         *     *  
  +     *         *   *         *     +  
  * * * * * * * * * * * * * * * * * * *  
  *     *   *               *   *     *  
  * * * *   * * * *   * * * *   * * * *  
        *                       *        
        *                       *        
        *                       *        
        *                       *        
        *                       *        
        *                       *        
        *                       *        
        *                       *        
  * * * * * * * * *   * * * * * * * * *  
  *     *         *   *         *     *  
  + *   * * * * * *   * * * * * *   * +  
    *   *   *               *   *   *    
  * * * *   * * * *   * * * *   * * * *  
  *               *   *               *  
  * * * * * * * * * * * * * * * * * * *  
                                         ";
            }
            public void showmap(bool renderSpace = false)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                int x = Console.CursorLeft;
                int y = Console.CursorTop;
                foreach (char c in walls)
                {
                    if (c is '\n')
                    {
                        Console.SetCursorPosition(x, ++y);
                    }
                    else if (!(c is ' ') || renderSpace)
                    {
                        Console.Write(c);
                    }
                    else
                    {
                        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                    }
                }
                Console.SetCursorPosition(0, 0);
                x = Console.CursorLeft;
                y = Console.CursorTop;
                foreach (char c in points)
                {
                    if (c is '\n')
                    {
                        Console.SetCursorPosition(x, ++y);
                    }
                    else if (!(c is ' ') || renderSpace)
                    {
                        Console.Write(c);
                    }
                    else
                    {
                        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                    }
                }
                Console.ReadKey();
            }
        }

        public class Pacman
        {
            enum direction { up = 0, down = 1, left = 2, right = 3 };
            private direction pacdirection;
            private int X;
            private int Y;
            public Pacman()
            {
                X = 20;
                Y = 17;
            }
            public void setPosition(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }
            public int getXPosition()
            {
                return X;
            }
            public int getYPosition()
            {
                return Y;
            }
            public void Printpacman()
            {
                Console.SetCursorPosition(X, Y);
                Console.Write("O");
                Console.ReadKey();
            }
        }

        public class Game
        {
            private Map map;
            private Pacman pacman;
            public Game()
            {
                //map=
            }
        }

        public static void printtitle(object source, ElapsedEventArgs e)
        {
            if (ontick % 2 == 0)
               {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.SetCursorPosition(0, 0);
                    Console.Write("_/_/_/_/_/  _/      _/  _/_/_/    _/_/_/_/");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("     _/      _/    _/_/    _/      _/   ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("   _/        _/  _/    _/    _/  _/       ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("    _/_/  _/_/  _/    _/  _/_/    _/    ");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("  _/          _/      _/_/_/    _/_/_/    ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("   _/  _/  _/  _/_/_/_/  _/  _/  _/     ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(" _/          _/      _/        _/         ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  _/      _/  _/    _/  _/    _/_/     ");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("_/          _/      _/        _/_/_/_/    ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(" _/      _/  _/    _/  _/      _/     ");
                    Console.ResetColor();
                    ontick = 1;
                    //System.Threading.Thread.Sleep(waittime);
                }
                else if(ontick % 2 == 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.SetCursorPosition(0, 0);
                    Console.Write("_/_/_/_/_/  _/      _/  _/_/_/    _/_/_/_/");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("     _/      _/    _/_/    _/      _/   ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("   _/        _/  _/    _/    _/  _/       ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("    _/_/  _/_/  _/    _/  _/_/    _/    ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("  _/          _/      _/_/_/    _/_/_/    ");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("   _/  _/  _/  _/_/_/_/  _/  _/  _/     ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" _/          _/      _/        _/         ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("  _/      _/  _/    _/  _/    _/_/     ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("_/          _/      _/        _/_/_/_/    ");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine(" _/      _/  _/    _/  _/      _/     ");
                    Console.ResetColor();
                    ontick = 2;
                    //System.Threading.Thread.Sleep(waittime);
                }
            //}
        }

        public static void zakoncz()
        {
            Console.Clear();
            while (true)
            {
                ConsoleKeyInfo ckey = Console.ReadKey();
                if (ckey.Key == ConsoleKey.DownArrow)
                {
                    Console.WriteLine("Gra");
                }
                else if (ckey.Key == ConsoleKey.UpArrow)
                {
                    Console.WriteLine("Gra");
                }
                else if (ckey.Key == ConsoleKey.LeftArrow)
                {
                    Console.WriteLine("Gra");
                }
                else if (ckey.Key == ConsoleKey.RightArrow)
                {
                    Console.WriteLine("Gra");
                }
                else if (ckey.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            Console.Clear();
        }
        public static string drawMainMenu(List<string> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (i == indexMainMenu)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition((tabulator + 5) / 2, 10 + i);
                    Console.WriteLine("<<<" + items[i] + ">>>");
                }
                else
                {
                    Console.SetCursorPosition((tabulator) / 2, 10 + i);
                    Console.WriteLine(items[i]);
                }
                Console.ResetColor();
            }
            ConsoleKeyInfo ckey = Console.ReadKey();
            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (indexMainMenu == items.Count - 1) { }
                else { indexMainMenu++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (indexMainMenu <= 0) { }
                else { indexMainMenu--; }
            }
            else if (ckey.Key == ConsoleKey.LeftArrow)
            {
                Console.Clear();
            }
            else if (ckey.Key == ConsoleKey.RightArrow)
            {
                Console.Clear();
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
                return items[indexMainMenu];
            }
            else
            {
                return "";
            }

            Console.Clear();
            return "";
        }
    }
}