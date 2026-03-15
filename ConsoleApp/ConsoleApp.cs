using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp
{

    class ConsoleApp
    {
        private static int menuSize = 5;
        private static int choice = 0;
        public static void PrintTask()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Use WSAD or Arrow Keys to change option, Press Enter or Spacebar to select.".PadRight(Console.BufferWidth - 1));
            Console.WriteLine("Use [Hotkeys] to access options quickly.".PadRight(Console.BufferWidth - 1));
            for (int i = 0; i < menuSize; i++)
            {
                if (choice % menuSize == i)
                    Console.WriteLine(" --> " + SCM.getList(i).PadRight(Console.BufferWidth - 6));
                else Console.WriteLine("     " + SCM.getList(i).PadRight(Console.BufferWidth - 6));
            }
        }
        //KONTROLER MENU
        public static void Main(string[] args)
        {
            bool choiceFlag = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            while (true)
            {
                PrintTask();
                while (!choiceFlag)
                {
                    var key = Console.ReadKey(intercept: true).Key;
                    switch (key)
                    {
                        // Menu UP
                        case ConsoleKey.W:
                        case ConsoleKey.UpArrow:
                            choice--;
                            if (choice < 0)
                                choice = 4;
                            choiceFlag = true;
                            break;
                        // Menu DOWN
                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            choice++;
                            choiceFlag = true;
                            break;
                        // Speed LEFT & RIGHT
                        case ConsoleKey.A:
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.D:
                        case ConsoleKey.RightArrow:
                            if(choice % 5 == 1)
                            {
                                SCM.changeSpeedOpt();
                            }
                            choiceFlag = true;
                            break;
                        // HOTKEYS
                        case ConsoleKey.F:
                            SCM.changeSpeedOpt();
                            choiceFlag = true;
                            break;
                        case ConsoleKey.Q:
                            Environment.Exit(0);
                            break;
                        case ConsoleKey.E:
                            SCM.startGame();
                            PrintTask();
                            break;
                        case ConsoleKey.C:
                            Console.Clear();
                            Console.WriteLine(Snake.gameControls());
                            break;
                        case ConsoleKey.Z:
                            Console.Clear();
                            Console.WriteLine(Snake.Credits());
                            break;
                        // SELECT OPT
                        case ConsoleKey.Spacebar:
                        case ConsoleKey.Enter:
                            // PLAY
                            if (choice % menuSize == 0)
                            {
                                SCM.startGame();
                                PrintTask();
                            }
                            // CONTROLS
                            if (choice % menuSize == 2)
                            {
                                Console.Clear();
                                Console.WriteLine(Snake.gameControls());
                            }
                            // CREDITS
                            if (choice % menuSize == 3)
                            {
                                Console.Clear();
                                Console.WriteLine(Snake.Credits());
                            }
                            // QUIT
                            if (choice % menuSize == 4)
                            {
                                Environment.Exit(0);
                            }
                            break;
                    }
                }
                choiceFlag = false;
            }
        }
    }
}