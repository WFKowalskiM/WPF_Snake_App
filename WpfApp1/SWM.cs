using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp
{
    // (non)Static WPF Methods
    class SWM
    {
        
        private Brush snakeColor = Brushes.Green;
        private Canvas gameCanvas;
        private Snake s;
        public SWM(Canvas canvas, Snake snek)
        {
            this.gameCanvas = canvas;
            this.s = snek;
        }
        public void paintSnake(newPoint currentposition)
        {
            Ellipse newEllipse = new Ellipse();
            newEllipse.Fill = snakeColor;
            newEllipse.Width = s.getHeadSize();
            newEllipse.Height = s.getHeadSize();
            Canvas.SetTop(newEllipse, currentposition.getY());
            Canvas.SetLeft(newEllipse, currentposition.getX());
            int count = gameCanvas.Children.Count;
            gameCanvas.Children.Add(newEllipse);
            s.getSnakePoints().Add(new newPoint(currentposition));

            if (count > s.getLength())
            {
                gameCanvas.Children.RemoveAt(count - s.getLength() + 19);
                s.getSnakePoints().RemoveAt(count - s.getLength());
            }
        }
        public void paintBonus(int index)
        {
            newPoint bonusPoint = s.getBonusPoints()[index];
            Ellipse newEllipse = new Ellipse
            {
                Fill = Brushes.Red,
                Width = s.getHeadSize(),
                Height = s.getHeadSize()
            };
            Canvas.SetTop(newEllipse, bonusPoint.getY());
            Canvas.SetLeft(newEllipse, bonusPoint.getX());
            gameCanvas.Children.Insert(index, newEllipse);
        }
        public void deleteBonus(int index)
        {
            gameCanvas.Children.RemoveAt(index);
            paintBonus(index);
        }
    }
}
