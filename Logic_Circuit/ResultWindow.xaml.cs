using Logic_Circuit.Models;
using Logic_Circuit.Models.BaseNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Logic_Circuit
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        private Brush Green1 = (Brush)new BrushConverter().ConvertFrom("#4CAF50"); // 500
        private Brush Green2 = (Brush)new BrushConverter().ConvertFrom("#388E3C"); // 700
        private Brush Green3 = (Brush)new BrushConverter().ConvertFrom("#81C784"); // 300
        private Brush Green4 = (Brush)new BrushConverter().ConvertFrom("#1B5E20"); // 900

        private Brush Red1 = (Brush)new BrushConverter().ConvertFrom("#F44336"); // 500
        private Brush Red2 = (Brush)new BrushConverter().ConvertFrom("#D32F2F"); // 700
        private Brush Red3 = (Brush)new BrushConverter().ConvertFrom("#E57373"); // 300
        private Brush Red4 = (Brush)new BrushConverter().ConvertFrom("#B71C1C"); // 900

        private List<INode> doneNodes = new List<INode>();

        public ResultWindow(Circuit circuit)
        {
            InitializeComponent();

            Canvas.Width = 10;
            Canvas.Height = 10;

            // get max depth
            foreach (OutputNode outputNode in circuit.OutputNodes.Values)
            {
                RenderNodeD(outputNode, 1);
            }

            // draw buttons
            List<string> doneNames = new List<string>();
            for (int i = 0; i < maxDepth; i++)
            {
                List<string> tmpDoneNames = new List<string>();

                foreach (INode node in circuit.Nodes.Values)
                {
                    if (doneNames.Contains(node.Name)) continue;

                    if (node is CircuitNode)
                    {
                        bool allInputsDisplayed = true;
                        foreach (INode input in ((CircuitNode)node).Inputs)
                        {
                            if (!doneNames.Contains(input.Name))
                            {
                                allInputsDisplayed = false;
                            }
                        }
                        if (!allInputsDisplayed) continue;
                    }
                    else if (node is OutputNode)
                    {
                        continue;
                    }

                    DisplayNode(node, i+1 );
                    tmpDoneNames.Add(node.Name);
                    doneNodes.Add(node);
                }

                doneNames.AddRange(tmpDoneNames);
            }

            foreach (OutputNode node in circuit.OutputNodes.Values)
            {
                DisplayNode(node, maxDepth+1);
                doneNodes.Add(node);
                doneNames.Add(node.Name);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // draw lines
            foreach (INode doneNode in doneNodes)
            {
                Button outputbtn = (Button)LogicalTreeHelper.FindLogicalNode(Canvas, doneNode.Name);

                if (doneNode is CircuitNode)
                {
                    foreach (INode input in ((CircuitNode)doneNode).Inputs)
                    {
                        Button inputbtn = (Button)LogicalTreeHelper.FindLogicalNode(Canvas, input.Name);
                        Brush brush = input.Process() ? Green3 : Red3;

                        Point btn1Point = inputbtn.TransformToAncestor(Canvas).Transform(new Point(0, 0));
                        Point btn2Point = outputbtn.TransformToAncestor(Canvas).Transform(new Point(0, 0));
                        Line l = new Line();
                        l.Stroke = brush;
                        l.StrokeThickness = 2.0;
                        l.X1 = btn1Point.X + inputbtn.ActualWidth;
                        l.X2 = btn2Point.X;
                        l.Y1 = btn1Point.Y + inputbtn.ActualHeight / 2;
                        l.Y2 = btn2Point.Y + outputbtn.ActualHeight / 2;
                        Canvas.Children.Add(l);
                    }
                }
                else if (doneNode is OutputNode)
                {
                    Button inputbtn = (Button)LogicalTreeHelper.FindLogicalNode(Canvas, ((OutputNode)doneNode).Input.Name);
                    Brush brush = ((OutputNode)doneNode).Input.Process() ? Green3 : Red3;

                    Point btn1Point = inputbtn.TransformToAncestor(Canvas).Transform(new Point(0, 0));
                    Point btn2Point = outputbtn.TransformToAncestor(Canvas).Transform(new Point(0, 0));
                    Line l = new Line();
                    l.Stroke = brush;
                    l.StrokeThickness = 2.0;
                    l.X1 = btn1Point.X + inputbtn.ActualWidth;
                    l.X2 = btn2Point.X;
                    l.Y1 = btn1Point.Y + inputbtn.ActualHeight / 2;
                    l.Y2 = btn2Point.Y + outputbtn.ActualHeight / 2;
                    Canvas.Children.Add(l);
                }
            }
        }

        private int maxDepth = 0;
        private void RenderNodeD(INode node, int depth)
        {
            maxDepth = depth > maxDepth ? depth-1 : maxDepth;

            if (node is OutputNode)
            {
                RenderNodeD(((OutputNode)node).Input, depth + 1);
            }
            else if (node is CircuitNode)
            {
                foreach (INode input in ((CircuitNode)node).Inputs)
                {
                    RenderNodeD(input, depth + 1);
                }
            }
        }

        private int[] depthCounter = new int[99];
        private void DisplayNode( INode node, int depth)
        {
            depth--;

            Button btn = new Button
            {
                Width = 100,
                Height = 20,
                Margin = new Thickness(50 + (200 * depth), 50 + (100 * depthCounter[depth]) + (depth % 2 == 0 ? 0 : 50), 5, 5),
                Content = node is CircuitNode ? ((CircuitNode)node).Type : node.Name,
                Name = node.Name,
                Background = node.Process() ? node is CircuitNode ? Green1 : Green2 : node is CircuitNode ? Red1 : Red2
            };

            Canvas.Children.Add(btn);
            Canvas.SetZIndex(btn, 3);

            if (Canvas.Height < 100 + (100 * depthCounter[depth]) + (depth % 2 == 0 ? 0 : 50)) Canvas.Height = 100 + (100 * depthCounter[depth]) + (depth % 2 == 0 ? 0 : 50);
            if (Canvas.Width < 100 + (200 * depth)) Canvas.Width = 100 + (200 * depth);

            depthCounter[depth]++;
        }
    }
}
