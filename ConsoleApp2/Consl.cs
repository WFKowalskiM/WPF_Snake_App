using System;
using System.Threading;
using ClassLibrary;
using Projekt;

class Program
{
    static SnakeConsole snake;

    static void Main()
    {
        Console.WriteLine("Console Snake Game");
        Console.WriteLine("Use arrow keys to control the snake.");
        snake = new SnakeConsole();
        //snake.mainMenu();
        snake.mainLoop();
    }
    public static void initGame() 
    {
    }
    static void ReadInput()
    {
        
        do
        {
            
        } while (!snake.NO);
    }
    private void paintSnake() 
    {
        for (int i = 0; i < snake.snakePoints.Count; i++)
        {
            setCursor((int)snake.snakePoints[i].x, (int)snake.snakePoints[i].y);
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
    public void paintBonus(int x, int y)
    {
        setCursor(x, y);
        Console.Write("O");
    }
    private void setCursor(int x, int y) 
    {
        Console.SetCursorPosition(x, y);
    }

}