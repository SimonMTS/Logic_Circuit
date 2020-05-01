using Logic_Circuit.Models;
using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Nodes;
using Logic_Circuit.Models.Nodes.NodeInputTypes;
using Logic_Circuit.Models.Circuits;
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

                    if (node is IMultipleInputs)
                    {
                        bool allInputsDisplayed = true;
                        foreach (INode input in ((IMultipleInputs)node).Inputs)
                        {
                            if (!doneNames.Contains(input.Name))
                            {
                                allInputsDisplayed = false;
                            }
                        }
                        if (!allInputsDisplayed) continue;
                    }
                    else if (node is ISingleInput)
                    {
                        continue;
                    }

                    node.RealDepth = i;
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

        public void DrawLines(IResultWin window)
        {
            foreach (INode doneNode in doneNodes)
            {
                if (doneNode is IMultipleInputs)
                {
                    foreach (INode input in ((IMultipleInputs)doneNode).Inputs)
                    {
                        INode start = input;
                        INode end = doneNode;

                        window.DrawLine(start, end, GetColor(start));
                    }
                }
                else if (doneNode is ISingleInput)
                {
                    INode start = ((ISingleInput)doneNode).Input;
                    INode end = doneNode;

                    window.DrawLine(start, end, GetColor(start));
                }
            }
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

            if (node is ISingleInput)
            {
                RenderNodeD(((ISingleInput)node).Input, depth + 1);
            }
            else if (node is IMultipleInputs)
            {
                foreach (INode input in ((IMultipleInputs)node).Inputs)
                {
                    RenderNodeD(input, depth + 1);
                }
            }
        }

        #endregion depth

        #region color

        private readonly Brush Green1   = (Brush)new BrushConverter().ConvertFrom("#4CAF50"); // 500
        private readonly Brush Green2   = (Brush)new BrushConverter().ConvertFrom("#81C784"); // 300

        private readonly Brush Red1     = (Brush)new BrushConverter().ConvertFrom("#F44336"); // 500
        private readonly Brush Red2     = (Brush)new BrushConverter().ConvertFrom("#E57373"); // 300

        private readonly Brush Orange1  = (Brush)new BrushConverter().ConvertFrom("#FF9800"); // 500
        private readonly Brush Orange2  = (Brush)new BrushConverter().ConvertFrom("#FFB74D"); // 300

        public Brush GetColor(Brush brush, bool highlighted)
        {
            if (brush == Green1 || brush == Green2)
            {
                return highlighted ? Green1 : Green2;
            }
            else if (brush == Red1 || brush == Red2)
            {
                return highlighted ? Red1 : Red2;
            }
            else
            {
                return highlighted ? Orange1 : Orange2;
            }
        }

        public Brush GetColor(INode node)
        {
            return node.GetDisplayableValue(Green2, Red2, Orange2);
        }

        #endregion color

        #region interaction

        public void OnButtonClick(string nodeName, IResultWin window)
        {
            INode node = doneNodes.Find(n => n.Name == nodeName);

            if (node is InputNode)
            {
                Circuit.InputNodes[nodeName].Value = !Circuit.InputNodes[nodeName].Value;

                window.UpdateCanvas(Circuit);
            }

            if (node is CircuitNode)
            {
                window.SpawnNew(((CircuitNode)node).Circuit, ((CircuitNode)node).Type);
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

    void SpawnNew(Circuit circuit, string title);
}