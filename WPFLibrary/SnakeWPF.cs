// SnakeLogic.cs
using System;
using System.Collections.Generic;

namespace WPFLibrary
{
    public class SnakeWPF
    {
       
        
        public List<newPoint> bonusPoints = new List<newPoint>();
        public List<newPoint> snakePoints = new List<newPoint>();
        public bool STOP = false;

        private enum MOVINGDIRECTION
        {
            UPWARDS = 8,
            DOWNWARDS = 2,
            TOLEFT = 4,
            TORIGHT = 6
        };
        //private newPoint startingPoint = new newPoint();
        private newPoint currentPosition = new newPoint();
        private int direction = 6;
        public static int headSize;
        private int previousDirection = 6;
        private int length = 30;
        
        public Random rnd = new Random();
        public SnakeWPF()
        {
        }

        public static string Credits()
        {
            string cat =
            "Credits:\n" +
            "    Me, for this\n" +
            "    Internet CSS resources, for massive help coding \n" +
            "        for the game of snake AGAIN\n" +
            "    Taneli Armanto, for inventing the game of snake\n";
            return cat;
        }
        public void InitializeGame()
        {
            STOP = false;
            //startingPoint.copy(new newPoint(100, 100));
            currentPosition.copy(new newPoint(100, 100));
        }
        public void moveSnake()
        {
            switch (direction)
            {
                case (int)MOVINGDIRECTION.DOWNWARDS:
                    currentPosition.y += 1;
                    break;
                case (int)MOVINGDIRECTION.UPWARDS:
                    currentPosition.y -= 1;
                    break;
                case (int)MOVINGDIRECTION.TOLEFT:
                    currentPosition.x -= 1;
                    break;
                case (int)MOVINGDIRECTION.TORIGHT:
                    currentPosition.x += 1;
                    break;
            }
        }
        public bool checkBodyDistance(newPoint point)
        {
            if ((Math.Abs(point.x - currentPosition.x) < headSize) && (Math.Abs(point.y - currentPosition.y) < headSize))
                return true;
            else
                return false;
        }
        public int checkFoodCollision()
        {
            int n = 0;
            foreach (newPoint point in bonusPoints)
            {
                if (checkBodyDistance(point))
                {
                    length += 10;

                    bonusPoints.RemoveAt(n);
                    //deleteBonus(n);
                    return n;
                }
                n++;
            }
            return -1;
        }
        public void checkBodyCollision()
        {
            for (int q = 1; q < (snakePoints.Count - headSize * 2); q++)
            {
                newPoint point = new newPoint(snakePoints[q].x, snakePoints[q].y);
                if ((Math.Abs(point.x - currentPosition.x) < (headSize/2)) &&
                    (Math.Abs(point.y - currentPosition.y) < (headSize/2)))
                {
                    GameOver();
                    break;
                }
            }
        }
        public void changeDirection(string dir)
        {
            switch (dir)
            {
                default:
                    break;
                case "Up":
                    if (previousDirection != (int)MOVINGDIRECTION.DOWNWARDS)
                        direction = (int)MOVINGDIRECTION.UPWARDS;
                    break;
                case "Down":
                    if (previousDirection != (int)MOVINGDIRECTION.UPWARDS)
                        direction = (int)MOVINGDIRECTION.DOWNWARDS;
                    break;
                case "Left":
                    if (previousDirection != (int)MOVINGDIRECTION.TORIGHT)
                        direction = (int)MOVINGDIRECTION.TOLEFT;
                    break;
                case "Right":
                    if (previousDirection != (int)MOVINGDIRECTION.TOLEFT)
                        direction = (int)MOVINGDIRECTION.TORIGHT;
                    break;
            }
            previousDirection = direction;
        }
        public newPoint getCurrentPos()
        {
            return currentPosition;
        }
        public int getLength()
        {
            return length;
        }
        public void checkBoundries(double posX, double poxY, int maxX, int maxY)
        {
            if ((posX < 0) || (posX > maxX) || (poxY < 0) || (poxY > maxY))
                GameOver();
        }
        public void GameOver()
        {
            STOP = true;
        }
    }
    public class newPoint
    {
        public double x, y;
        public newPoint() { }
        public newPoint(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public newPoint(newPoint p)
        {
            this.x = p.x;
            this.y = p.y;
        }
        public void copy(newPoint p)
        {
            this.x = p.x;
            this.y = p.y;
        }
    }
}
