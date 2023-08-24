using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WhiteBoard.View
{
    /// <summary>
    /// Interaction logic for DrawView.xaml
    /// </summary>
    public partial class DrawView : UserControl
    {
        private Stroke currentStroke;
        private Point startPoint;
        private Ellipse currentEllipse;

        public DrawView()
        {
            InitializeComponent();
        }

        private void EllipseBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("AAAAAAAAAAAAAAA SUKA");
            // Set EditingMode to Ink to enable drawing
            MyInkCanvas.EditingMode = InkCanvasEditingMode.Ink;

            // Set DefaultDrawingAttributes for drawing ellipses
            MyInkCanvas.DefaultDrawingAttributes.StylusTip = StylusTip.Ellipse;
            MyInkCanvas.DefaultDrawingAttributes.Width = 10;
            MyInkCanvas.DefaultDrawingAttributes.Height = 10;
            MyInkCanvas.DefaultDrawingAttributes.IgnorePressure = true; // You might want to adjust this

            // Attach Mouse events to handle drawing
            MyInkCanvas.MouseDown += MyInkCanvas_MouseDown;
            MyInkCanvas.MouseMove += MyInkCanvas_MouseMove;
            MyInkCanvas.MouseUp += MyInkCanvas_MouseUp;
        }

        private void MyInkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MyInkCanvas.EditingMode == InkCanvasEditingMode.None)
            {
                startPoint = e.GetPosition(ShapeCanvas);

                if (e.Source is Button button && button.Name == "EllipseButton")
                {
                    currentEllipse = new Ellipse
                    {
                        Stroke = Brushes.Black,
                        StrokeThickness = 2
                    };

                    ShapeCanvas.Children.Add(currentEllipse);
                    Canvas.SetLeft(currentEllipse, startPoint.X);
                    Canvas.SetTop(currentEllipse, startPoint.Y);
                }
            }
        }

        private void MyInkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentEllipse != null && e.LeftButton == MouseButtonState.Pressed)
            {
                double width = e.GetPosition(ShapeCanvas).X - startPoint.X;
                double height = e.GetPosition(ShapeCanvas).Y - startPoint.Y;

                currentEllipse.Width = Math.Abs(width);
                currentEllipse.Height = Math.Abs(height);
            }
        }

        private void MyInkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (MyInkCanvas.EditingMode == InkCanvasEditingMode.Ink)
            {
                currentEllipse = null;
            }
        }
    }
}