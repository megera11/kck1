using System;
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
        Random random = new Random();
        static int indexMainMenu = 0;
        static int tabulator = 100;
        public static System.Timers.Timer timer = new System.Timers.Timer();
        static int ontick = 1;

        public static void Main()
        {
            Console.Clear();

            //Muzyka
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer(soundLocation:@"pacmanmusic.wav");
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
                    while(true)
                    {
                        game.PacmanMove();
                        break;
                    }
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
                    About();
                }
                else if (selectedMenuItem == "Exit")
                {
                    Environment.Exit(0);
                }
            }
        }
        public static void About()
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
            Console.WriteLine("Press a random key to back to menu...");
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
══════╝   ╩   ╔════   ════╗   ╩   ╚══════
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
11111110001000111110001111100010001111111
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

        public class Pacman
        {
            // 0 == up
            // 1 == down 
            // 2 == left
            // 3 == right
            private int direction = 0;
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
            public int getDirection()
            {
                return direction;
            }

            public void setDirection(int direction )
            {
                this.direction = direction;
            }
            public void Printpacman(int X, int Y)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(X, Y);
                setPosition(X, Y);
                Console.Write("O");
                Console.ResetColor();
                //Console.ReadKey();
            }
        }
        public class Ghost
        {
            private int X;
            private int Y;
            public Ghost(int X, int Y)
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
            public void setPosition(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }
            public void Printghost(int X, int Y)
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
                pacman = new Pacman();
                ghost = new Ghost[4] {new Ghost(16,10), new Ghost(18,10), new Ghost(22,10), new Ghost(24,10) };
                score = 0;
            }

            public static Game GetGame()
            {
                game = null;
                if (game == null)
                {
                    game = new Game();
                }
                return game;
            }
            public void Init()
            {
                Console.Write("Your nickname (can not be blank): ");
                string nickname = Console.ReadLine();
                Console.Clear();
                GetMap().showmap();
                GetPacman().Printpacman(20, 17);
                GetGhost(0).Printghost(16, 10);
                GetGhost(1).Printghost(18, 10);
                GetGhost(2).Printghost(22, 10);
                GetGhost(3).Printghost(24, 10);
                SaveScore();
            }
            public void SetNickname(string nickname)
            {
                this.nickname = nickname;
            }
            public Map GetMap()
            {
                return map;
            }

            public Pacman GetPacman()
            {
                return pacman;

            }
            public Ghost GetGhost(int i)
            {
                return ghost[i];
            }
            private char BoardAt(int x, int y)
            {
                return map.GetWallsPosition()[y * 43 + x];
            }
            private char PointAt(int X, int Y)
            {
                return map.GetPoints()[Y * 43 + X];
            }
            public bool CanMove(int X, int Y , int direction)
            {

                if (direction == 0 && BoardAt(X, Y - 1) == '0' && BoardAt(X - 1, Y - 1) == '0' && BoardAt(X + 1, Y - 1) == '0')
                {
                    return true;
                }
                else if (direction == 1 && BoardAt(X, Y + 1) == '0' && BoardAt(X - 1, Y + 1) == '0' && BoardAt(X + 1, Y + 1) == '0')
                {
                    return true;
                }
                else if (direction == 2 && BoardAt(X - 2, Y) == '0')
                {
                    return true;
                }
                else if (direction == 3 && BoardAt(X + 2, Y) == '0')
                {
                    return true;
                }
                else
                {
                    this.pacman.Printpacman(pacman.getXPosition(), pacman.getYPosition());
                    return false;
                }
            }
            public void PacmanMove()
            {
                while (true)
                {
                    ConsoleKeyInfo ckey = Console.ReadKey(true);
                    if (ckey.Key == ConsoleKey.UpArrow && CanMove(pacman.getXPosition(), pacman.getYPosition(), 0))
                    {
                        Console.SetCursorPosition(pacman.getXPosition(), pacman.getYPosition());
                        Console.Write(" ");
                        this.pacman.Printpacman(pacman.getXPosition(), pacman.getYPosition() - 1);
                        game.SaveScore();
                    }
                    else if (ckey.Key == ConsoleKey.DownArrow && CanMove(pacman.getXPosition(), pacman.getYPosition(), 1))
                    {
                        Console.SetCursorPosition(pacman.getXPosition(), pacman.getYPosition());
                        Console.Write(" ");
                        this.pacman.Printpacman(pacman.getXPosition(), pacman.getYPosition() + 1);
                        game.SaveScore();
                    }
                    else if (ckey.Key == ConsoleKey.LeftArrow && CanMove(pacman.getXPosition(), pacman.getYPosition(), 2))
                    {
                        Console.SetCursorPosition(pacman.getXPosition(), pacman.getYPosition());
                        Console.Write(" ");
                        this.pacman.Printpacman(pacman.getXPosition() - 1, pacman.getYPosition());
                        game.SaveScore();
                    }
                    else if (ckey.Key == ConsoleKey.RightArrow  && CanMove(pacman.getXPosition(), pacman.getYPosition(), 3))
                    {
                        Console.SetCursorPosition(pacman.getXPosition(), pacman.getYPosition());
                        Console.Write(" ");
                        this.pacman.Printpacman(pacman.getXPosition() + 1, pacman.getYPosition());
                        game.SaveScore();
                    }
                    else if (ckey.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
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
            public void GhostMove(Ghost ghost)
            {

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