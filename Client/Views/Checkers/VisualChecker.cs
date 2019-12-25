using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Domain.Entities;

namespace OnlineCheckers.Client.Views.Checkers
{
    public class VisualChecker : VisualCheckerShape
    {
        public CChecker Checker
        {
            get => _checker;
            set
            {
                _checker = value;

                Fill = (_checker.Id >= 1 && _checker.Id <= 12)
                    ? new SolidColorBrush(Color.FromRgb(0, 0, 0))
                    : new SolidColorBrush(Color.FromRgb(255, 255, 255));

                Visibility = (_checker.IsAlive) ? Visibility.Visible : Visibility.Hidden;

                Canvas.SetTop(this, _checker.Location.Y);
                Canvas.SetLeft(this, _checker.Location.X);
            }
        }

        private CChecker _checker;
    }
}
