// SnakeLogic.cs
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPFLibrary
{
    public class SnakeLogic
    {
        private Window w;
        private Canvas gameCanvas;
        private List<newPoint> bonusPoints = new List<newPoint>();
        private List<newPoint> snakePoints = new List<newPoint>();
        private Action createMainWindowCallback;
        private Brush snakeColor = Brushes.Green;
        public SnakeLogic(Window w, Canvas c, Action createMainWindowCallback) 
        {
            this.w = w;
            this.gameCanvas = c;
            this.createMainWindowCallback = createMainWindowCallback;
        }
        private enum SIZE
        {
            THIN = 4,
            NORMAL = 6,
            THICK = 8
        };

        private bool NO = false;

        private enum MOVINGDIRECTION
        {
            UPWARDS = 8,
            DOWNWARDS = 2,
            TOLEFT = 4,
            TORIGHT = 6
        };

        private TimeSpan FAST = new TimeSpan(1);
        private TimeSpan MODERATE = new TimeSpan(10000);
        private TimeSpan SLOW = new TimeSpan(50000);
        private TimeSpan DAMNSLOW = new TimeSpan(500000);

        private newPoint startingPoint = new newPoint(100, 100);
        private newPoint currentPosition = new newPoint();
        private int direction = 0;
        private int previousDirection = 0;
        private int headSize = (int)SIZE.THICK;
        private int length = 30;
        private int score = 0;
        public static int bestScore = 0;
        public static int latestScore = 0;
        private Random rnd = new Random();

        public void InitializeGame()
        {
            NO = false;
            Console.WriteLine("balls");

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = MODERATE;
            timer.Start();

            w.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            paintSnake(startingPoint);

            currentPosition.copy(startingPoint);

            for (int n = 0; n < 10; n++)
            {
                paintBonus(n);
            }
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            switch (direction)
            {
                case (int)MOVINGDIRECTION.DOWNWARDS:
                    currentPosition.y += 1;
                    paintSnake(currentPosition);
                    break;
                case (int)MOVINGDIRECTION.UPWARDS:
                    currentPosition.y -= 1;
                    paintSnake(currentPosition);
                    break;
                case (int)MOVINGDIRECTION.TOLEFT:
                    currentPosition.x -= 1;
                    paintSnake(currentPosition);
                    break;
                case (int)MOVINGDIRECTION.TORIGHT:
                    currentPosition.x += 1;
                    paintSnake(currentPosition);
                    break;
            }

            if ((currentPosition.x < 5) || (currentPosition.x > 620) || (currentPosition.y < 5) || (currentPosition.y > 380))
                GameOver();

            int n = 0;
            foreach (newPoint point in bonusPoints)
            {
                if ((Math.Abs(point.x - currentPosition.x) < headSize) &&
                    (Math.Abs(point.y - currentPosition.y) < headSize))
                {
                    length += 10;
                    score += 10;

                    bonusPoints.RemoveAt(n);
                    gameCanvas.Children.RemoveAt(n);
                    paintBonus(n);
                    break;
                }
                n++;
            }

            for (int q = 0; q < (snakePoints.Count - headSize * 2); q++)
            {
                newPoint point = new newPoint(snakePoints[q].x, snakePoints[q].y);
                if ((Math.Abs(point.x - currentPosition.x) < (headSize)) &&
                    (Math.Abs(point.y - currentPosition.y) < (headSize)))
                {
                    GameOver();
                    break;
                }
            }
        }

        public void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    if (previousDirection != (int)MOVINGDIRECTION.UPWARDS)
                        direction = (int)MOVINGDIRECTION.DOWNWARDS;
                    break;
                case Key.Up:
                    if (previousDirection != (int)MOVINGDIRECTION.DOWNWARDS)
                        direction = (int)MOVINGDIRECTION.UPWARDS;
                    break;
                case Key.Left:
                    if (previousDirection != (int)MOVINGDIRECTION.TORIGHT)
                        direction = (int)MOVINGDIRECTION.TOLEFT;
                    break;
                case Key.Right:
                    if (previousDirection != (int)MOVINGDIRECTION.TOLEFT)
                        direction = (int)MOVINGDIRECTION.TORIGHT;
                    break;
            }
        }
        public static string Credits()
        {
            string cat =
            "Credits:\n" +
            "    Me, for all of this\n" +
            "    Internet CSS resources, for massive help coding \n" +
            "        for the game of snake AGAIN\n" +
            "    Taneli Armanto, for inventing the game of snake\n";
            return cat;
        }
        public void GameOver()
        {
            if (!NO)
            {
                latestScore = score;
                if (bestScore <= score)
                {
                    bestScore = score;
                }
                createMainWindowCallback.Invoke();
            }
            NO = true;
            w.Close();
        }

        public virtual void paintSnake(newPoint currentposition)
        {
            Ellipse newEllipse = new Ellipse();
            newEllipse.Fill = snakeColor;
            newEllipse.Width = headSize;
            newEllipse.Height = headSize;
            Canvas.SetTop(newEllipse, currentposition.y);
            Canvas.SetLeft(newEllipse, currentposition.x);
            int count = gameCanvas.Children.Count;
            gameCanvas.Children.Add(newEllipse);
            snakePoints.Add(new newPoint(currentposition));

            if (count > length)
            {
                gameCanvas.Children.RemoveAt(count - length + 9);
                snakePoints.RemoveAt(count - length);
            }
        }

        public virtual void paintBonus(int index)
        {
            newPoint bonusPoint = new newPoint(rnd.Next(5, 620), rnd.Next(5, 380));
            Ellipse newEllipse = new Ellipse();
            newEllipse.Fill = Brushes.Red;
            newEllipse.Width = headSize;
            newEllipse.Height = headSize;

            Canvas.SetTop(newEllipse, bonusPoint.y);
            Canvas.SetLeft(newEllipse, bonusPoint.x);
            gameCanvas.Children.Insert(index, newEllipse);
            bonusPoints.Insert(index, bonusPoint);
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
