using System.Windows;
using ClassLibrary;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        Snake s;
        public MainWindow()
        {
            InitializeComponent();
            s = new Snake();
        }
        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            Window1 bWin = new Window1(s);
            bWin.Show();
            this.Close();
        }
        private void buttonNormal_Click(object sender, RoutedEventArgs e)
        {
            s.setSpeed(1);
            TextBlock1.Text = "Game Speed: Normal";
        }
        private void buttonFast_Click(object sender, RoutedEventArgs e)
        {
            s.setSpeed(2);
            TextBlock1.Text = "Game Speed: Fast";
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonCredits_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Snake.Credits());
        }
        private void buttonControls_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Snake.gameControls() + Snake.menuControls());
        }
    }
}
