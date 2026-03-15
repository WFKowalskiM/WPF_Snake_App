using WPFLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            //bWin.Owner = this;
            TextBlock1.Text = "Latest Score: " + SnakeLogic.latestScore;
            TextBlock2.Text = "Best Score: " + SnakeLogic.bestScore;
        }
        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            Window1 bWin = new Window1();
            bWin.Show();
            this.Close();
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonCredits_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(SnakeLogic.Credits());
        }
    }
}
