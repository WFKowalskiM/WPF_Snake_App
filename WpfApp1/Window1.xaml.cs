using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using ClassLibrary;
using System.Collections.Generic;

namespace WpfApp
{
    public partial class Window1 : Window
    {
        private Snake? s;
        private SWM SWM;
        public Window1(Snake snak)
        {
            InitializeComponent();
            s = snak;//new Snake();
            SWM = new SWM(gameCanvas, s);
            s.InitializeGame(30, 100, 12, 10, 620, 380);
            SWM.paintSnake(s.getCurrentPos());

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(10000);
            timer.Start();
            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);

            for (int n = 0; n < 20; n++)
            {
                SWM.paintBonus(n);
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (s.gameLoop())
            {
                SWM.deleteBonus(s.getBonusIndex());
            }
            if (s.DOISTOP())
            {
                this.Close();
            }
            SWM.paintSnake(s.getCurrentPos());
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            base.OnClosing(e);
        }
        public void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.S:
                case Key.Down:
                    s.changeDirection("Down");
                    break;
                case Key.W:
                case Key.Up:
                    s.changeDirection("Up");
                    break;
                case Key.A:
                case Key.Left:
                    s.changeDirection("Left");
                    break;
                case Key.D:
                case Key.Right:
                    s.changeDirection("Right");
                    break;
            }
        }
        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
    }
}

