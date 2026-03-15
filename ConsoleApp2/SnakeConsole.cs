using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClassLibrary;
using System.Threading;

namespace Projekt
{
    public class SnakeConsole : SnakeBase
    {
        public override void paintSnake(newPoint currentposition)
        {
            for (int i = 0; i < snakePoints.Count; i++)
            {
                setCursor((int)snakePoints[i].x, (int)snakePoints[i].y);
                if (i == 0)
                {
                    Console.Write("■");
                }
                else
                {
                    Console.Write("●");
                }
            }
            Thread.Sleep(100);
            Console.Clear();
        }
        public override void paintBonus(int index)
        {
            newPoint bonusPoint = new newPoint(rnd.Next(0, 30), rnd.Next(0, 15));
            setCursor((int)bonusPoint.x, (int)bonusPoint.y);
            Console.Write("$");
        }
        public override void deleteBonus(int index)
        {
            bonusPoints.RemoveAt(index);
            paintBonus(index);
        }
        public void mainLoop() 
        {
            Thread inputThread = new Thread(detectKey);
            inputThread.Start();
            InitializeGame();
            while (!NO)
            {
                paintSnake(startingPoint);
                Thread.Sleep(100);
            }
            //GameOver();

            inputThread.Join();
        }
        public void mainMenu() 
        { 
        
        }
        public void detectKey() 
        {
            ConsoleKeyInfo keyInfo;
            keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                default:
                    break;
                case ConsoleKey.UpArrow:
                    changeDirection("Up");
                    break;
                case ConsoleKey.DownArrow:
                    changeDirection("Down");
                    break;
                case ConsoleKey.LeftArrow:
                    changeDirection("Left");
                    break;
                case ConsoleKey.RightArrow:
                    changeDirection("Right");
                    break;
            }
        }
        public override void InitializeGame()
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            NO = false;
            startingPoint.copy(new newPoint(3, 3));
        }
        public void setCursor(int x, int y) 
        {
            Console.SetCursorPosition(x, y);
        }
        public void GameOver()
        {
            base.GameOver();
            Console.Clear();
            Console.WriteLine("Game Over!");
            Console.WriteLine($"Score: {score}");
            Console.WriteLine($"Best Score: {SnakeBase.bestScore}");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
