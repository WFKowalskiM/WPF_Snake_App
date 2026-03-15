using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using ConsoleApp;


namespace ConsoleApp
{
    // Static Console Methods
    internal class SCM
    {
        private static Snake s;
        private static String[] list = { "[E] Start Game", "[F] Game Speed: < Normal >", "[C] Controls", "[Z] Credits", "[Q] Exit Game" };
        private static String[] speedopt = { "< Normal >", "< Fast >", "100", "50" };
        private static int speed = 100;
        private static int speedoptalt = 0;
        private static ConsoleKey input;

        // MENU 
        public static void changeSpeedOpt()
        {
            speedoptalt++;
            list[1] = list[1].Substring(0, 16) + speedopt[speedoptalt % 2];
            Int32.TryParse(speedopt[(speedoptalt % 2) + 2], out speed);
        }
        public static void startGame()
        {
            Console.Clear();
            mainLoop();
            Console.Clear();
        }
        public static void mainLoop()
        {
            s = new Snake();
            s.InitializeGame(3, 3, 0, 1, 80, 30);
            Console.SetWindowSize(s.getWidth(), s.getHeight());
            Console.SetBufferSize(80, 30);
            printBonus();
            //int n = 0;
            while (!s.DOISTOP())
            {
                readInput();
                s.gameLoop();
                printSnake(s.getCurrentPos());
                Thread.Sleep(speed);
            }
            printOutro();
            Thread.Sleep(800);
        }

        // ODCZYT KLAWIATURY
        public static void readInput()
        {
            if (Console.KeyAvailable)
            {
                input = Console.ReadKey(true).Key;
                switch (input)
                {
                    default:
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        s.changeDirection("Up");
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        s.changeDirection("Down");
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        s.changeDirection("Left");
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        s.changeDirection("Right");
                        break;
                    case ConsoleKey.Q:
                        s.STOPIT(true);
                        break;
                }
            }
        }

        // PRINTS
        public static void printSnake(newPoint currentposition)
        {
            if (!s.DOISTOP())
            {
                int count = s.getSnakePoints().Count;
                s.getSnakePoints().Insert(0, new newPoint(currentposition));
                if (count >= s.getLength())
                {
                    Console.SetCursorPosition(s.getSnakePoints()[count].getX(), s.getSnakePoints()[count].getY());
                    Console.Write(" ");
                    s.getSnakePoints().RemoveAt(count);
                }
                else
                {
                    printBonus();
                }
                foreach (var part in s.getSnakePoints())
                {
                    Console.SetCursorPosition(part.getX(), part.getY());
                    if (part.getX() == currentposition.getX() && part.getY() == currentposition.getY())
                    {
                        Console.Write("■");
                    }
                    else
                    {
                        Console.Write("●");
                    }
                }
            }
        }
        public static void printBonus()
        {
            foreach (var bonus in s.getBonusPoints())
            {
                Console.SetCursorPosition(bonus.getX(), bonus.getY());
                Console.Write("★");
            }
        }
        public static void printOutro()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            setOutro(0);
            for (int i = 1; i < (Console.BufferHeight / 2) - 2; i++)
            {
                Console.SetCursorPosition(34, i - 1);
                Console.WriteLine("            ");
                setOutro(i);
                Thread.Sleep(50);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void setOutro(int i)
        {
            Console.SetCursorPosition(34, i);
            Console.WriteLine(" ---------- ");
            Console.SetCursorPosition(34, i + 1);
            Console.WriteLine("[   Game   ]");
            Console.SetCursorPosition(34, i + 2);
            Console.WriteLine("[   Over   ]");
            Console.SetCursorPosition(34, i + 3);
            Console.WriteLine("[__________]");
        }
        // SET / GET
        public static String getList(int x)
        {
            return list[x];
        }
    }
}
