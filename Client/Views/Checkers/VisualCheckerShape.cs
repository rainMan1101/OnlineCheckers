using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OnlineCheckers.Client.Views.Checkers
{
    // Потом можно будет нарисовать шашку покрасивее в этом классе
    public class VisualCheckerShape : Shape
    {
        private EllipseGeometry _ellipse;

        public VisualCheckerShape()
        {
            _ellipse = new EllipseGeometry();
            Height = 40;
            Width = 40;
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                TranslateTransform t = new TranslateTransform(ActualWidth / 2, ActualHeight / 2);
                _ellipse.Transform = t;
                _ellipse.RadiusX = this.ActualWidth / 2;
                _ellipse.RadiusY = this.ActualHeight / 2;
                return _ellipse;
            }
        }
    }
}
