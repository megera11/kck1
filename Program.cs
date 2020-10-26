﻿using System;
using System.Collections.Generic;
using System.Timers;
using System.Media;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Text;

namespace KCK_Projekt
{
    class Program
    {
        
        static int indexMainMenu = 0;
        static int tabulator = 100;
        public static System.Timers.Timer timer = new System.Timers.Timer();
        static int ontick = 1;
        public static Mutex mut = new Mutex();
       
        public static void Main()
        {
            Console.Clear();

            //Muzyka
           // System.Media.SoundPlayer sp = new System.Media.SoundPlayer(soundLocation:@"pacmanmusic.wav");
           // sp.PlayLooping();
           // sp.Play();

            List<string> menuItems = new List<string>()
            {
                "New Game",
                "Scores",
                "Settings",
                "About",
                "Exit"
            };

            Console.CursorVisible = false;

            timer.Interval = 250;
            timer.Elapsed += printtitle;
            timer.AutoReset = true;
            timer.Start();

            while (true)
            {
                string selectedMenuItem = drawMainMenu(menuItems);

                if (selectedMenuItem == "New Game")
                {
                    timer.Stop();
                    Console.Clear();

                    Game game = Game.GetGame();
                    game.Init();
                    Console.Clear();
                    timer.Start();
                }
                else if (selectedMenuItem == "Scores")
                {
                    Scores scores = new Scores();
                    scores.PrintScores();

                }
                else if (selectedMenuItem == "Settings")
                {
                    Console.WriteLine("Settings chosen");
                }
                else if (selectedMenuItem == "About")
                {
                    AboutDisplay();
                }
                else if (selectedMenuItem == "Exit")
                {
                    Environment.Exit(0);
                }
            }
        }
        public static void AboutDisplay()
        {
            timer.Stop();
            Console.Clear();
            Console.SetCursorPosition(tabulator / 2, 5);
            Console.WriteLine("Wykonali:");
            Console.SetCursorPosition(tabulator / 2, 6);
            Console.WriteLine("Weronika Żukowska");
            Console.SetCursorPosition(tabulator / 2, 7);
            Console.WriteLine("Marek Hajduczenia");
            Console.SetCursorPosition(tabulator / 2, 8);
            Console.WriteLine("Przemysław Jarocki");
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
            Console.Clear();
            timer.Start();
        }

        public static void Settings()
        {
            int option = 0;
            timer.Stop();
            Console.Clear();
            Console.SetCursorPosition(tabulator / 2, 5);
            Console.WriteLine("Choose your control settings");
        }


        public class Map
        {
            private string walls;
            private string points;
            private string wallsposition;
            public Map()
            {
                walls = @"
╔═══════════════════╦═══════════════════╗
║                   ║                   ║
║   ╔═╗   ╔═════╗   ║   ╔═════╗   ╔═╗   ║
║   ╚═╝   ╚═════╝   ╩   ╚═════╝   ╚═╝   ║
║                                       ║
║   ═══   ╦   ══════╦══════   ╦   ═══   ║
║         ║         ║         ║         ║
╚═════╗   ╠══════   ╩   ══════╣   ╔═════╝
      ║   ║                   ║   ║      
══════╝   ╩   ╔           ╗   ╩   ╚══════
              ║           ║             
══════╗   ╦   ║           ║   ╦   ╔══════
      ║   ║   ╚═══════════╝   ║   ║      
      ║   ║                   ║   ║      
╔═════╝   ╩   ══════╦══════   ╩   ╚═════╗
║                   ║                   ║
║   ══╗   ═══════   ╩   ═══════   ╔══   ║
║     ║                           ║     ║
╠══   ╩   ╦   ══════╦══════   ╦   ╩   ══╣
║         ║         ║         ║         ║
║   ══════╩══════   ╩   ══════╩══════   ║
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
                wallsposition = @"
11111111111111111111111111111111111111111
10000000000000000000100000000000000000001
10001110001111111000100011111110001110001
10001110001111111000100011111110001110001
10000000000000000000000000000000000000001
10001110001000111111111111100010001110001
10000000001000000000100000000010000000001
11111110001111111000100011111110001111111
00000010001000000000000000000010001000000
11111110001000100000000000100010001111111
00000000000000100000000000100000000000000
11111110001000100000000000100010001111111
00000010001000111111111111100010001000000
00000010001000000000000000000010001000000
11111110001000111111111111100010001111111
10000000000000000000100000000000000000001
10001110001111111000100011111110001110001
10000010000000000000000000000000001000001
11100010001000111111111111100010001000111
10000000001000000000100000000010000000001
10001111111111111000100011111111111110001
10000000000000000000000000000000000000001
11111111111111111111111111111111111111111"
.Trim();
            }
            public string GetWallsPosition()
            {
                return wallsposition;
            }
            public string GetPoints()
            {
                return points;
            }
            public void ChangePoint(int X, int Y)
            {
                StringBuilder sb = new StringBuilder(points);
                sb[Y * 43 + X] = ' ';
                points = sb.ToString();
            }
            public void showmap(bool renderSpace = false)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Blue;
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
                Console.ResetColor();
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
                //Console.ReadKey();
            }
        }

