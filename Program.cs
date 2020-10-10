using System;
using System.Collections.Generic;
using System.Media;
using System.ComponentModel;
using System.Diagnostics;

namespace KCK_Projekt
{
    class Program
    {
        static int indexMainMenu = 0;  
        static int tabulator = 100;
        public static void Main()
        {
           // Console.Clear();
            List<string> menuItems = new List<string>()
            {
                "New Game",
                "Scores",
                "Settings",
                "About",
                "Exit"
            };

            //Muzyka
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer(@"\ProjektKCK1\ProjektKCK\pacmanmusic.wav");
            //sp.PlayLooping();
            //sp.Play();


          

            Console.CursorVisible = false;


            while (true)
            {
                printtitle();
                string selectedMenuItem = drawMainMenu(menuItems);

                if (selectedMenuItem == "New Game")
                {
                    Console.Clear();
                    //Console.WriteLine("New Game chosen");
                    //zakoncz();
                    Game game = Game.GetGame();
                    game.GetMap().PrintMap();
                    game.GetPacman().PrintPacman();
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
        }//main end
        

        public static void printtitle()
        {

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition((tabulator - 50) / 2, 3);
            Console.Write("_/_/_/_/_/  _/      _/  _/_/_/    _/_/_/_/");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(Console.CursorLeft, 3);
            Console.WriteLine("     _/      _/    _/_/    _/      _/   ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition((tabulator - 50) / 2, Console.CursorTop);
            Console.Write("   _/        _/  _/    _/    _/  _/       ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(Console.CursorLeft ,4);
            Console.WriteLine("    _/_/  _/_/  _/    _/  _/_/    _/    ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition((tabulator - 50) / 2, Console.CursorTop);
            Console.Write("  _/          _/      _/_/_/    _/_/_/    ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(Console.CursorLeft, 5);
            Console.WriteLine("   _/  _/  _/  _/_/_/_/  _/  _/  _/     ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition((tabulator - 50) / 2, Console.CursorTop);
            Console.Write(" _/          _/      _/        _/         ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(Console.CursorLeft, 6);
            Console.WriteLine("  _/      _/  _/    _/  _/    _/_/     ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition((tabulator - 50) / 2, Console.CursorTop);
            Console.Write("_/          _/      _/        _/_/_/_/    ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(Console.CursorLeft, 7);
            Console.WriteLine(" _/      _/  _/    _/  _/      _/     ");
            Console.ResetColor();


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


        public class Map
        {
            private string walls = @"
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
            private string points = @"                                         
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

         
            public void PrintMap(bool renderSpace = false)
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
            public void PrintPacman()
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
            private static Game game;

            private Game(){
                map = new Map();
                pacman = new Pacman();
            }

            public static Game GetGame(){
                if(game == null){
                    game = new Game();
                    
                }
                return game;
            }

            public Map GetMap(){
                return map;
            }

            public Pacman GetPacman(){
                return pacman;
            }
        }

        
    }
}