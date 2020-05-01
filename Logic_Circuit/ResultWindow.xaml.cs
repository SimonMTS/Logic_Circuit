using Logic_Circuit.Controllers;
using Logic_Circuit.Models;
using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Circuits;
using Logic_Circuit.Models.Nodes.NodeInputTypes;
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
                Margin = new Thickness(50 + (200 * depth), 50 + (200 * depthCounter[depth]) + (depth % 2 == 0 ? 0 : 100), 5, 5),
                Content = (node is IMultipleInputs ? node.Name + "\n(" + ((IMultipleInputs)node).Type + ")" : node.Name),
                Name = node.Name,
                Tag = (depthCounter[depth], depth),
                Background = controller.GetColor(node),
                Style = this.FindResource("HoverButton") as Style
            };

            btn.MouseEnter += new MouseEventHandler(Button_MouseEnter);
            btn.MouseLeave += new MouseEventHandler(Button_MouseLeave);
            btn.Click += Button_Click;

            Canvas.Children.Add(btn);
            Canvas.SetZIndex(btn, 3);

            TextBlock txt = new TextBlock
            {
                Name = "nano",
                Width = 80,
                Height = 20,
                Margin = new Thickness(50 + (200 * depth), 130 + (200 * depthCounter[depth]) + (depth % 2 == 0 ? 0 : 100), 5, 5),
                Text = (node.RealDepth * 15) + " nanosec.",
                TextAlignment = TextAlignment.Right
            };

            Canvas.Children.Add(txt);
            Canvas.SetZIndex(txt, 5);

            if (Canvas.Height < 200 + (200 * depthCounter[depth]) + (depth % 2 == 0 ? 0 : 100)) Canvas.Height = 200 + (200 * depthCounter[depth]) + (depth % 2 == 0 ? 0 : 100);
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

            int offsetX = ((int)((ValueTuple<int, int>)inputbtn.Tag).Item1) * 6;
            int offsetY = ((int)((ValueTuple<int, int>)inputbtn.Tag).Item2 + 1) * (((int)((ValueTuple<int, int>)inputbtn.Tag).Item1) * 4);

            if (offsetY >= 40)
            {
                offsetY = offsetY - 32;
                offsetY = offsetY - (offsetY * 2);
            }

            int prevLines = 0;
            while (((Line)LogicalTreeHelper.FindLogicalNode(Canvas, inputbtn.Name + "line1_" + prevLines)) != null)
            {
                prevLines++;
            }

            DrawLineSegment(
                inputbtn.Name + "line1_" + prevLines,
                brush,
                new Point(endPoint.X - 50 + 1 + offsetX, startPoint.Y + offsetY),
                new Point(startPoint.X, startPoint.Y + offsetY)
            );

            DrawLineSegment(
                inputbtn.Name + "line2_" + prevLines,
                brush,
                new Point(endPoint.X - 50 + offsetX, startPoint.Y + offsetY),
                new Point(endPoint.X - 50 + offsetX, endPoint.Y + offsetY)
            );

            DrawLineSegment(
                inputbtn.Name + "line3_" + prevLines,
                brush,
                new Point(endPoint.X - 50 - 1 + offsetX, endPoint.Y + offsetY),
                new Point(endPoint.X, endPoint.Y + offsetY)
            );

        }

        private void DrawLineSegment(string name, Brush brush, Point start, Point end)
        {
            Line line = new Line
            {
                Name = name,
                Stroke = brush,
                StrokeThickness = 2.0,
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y
            };
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

        public void SpawnNew(Circuit circuit, string title)
        {
            ResultWindow result = new ResultWindow(circuit);
            result.SizeToContent = SizeToContent.WidthAndHeight;
            result.Title = title;
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
            foreach (var child in Canvas.Children)
            {
                string nodeName = Regex.Replace(((FrameworkElement)child).Name, @"line\d_\d*", "");

                if (nodeName == "nano") continue;

                INode node = circuit.Nodes[nodeName];

                if (child is Line)
                {
                    ((Line)child).Stroke = controller.GetColor(node);

                }

                if (child is Button)
                {
                    ((Button)child).Background = controller.GetColor(node);
                }
            }
        }

    }
}