        public class Entity
        {
            // 0 == up
            // 1 == down 
            // 2 == left
            // 3 == right
            protected int direction = 0;
            protected int X;
            protected int Y;
            protected Map map;

            public Entity()
            {

            }

            public int getXPosition()
            {
                return X;
            }
            public int getYPosition()
            {
                return Y;
            }
            public void setPosition(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }
            public int getDirection()
            {
                return direction;
            }

            public void setDirection(int direction)
            {
                this.direction = direction;
            }

            protected char BoardAt(int x, int y, Map map)
            {
                return map.GetWallsPosition()[y * 43 + x];
            }

            public bool CanMove( int direction)
            {

                if (direction == 0 && BoardAt(X, Y - 1, map) == '0' && BoardAt(X - 1, Y - 1, map) == '0' && BoardAt(X + 1, Y - 1, map) == '0')
                {
                    return true;
                }
                else if (direction == 1 && BoardAt(X, Y + 1, map) == '0' && BoardAt(X - 1, Y + 1, map) == '0' && BoardAt(X + 1, Y + 1, map) == '0')
                {
                    return true;
                }
                else if (direction == 2 && BoardAt(X - 2, Y, map) == '0' || (X == 39 && Y == 10))
                {
                    return true;
                }
                else if (direction == 3 && BoardAt(X + 2, Y, map) == '0' || (X == 1 && Y == 10))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        public class Pacman : Entity
        {
            public Pacman(int X,int Y,Map map)
            {
                this.X = X;
                this.Y = Y;
                this.map = map;

            }


            public void Move(ConsoleKeyInfo ckey)
            {
                mut.WaitOne();    
                if (ckey.Key == ConsoleKey.UpArrow && CanMove(0))
                {
                    Console.SetCursorPosition(X, Y);
                    Console.Write(" ");
                    PrintandUpdatepacman(X, Y - 1);

                }
                else if (ckey.Key == ConsoleKey.DownArrow && CanMove(1))
                {
                    Console.SetCursorPosition(X, Y);
                    Console.Write(" ");
                    PrintandUpdatepacman(X, Y + 1);

                }
                else if (ckey.Key == ConsoleKey.LeftArrow && CanMove(2))
                {

                    Console.SetCursorPosition(X, Y);
                    Console.Write(" ");
                    PrintandUpdatepacman(X - 1, Y);


                }
                else if (ckey.Key == ConsoleKey.RightArrow && CanMove(3))
                {
                    Console.SetCursorPosition(X, Y);
                    Console.Write(" ");
                    PrintandUpdatepacman(X + 1, Y);


                }
                else
                {
                    PrintandUpdatepacman(X, Y);
                }
                mut.ReleaseMutex();
            }

            public void PrintandUpdatepacman(int X, int Y)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (X == 40 && Y == 10)
                {
                    Console.SetCursorPosition(40, 10);
                    Console.Write(" ");
                    Console.SetCursorPosition(1, 10);
                    setPosition(1, 10);
                    Console.Write("O");
                    return;
                    
                }
                 if ( X == 0 && Y == 10)
                {
                    Console.SetCursorPosition(0, 10);
                    Console.Write(" ");
                    Console.SetCursorPosition(39, 10);
                    setPosition(39, 10);
                    Console.Write("O");
                    return;
                   
                 }
                
                
                    Console.SetCursorPosition(X, Y);
                    setPosition(X, Y);
                    Console.Write("O");
                    Console.ResetColor();
            }
        }
        public class Ghost : Entity
        {
            Random random = new Random();
         

            public Ghost(int X, int Y,Map map,int direction)
            {
                this.X = X;
                this.Y = Y;
                this.map = map;
                this.direction = direction;
            }

            public void Move()
            {
                while(true){
                    mut.WaitOne();
                    direction = new Random().Next(4);
                    mut.ReleaseMutex();
                    
                    while (CanMove(direction))
                    {
                        
                            mut.WaitOne();
                            if (direction == 0 && CanMove(0))
                            {
                                Console.SetCursorPosition(X, Y);
                                Console.Write(" ");
                                Printghost(X, Y - 1);

                            }
                            else if (direction == 1 && CanMove(1))
                            {
                                Console.SetCursorPosition(X, Y);
                                Console.Write(" ");
                                Printghost(X, Y + 1);

                            }
                            else if (direction == 2 && CanMove(2))
                            {

                                Console.SetCursorPosition(X, Y);
                                Console.Write(" ");
                                Printghost(X - 1, Y);


                            }
                            else if (direction == 3 && CanMove(3))
                            {
                               Console.SetCursorPosition(X, Y);
                               Console.Write(" ");
                               Printghost(X + 1, Y);


                            }
                            else
                            {
                               break;
                            }
                            mut.ReleaseMutex();
                            Thread.Sleep(100);
                        
                    }

                }
            }


