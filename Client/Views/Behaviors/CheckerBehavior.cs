using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using Domain.ValueTypes;
using OnlineCheckers.Client.ViewModels;
using OnlineCheckers.Client.Views.Checkers;
using OnlineCheckers.Client.Views.Pages.Subpages;

namespace OnlineCheckers.Client.Views.Behaviors
{
    public class CheckerBehavior : Behavior<UIElement>
    {
        private Canvas _canvas;

        private bool _isDragging = false;

        private Point _mouseOffset;

        private Size _figureSize;

        private Point _lastPoint;

        private MovableVisualChecker _visualChecker;

        private Int32 _startColumn;

        private Int32 _startRow;


        protected override void OnAttached()
        {
            base.OnAttached();

            _visualChecker = AssociatedObject as MovableVisualChecker;

            if (_visualChecker != null)
            {
                _canvas = VisualTreeHelper.GetParent(_visualChecker) as Canvas;

                if (_canvas != null)
                {
                    _lastPoint = new Point();
                    _figureSize.Height = _visualChecker.Height;
                    _figureSize.Width = _visualChecker.Width;


                    _visualChecker.PreviewMouseDown += _visualChecker_PreviewMouseDown;
                    _visualChecker.PreviewMouseMove += _visualChecker_PreviewMouseMove;
                    _visualChecker.PreviewMouseUp += _visualChecker_PreviewMouseUp;
                }
            }
        }


        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (_visualChecker != null)
            {
                _visualChecker.PreviewMouseDown -= _visualChecker_PreviewMouseDown;
                _visualChecker.PreviewMouseMove -= _visualChecker_PreviewMouseMove;
                _visualChecker.PreviewMouseUp -= _visualChecker_PreviewMouseUp;
            }
        }

        private void _visualChecker_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //_visualChecker.Player.BeActive(_visualChecker.Checker);

            _visualChecker.Player.BeActive(_visualChecker.Checker);

            if (_visualChecker.Player.IsActive)
            {
                _isDragging = true;
                // Координаты курсора относилетьно начала перетаскиваемой фигуры
                _mouseOffset = e.GetPosition(_visualChecker);
                _visualChecker.CaptureMouse();

                // Запоминаем старые координаты
                _startColumn = GetСoordinateNumber(_canvas.Width, _lastPoint.X, _figureSize.Width); // Получение номера столбца
                _startRow = GetСoordinateNumber(_canvas.Height, _lastPoint.Y, _figureSize.Height); // Получение номера строки
            }
        }

        private void _visualChecker_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // Координаты курсора относительно шахматного поля (родительского контейнера)
                Point point = e.GetPosition(_canvas);
                double maxX = _canvas.Width - _figureSize.Width;
                double maxY = _canvas.Height - _figureSize.Height;
                double x = point.X - _mouseOffset.X;
                double y = point.Y - _mouseOffset.Y;

                _lastPoint.X = x < 0 ? 0.0 : (x > maxX ? maxX : x);
                _lastPoint.Y = y < 0 ? 0.0 : (y > maxY ? maxY : y);

                //_visualChecker.Checker.X = _lastPoint.X;
                //_visualChecker.Checker.Y = _lastPoint.Y;

                _visualChecker.SetValue(Canvas.LeftProperty, _lastPoint.X);
                _visualChecker.SetValue(Canvas.TopProperty, _lastPoint.Y);
            }
        }

        private void _visualChecker_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDragging)
            {
                _isDragging = false;

                _visualChecker.ReleaseStylusCapture();

                Int32 newColumn = GetСoordinateNumber(_canvas.Width, _lastPoint.X, _figureSize.Width);  // Получение номера столбца
                Int32 newRow = GetСoordinateNumber(_canvas.Height, _lastPoint.Y, _figureSize.Height);   // Получение номера строки

                SetCoordinate(newColumn, newRow, _canvas.Width, _canvas.Height);
            }
        }

        private int GetСoordinateNumber(double maxCoordinate, double currentCoordinate, double sideSizeFigure)
        {
            //  Определение ширины / высоты ячейки
            double partLength = maxCoordinate / 8;
            //  Определение номера ячейки (0-7), в которой находится верхний левый угол фигуры
            int result = (int)(currentCoordinate / partLength);
            //  Определение номера ячейки, в которой находится большая часть фигуры
            return (currentCoordinate - result * partLength + sideSizeFigure / 2 > partLength) ? ++result : result;
        }

        //  column / row: 0-7
        private void SetCoordinate(int column, int row, double width, double height)
        {
            double x = column * (width / 8) + ((width / 8) - _figureSize.Width) / 2;
            double y = row * (height / 8) + ((height / 8) - _figureSize.Height) / 2;

            //_visualChecker.Checker.X = x;
            //_visualChecker.Checker.Y = y;

            _visualChecker.SetValue(Canvas.LeftProperty, x);
            _visualChecker.SetValue(Canvas.TopProperty, y);

            if (_visualChecker.Player.IsActive)
            {
                if (column != _startColumn || row != _startRow)
                    _visualChecker.Player.Move(new CLocation(x, y));
                else
                    _visualChecker.Player.LoseMoving();
            }
        }
    }
}
