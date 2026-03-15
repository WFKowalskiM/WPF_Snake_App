// SnakeLogic.cs
using System;
using System.Collections.Generic;
using System.Net;

namespace ClassLibrary
{
    // PUNKT
    public class newPoint
    {
        private int x, y;
        public newPoint() { }
        public newPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public newPoint(newPoint p)
        {
            this.x = p.getX();
            this.y = p.getY();
        }
        public int getX()
        {
            return x;
        }
        public int getY()
        {
            return y;
        }
        public void setX(int x) 
        {
            this.x = x;
        }
        public void setY(int y)
        {
            this.y = y;
        }
    }
    // SNAKE
    public class Snake
    {
        private List<newPoint> bonusPoints = new List<newPoint>();
        private List<newPoint> snakePoints = new List<newPoint>();

        private enum MOVINGDIRECTION
        {
            UPWARDS = 8,
            DOWNWARDS = 2,
            TOLEFT = 4,
            TORIGHT = 6
        };

        private newPoint currentPosition = new newPoint();
        private int direction = 6;
        private int previousDirection = 6;
        private int headSize;
        private int bonusIndex = -1;
        private int length;
        private int score;
        private int width;
        private int height;
        private int speed = 1;
        private bool STOP = false;

        public Random rnd = new Random();
        public Snake()
        {
        }

        // LOGIKA
        public void InitializeGame(int length, int start, int head, int score, int width, int height)
        {
            STOP = false;
            currentPosition = new newPoint(start, start);
            this.length = length;
            this.headSize = head;
            this.score = score;
            this.width = width;
            this.height = height;
            for (int i = 0; i < 20; i++)
            {
                bonusPoints.Add(new newPoint(rnd.Next(2, width - 2), rnd.Next(2, height - 2)));
            }
        }
        public bool gameLoop()
        {
            bool catchFood = false;
            moveSnake();
            checkBoundries(getCurrentPos().getX(), getCurrentPos().getY(), getWidth() - 5, getHeight() - 5);
            checkFoodCollision();
            if (getBonusIndex() > -1)
            {
                catchFood = true;
            }
            checkBodyCollision();
            return catchFood;
        }
        public void moveSnake()
        {
            switch (direction)
            {
                case (int)MOVINGDIRECTION.UPWARDS:
                    currentPosition.setY(currentPosition.getY() - speed);
                    break;
                case (int)MOVINGDIRECTION.DOWNWARDS:
                    currentPosition.setY(currentPosition.getY() + speed);
                    break;
                case (int)MOVINGDIRECTION.TOLEFT:
                    currentPosition.setX(currentPosition.getX() - speed);
                    break;
                case (int)MOVINGDIRECTION.TORIGHT:
                    currentPosition.setX(currentPosition.getX() + speed);
                    break;
            }
        }
        public bool checkBodyDistance(newPoint point)
        {
            if ((Math.Abs(point.getX() - currentPosition.getX()) <= headSize) && (Math.Abs(point.getY() - currentPosition.getY()) <= headSize))
                return true;
            else
                return false;
        }
        public void checkFoodCollision()
        {
            bonusIndex = -1;
            foreach (newPoint point in bonusPoints)
            {
                if (checkBodyDistance(point))
                {
                    length += score;
                    bonusIndex = bonusPoints.IndexOf(point);
                    bonusPoints.RemoveAt(bonusIndex);
                    bonusPoints.Insert(bonusIndex, new newPoint(rnd.Next(2, width-2), rnd.Next(2, height-2)));
                    break;
                }
            }
        }
        public void checkBodyCollision()
        {
            for (int q = 1; q < (snakePoints.Count - headSize * 2); q++)
            {
                newPoint point = new newPoint(snakePoints[q].getX(), snakePoints[q].getY());
                if ((Math.Abs(point.getX() - currentPosition.getX()) <= (headSize / 2)) &&
                    (Math.Abs(point.getY() - currentPosition.getY()) <= (headSize / 2)))
                {
                    STOP = true;
                    break;
                }
            }
        }
        public void checkBoundries(double posX, double poxY, int maxX, int maxY)
        {
            if ((posX < 0) || (posX > maxX) || (poxY < 0) || (poxY > maxY))
                STOP = true;
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

        // TEKST
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
        public static string gameControls()
        {
            string cat =
                "Game Controls:\n" +
                "Use WSAD or Arrow Keys to move,\nPress Q to quit.\n";
            return cat;
        }
        public static string menuControls()
        {
            string cat =
                "Menu Hotkeys:\n" +
                "Start Game - E\n" +
                "Select Game Speed: Normal - 1, Fast - 2\n" +
                "View Controls - C\n" +
                "View Credits - Z\n" +
                "Quit Game - Q";
            return cat;
        }


        // GET / SET
        public newPoint getCurrentPos()
        {
            return currentPosition;
        }
        public int getLength()
        {
            return length;
        }
        public int getWidth()
        {
            return width;
        }
        public int getHeight()
        {
            return height;
        }
        public int getBonusIndex()
        {
            return bonusIndex;
        }
        public int getHeadSize()
        {
            return headSize;
        }
        public List<newPoint> getSnakePoints()
        {
            return snakePoints;
        }
        public List<newPoint> getBonusPoints()
        {
            return bonusPoints;
        }
        public bool DOISTOP()
        {
            return STOP;
        }
        public void STOPIT(bool yes)
        {
            STOP = yes;
        }
        public void setSpeed(int x)
        {
            speed = x;
        }
    }
}