            public void Printghost(int X , int Y)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(X, Y);
                setPosition(X, Y);
                Console.Write("X");
                Console.ResetColor();
            }

        }
        public class Game
        {
            private Map map;
            private Pacman pacman;
            private static Game game;
            private Ghost[] ghost;
            private string nickname;
            private int score;

            private Game()
            {
                map = new Map();
                pacman = new Pacman(20,17,map);
                ghost = new Ghost[4] {new Ghost(16,10, map,0), new Ghost(18,10, map,1), new Ghost(22,10, map,2), new Ghost(24,10, map,3) };
                score = 0;
            }

            public static Game GetGame()
            {
                if (game == null)
                {
                    game = new Game();
                }
                return game;
            }
            public  bool CheckLose()
            {
                if(pacman.getXPosition() == ghost[0].getXPosition() && pacman.getYPosition() == ghost[0].getYPosition())
                {
                    return false;
                }
                else if (pacman.getXPosition() == ghost[1].getXPosition() && pacman.getYPosition() == ghost[0].getYPosition())
                {
                    return false;
                }
                else if (pacman.getXPosition() == ghost[2].getXPosition() && pacman.getYPosition() == ghost[0].getYPosition())
                {
                    return false;
                }
                else if (pacman.getXPosition() == ghost[3].getXPosition() && pacman.getYPosition() == ghost[0].getYPosition())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            public void Init()
            {
                Console.Write("Your nickname (can not be blank): ");
                nickname = Console.ReadLine();
                Console.Clear();
                map.showmap();
                pacman.PrintandUpdatepacman(20, 17);


                ghost[0].Printghost(16,10);
                ghost[1].Printghost(18,10);
                ghost[2].Printghost(22,10);
                ghost[3].Printghost(24,10);

                ThreadStart ghostref1 = new ThreadStart(ghost[0].Move);
                ThreadStart ghostref2 = new ThreadStart(ghost[1].Move);
                ThreadStart ghostref3 = new ThreadStart(ghost[2].Move);
                ThreadStart ghostref4 = new ThreadStart(ghost[3].Move);

                Thread ghostthread1 = new Thread(ghostref1);
                Thread ghostthread2 = new Thread(ghostref2);
                Thread ghostthread3 = new Thread(ghostref3);
                Thread ghostthread4 = new Thread(ghostref4);

                ConsoleKeyInfo ckey = Console.ReadKey(true);
                ghostthread1.Start();
                ghostthread2.Start();
                ghostthread3.Start();
                ghostthread4.Start();
                while (ckey.Key != ConsoleKey.Escape)
                {
                    
                    pacman.Move(ckey);
                    if(CheckLose() == false)
                    {
                        break;
                    }
                    SaveScore();
                    ckey = Console.ReadKey(true);
                }
                ghostthread1.Abort();
                ghostthread2.Abort();
                ghostthread3.Abort();
                ghostthread4.Abort();

                SaveScore();
            }



           
           
            public Pacman GetPacman()
            {
                 return pacman;
            }
            public Map GetMap()
            {
                return map;
            }



            public void SetNickname(string nickname)
            {
                this.nickname = nickname;
            }
            public Ghost GetGhost(int i)
            {
                return ghost[i];
            }
            
            private char PointAt(int X, int Y)
            {
                return map.GetPoints()[Y * 43 + X];
            }
            
            public void SaveScore()
            {

                StreamReader sr = new StreamReader("scores.txt");
                string line;
                int i = 0;
                while((line=sr.ReadLine())!=null)
                {
                    i++;
                }
                Scores[] scores = new Scores[i];

                
                if (PointAt(pacman.getXPosition(), pacman.getYPosition()) == '*')
                {
                    score++;
                    map.ChangePoint(pacman.getXPosition(), pacman.getYPosition());
                }
                else if(PointAt(pacman.getXPosition(), pacman.getYPosition()) == '+')
                {
                    score += 3;
                    map.ChangePoint(pacman.getXPosition(), pacman.getYPosition());
                }
                Console.SetCursorPosition(44, 1);
                Console.Write("Score: " + score);
            }
          
        }
        public class Scores
        {
            private string nickname;
            private int score;

            public Scores() { }
            public Scores(string nickname, int score)
            {
                this.nickname = nickname;
                this.score = score;
            }
            public void SetScore(int score)
            {
                this.score = score;
            }
            public void SetNickname(string nickname)
            {
                this.nickname = nickname;
            }
            public int GetScore()
            {
                return score;
            }
            public string GetNickname()
            {
                return nickname;
            }
            public void PrintScores()
            {
                timer.Stop();
                Console.Clear();
                int i = 0;
                Console.SetCursorPosition(tabulator / 2, 5);
                Console.WriteLine("SCORES: ");
                using (StreamReader sr = new StreamReader("scores.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.SetCursorPosition(tabulator / 2, (6 + i));
                        Console.WriteLine((i + 1) + ". " + line);
                        i++;
                    }
                }
                Console.WriteLine("Press a random key to back to menu...");
                Console.ReadKey();
                Console.Clear();
                timer.Start();
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
            else if (ontick % 2 == 1)
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
            ConsoleKeyInfo ckey = Console.ReadKey(true);
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