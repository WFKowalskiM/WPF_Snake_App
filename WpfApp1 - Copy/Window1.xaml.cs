using WPFLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;

namespace WpfApp1
{
    public partial class Window1 : Window
    {
        /*
        // This list describes the Bonus Red pieces of Food on the Canvas
        private List<newPoint> bonusPoints = new List<newPoint>();
        // This list describes the body of the snake on the Canvas
        private List<newPoint> snakePoints = new List<newPoint>();
        private Brush snakeColor = Brushes.Green;
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
        //private WPFSnake snake = new WPFSnake();
        private newPoint startingPoint = new newPoint(100, 100);
        private newPoint currentPosition = new newPoint();
        // Movement direction initialisation
        private int direction = 0;

        private int previousDirection = 0;

        private int headSize = (int)SIZE.THICK;
        private int length = 30;
        private int score = 0;
        private Random rnd = new Random();
        */
        private SnakeLogic s;
        public Window1()
        {
            InitializeComponent();
            SnakeLogic s = new SnakeLogic(this, gameCanvas, CreateMainWindow);
            /*
            NO = false;
            Console.WriteLine("balls");
            
            //this.Owner = App.Current.MainWindow;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = MODERATE;
            timer.Start();
            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            paintSnake(startingPoint);
            currentPosition.copy(startingPoint);
            // Instantiate Food Objects
            for (int n = 0; n < 10; n++)
            {
                paintBonus(n);
            }
            */
            s.InitializeGame();
        }
        /*
        private void paintSnake(newPoint currentposition)
        {
            /*
            // This method is used to paint a frame of the snake´s body
            // each time it is called.
            Ellipse newEllipse = new Ellipse();
            newEllipse.Fill = snakeColor;
            newEllipse.Width = headSize;
            newEllipse.Height = headSize;
            Canvas.SetTop(newEllipse, currentposition.y);
            Canvas.SetLeft(newEllipse, currentposition.x);
            int count = gameCanvas.Children.Count;
            gameCanvas.Children.Add(newEllipse);
            snakePoints.Add(new newPoint(currentposition));
            // Restrict the tail of the snake
            if (count > length)
            {
                gameCanvas.Children.RemoveAt(count - length + 9);
                snakePoints.RemoveAt(count - length);
            }
        }

        private void paintBonus(int index)
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
        */
        private void timer_Tick(object sender, EventArgs e)
        {
            /*
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

            // Restrict to boundaries of the Canvas
            if ((currentPosition.x < 5) || (currentPosition.x > 620) || (currentPosition.y < 5) || (currentPosition.y > 380))
                        GameOver();

                // Hitting a bonus Point causes the lengthen-Snake Effect
                int n = 0;
                foreach (newPoint point in bonusPoints)
                {
                    //newPoint point = new newPoint(snakePoints[q].x, snakePoints[q].y);
                    if ((Math.Abs(point.x - currentPosition.x) < headSize) &&
                        (Math.Abs(point.y - currentPosition.y) < headSize))
                    {
                        length += 10;
                        score += 10;
                        // In the case of food consumption, erase the food object
                        // from the list of bonuses as well as from the canvas
                        bonusPoints.RemoveAt(n);
                        gameCanvas.Children.RemoveAt(n);
                        paintBonus(n);
                        break;
                    }
                    n++;
                }
                // Restrict hits to body of Snake
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
            */
            s.timer_Tick(sender, e);
        }
        private void CreateMainWindow()
        {
            // Callback to create MainWindow
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }
        public void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            /*
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
            */
            s.OnButtonKeyDown(sender, e);
        }
        /*private void GameOver()
        {
            if (!NO)
            {
                SnakeLogic.latestScore = score;
                if (SnakeLogic.bestScore <= score)
                {
                    SnakeLogic.bestScore = score;
                }
                MainWindow mainWindow = new MainWindow();
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
            }
            NO = true;
            this.Close();
        }
        */
        }
    }

