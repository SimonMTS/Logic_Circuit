using Logic_Circuit.Models;
using Logic_Circuit.Models.BaseNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Logic_Circuit.Controllers
{
    public class ResultController
    {
        #region draw

        private readonly List<INode> doneNodes = new List<INode>();

        private Circuit Circuit;

        public void DrawButtons(Circuit circuit, IResultWin window)
        {
            this.Circuit = circuit;

            List<string> doneNames = new List<string>();
            for (int i = 0; i < GetMaxDepth(circuit); i++)
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

                    window.DisplayNode(node, i + 1);
                    tmpDoneNames.Add(node.Name);
                    doneNodes.Add(node);
                }

                doneNames.AddRange(tmpDoneNames);
            }


            foreach (OutputNode node in circuit.OutputNodes.Values)
            {
                window.DisplayNode(node, maxDepth + 1);
                doneNodes.Add(node);
                doneNames.Add(node.Name);
            }
        }

        private bool firstDraw = false;

        public void DrawLines(IResultWin window)
        {
            foreach (INode doneNode in doneNodes)
            {
                if (doneNode is CircuitNode)
                {
                    foreach (INode input in ((CircuitNode)doneNode).Inputs)
                    {
                        INode start = input;
                        INode end = doneNode;

                        window.DrawLine(start, end, GetColor(start.Process()));
                    }
                }
                else if (doneNode is OutputNode)
                {
                    INode start = ((OutputNode)doneNode).Input;
                    INode end = doneNode;

                    window.DrawLine(start, end, GetColor(start.Process()));
                }
            }

            firstDraw = true;
        }

        #endregion draw

        #region depth

        private int maxDepth = 0;
        private int GetMaxDepth(Circuit circuit)
        {
            foreach (OutputNode outputNode in circuit.OutputNodes.Values)
            {
                RenderNodeD(outputNode, 1);
            }

            return maxDepth;
        }

        private void RenderNodeD(INode node, int depth)
            {
                maxDepth = depth > maxDepth ? depth - 1 : maxDepth;

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

        #endregion depth

        #region color

        private Brush Green1 = (Brush)new BrushConverter().ConvertFrom("#4CAF50"); // 500
        private Brush Green2 = (Brush)new BrushConverter().ConvertFrom("#388E3C"); // 700
        private Brush Green3 = (Brush)new BrushConverter().ConvertFrom("#81C784"); // 300
        private Brush Green4 = (Brush)new BrushConverter().ConvertFrom("#1B5E20"); // 900

        private Brush Red1 = (Brush)new BrushConverter().ConvertFrom("#F44336"); // 500
        private Brush Red2 = (Brush)new BrushConverter().ConvertFrom("#D32F2F"); // 700
        private Brush Red3 = (Brush)new BrushConverter().ConvertFrom("#E57373"); // 300
        private Brush Red4 = (Brush)new BrushConverter().ConvertFrom("#B71C1C"); // 900

        private Brush HighLight1 = (Brush)new BrushConverter().ConvertFrom("#607D8B"); // 500

        public Brush GetColor(bool value) {
            return GetColor(value, false);
        }

        public Brush GetColor(Brush brush, bool highlighted) {
            bool value = brush == Green1 || brush == Green3;

            return GetColor(value, highlighted);
        }

        public Brush GetColor(bool value, bool highlighted)
        {
            if (highlighted)
            {
                return value ? Green1 : Red1;
            }
            else
            {
                return value ? Green3 : Red3;
            }
        }

        #endregion color

        #region interaction

        public void OnButtonClick(string nodeName, IResultWin window)
        {
            INode node = doneNodes.Find(n => n.Name == nodeName);

            if (node is InputNode)
            {
                Circuit.InputNodes[nodeName].Value = !Circuit.InputNodes[nodeName].Value;

                /*window.ClearCanvas();
                DrawButtons(this.Circuit, window);

                */
                window.UpdateCanvas(Circuit);
                /*DrawLines(window);*/


            }
        }

        #endregion interaction
    }
}

public interface IResultWin
{
    void DisplayNode(INode node, int depth);

    void DrawLine(INode startNode, INode endNode, Brush brush);

    void ClearCanvas();

    void UpdateCanvas(Circuit circuit);
}