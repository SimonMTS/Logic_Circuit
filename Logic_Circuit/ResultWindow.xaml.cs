using Logic_Circuit.Controllers;
using Logic_Circuit.Models;
using Logic_Circuit.Models.BaseNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Logic_Circuit
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window, IResultWin
    {
        private readonly ResultController controller = new ResultController();

        public ResultWindow(Circuit circuit)
        {
            InitializeComponent();

            depthCounter = new int[circuit.Nodes.Count];

            Canvas.Width = 10;
            Canvas.Height = 10;

            controller.DrawButtons(circuit, this);
        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            controller.DrawLines(this);
        }

        private int[] depthCounter;
        public void DisplayNode(INode node, int depth)
        {
            depth--;

            Button btn = new Button
            {
                Width = 80,
                Height = 80,
                Margin = new Thickness(50 + (200 * depth), 50 + (150 * depthCounter[depth]) + (depth % 2 == 0 ? 0 : 80), 5, 5),
                Content = (node is CircuitNode ? node.Name + "\n(" + ((CircuitNode)node).Type + ")" : node.Name),
                Name = node.Name,
                Tag = (depthCounter[depth], depth),
                Background = controller.GetColor(node.Process()),
                Style = this.FindResource("HoverButton") as Style
            };

            btn.MouseEnter += new MouseEventHandler(Button_MouseEnter);
            btn.MouseLeave += new MouseEventHandler(Button_MouseLeave);
            btn.Click += Button_Click;

            Canvas.Children.Add(btn);
            Canvas.SetZIndex(btn, 3);

            if (Canvas.Height < 200 + (150 * depthCounter[depth]) + (depth % 2 == 0 ? 0 : 80)) Canvas.Height = 200 + (150 * depthCounter[depth]) + (depth % 2 == 0 ? 0 : 80);
            if (Canvas.Width < 200 + (200 * depth)) Canvas.Width = 200 + (200 * depth);

            depthCounter[depth]++;
        }

        public void DrawLine(INode startNode, INode endNode, Brush brush)
        {
            Button outputbtn = (Button)LogicalTreeHelper.FindLogicalNode(Canvas, endNode.Name);
            Button inputbtn = (Button)LogicalTreeHelper.FindLogicalNode(Canvas, startNode.Name);


            Point btn1Point = inputbtn.TransformToAncestor(Canvas).Transform(new Point(0, 0));
            Point btn2Point = outputbtn.TransformToAncestor(Canvas).Transform(new Point(0, 0));

            Point startPoint = new Point(btn1Point.X + inputbtn.ActualWidth, btn1Point.Y + inputbtn.ActualHeight / 2);
            Point endPoint = new Point(btn2Point.X, btn2Point.Y + outputbtn.ActualHeight / 2);

            int offset = (((int)((ValueTuple<int, int>)inputbtn.Tag).Item1) % 2 != 0 ? (((int)((ValueTuple<int, int>)inputbtn.Tag).Item1) * 2) : (((int)((ValueTuple<int, int>)inputbtn.Tag).Item1) - 1) * 2 * -1);
            int extraOffset = ((int)((ValueTuple<int, int>)inputbtn.Tag).Item2) % 2 == 0 ? -15 : 0;

            int prevLines = 0;
            while (((Line)LogicalTreeHelper.FindLogicalNode(Canvas, inputbtn.Name + "line1_" + prevLines)) != null)
            {
                prevLines++;
            }

            DrawLineSegment(
                inputbtn.Name + "line1_" + prevLines,
                brush,
                new Point(endPoint.X - 50 + offset + extraOffset + 1, startPoint.Y + offset),
                new Point(startPoint.X, startPoint.Y + offset)
            );

            DrawLineSegment(
                inputbtn.Name + "line2_" + prevLines,
                brush,
                new Point(endPoint.X - 50 + offset + extraOffset, startPoint.Y + offset),
                new Point(endPoint.X - 50 + offset + extraOffset, endPoint.Y + offset)
            );

            DrawLineSegment(
                inputbtn.Name + "line3_" + prevLines,
                brush,
                new Point(endPoint.X - 50 + offset + extraOffset - 1, endPoint.Y + offset),
                new Point(endPoint.X, endPoint.Y + offset)
            );

        }

        private void DrawLineSegment(string name, Brush brush, Point start, Point end)
        {
            Line line = new Line();
            line.Name = name;
            line.Stroke = brush;
            line.StrokeThickness = 2.0;
            line.X1 = start.X;
            line.Y1 = start.Y;
            line.X2 = end.X;
            line.Y2 = end.Y;
            Canvas.Children.Add(line);
            Panel.SetZIndex(line, 1);

            line.UpdateLayout();
            Canvas.UpdateLayout();
        }

        void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Background = controller.GetColor(btn.Background, false);

            int i = 0;
            while (((Line)LogicalTreeHelper.FindLogicalNode(Canvas, btn.Name + "line1_" + i)) != null)
            {
                Line line1 = ((Line)LogicalTreeHelper.FindLogicalNode(Canvas, btn.Name + "line1_" + i));
                line1.Stroke = controller.GetColor(line1.Stroke, false);
                line1.StrokeThickness = 2;
                Canvas.SetZIndex(line1, 1);

                Line line2 = ((Line)LogicalTreeHelper.FindLogicalNode(Canvas, btn.Name + "line2_" + i));
                line2.Stroke = controller.GetColor(line2.Stroke, false);
                line2.StrokeThickness = 2;
                Canvas.SetZIndex(line2, 1);

                Line line3 = ((Line)LogicalTreeHelper.FindLogicalNode(Canvas, btn.Name + "line3_" + i));
                line3.Stroke = controller.GetColor(line3.Stroke, false);
                line3.StrokeThickness = 2;
                Canvas.SetZIndex(line3, 1);

                i++;
            }
        }

        void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Background = controller.GetColor(btn.Background, true);

            int i = 0;
            while (((Line)LogicalTreeHelper.FindLogicalNode(Canvas, btn.Name + "line1_" + i)) != null)
            {
                Line line1 = ((Line)LogicalTreeHelper.FindLogicalNode(Canvas, btn.Name + "line1_" + i));
                line1.Stroke = controller.GetColor(line1.Stroke, true);
                line1.StrokeThickness = 3;
                Canvas.SetZIndex(line1, 3);

                Line line2 = ((Line)LogicalTreeHelper.FindLogicalNode(Canvas, btn.Name + "line2_" + i));
                line2.Stroke = controller.GetColor(line2.Stroke, true);
                line2.StrokeThickness = 3;
                Canvas.SetZIndex(line2, 3);

                Line line3 = ((Line)LogicalTreeHelper.FindLogicalNode(Canvas, btn.Name + "line3_" + i));
                line3.Stroke = controller.GetColor(line3.Stroke, true);
                line3.StrokeThickness = 3;
                Canvas.SetZIndex(line3, 3);

                i++;
            }
        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            controller.OnButtonClick(btn.Name, this);
        }

        public void SpawnNew(Circuit circuit)
        {
            ResultWindow result = new ResultWindow(circuit);
            result.SizeToContent = SizeToContent.WidthAndHeight;
            App.Current.MainWindow = result;
            result.Show();
        }

        public void ClearCanvas()
        {
            depthCounter = new int[depthCounter.Length];

            Canvas.Children.Clear();
            Canvas.UpdateLayout();

            Canvas.Width = 10;
            Canvas.Height = 10;
        }

        public void UpdateCanvas(Circuit circuit)
        {
            /*Canvas.UpdateLayout();*/

            foreach (var child in Canvas.Children)
            {
                string nodeName = Regex.Replace(((FrameworkElement)child).Name, @"line\d_\d*", "");
                INode node = circuit.Nodes[nodeName];

                if (child is Line)
                {
                    ((Line)child).Stroke = controller.GetColor(node.Process());

                }

                if (child is Button)
                {
                    ((Button)child).Background = controller.GetColor(node.Process());
                }
            }
        }

    }
}
