using WPFLibrary;
using System;
using System.Windows;
using System.Windows.Input;

namespace WpfApp2
{
    public partial class Window1 : Window
    {
        private SnakeWPF? s;
        public Window1()
        {
            InitializeComponent();
            SnakeWPF s = new SnakeWPF(this, gameCanvas);
            s.InitializeGame();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Set focus to the window when it is initialized
            this.Activate();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            s.timer_Tick(sender, e);
        }
        public void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            s.OnButtonKeyDown(sender, e);
        }
    }
}

