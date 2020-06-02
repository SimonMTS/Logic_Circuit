using Logic_Circuit.Models.Circuits;
using Logic_Circuit.Models.Factories;
using Logic_Circuit.Models.Nodes.NodeInputTypes;
using Logic_Circuit.Models.Strategies;
using Logic_Circuit.Models.Strategies.NodeProcessStrategies;
using System.Collections.Generic;
using System.Windows.Media;

namespace Logic_Circuit.Models.BaseNodes
{
    /// <summary>
    /// A node that contains a Circuit.
    /// </summary>
    public class CircuitNode : IMultipleInputs
    {
        public string Name { get; set; }
        public string UID { get; set; }
        public int RealDepth { get => CalcRealDepth(); set { } }
        public string Type { get; set; }
        public Circuit Circuit { get; set; }
        public List<INode> Inputs { get; set; } = new List<INode>();

        private readonly Dictionary<string, int> RetrievedInputs = new Dictionary<string, int>();

        public CircuitNode(string name, string type, Circuit circuit)
        {
            Name = name;
            Type = type;
            Circuit = circuit;
            UID = "name(" + name + ")_UID(" + Cache.GetUniqueInt() + ")";
        }

        private int CalcRealDepth()
        {
            int result;
            if ((result = Cache.GetDepth(UID)) != int.MinValue)
            {
                return result;
            }

            int highestInternal = 0;
            if (Circuit != null)
            {
                foreach (INode input in Circuit.OutputNodes.Values)
                {
                    if (input.RealDepth > highestInternal) highestInternal = input.RealDepth;
                }
            }

            int highest = 0;
            foreach (INode input in Inputs)
            {
                if (input.RealDepth > highest) highest = input.RealDepth;
            }

            if (Circuit == null) highest++;

            Cache.PushDepth(UID, highestInternal + highest);
            return highestInternal + highest;
        }

        public int RetrievedInputsIncr(string name)
        {
            if (Circuit == null || Circuit.OutputNodes.Count == 1) return 0;

            if (!RetrievedInputs.ContainsKey(name))
            {
                RetrievedInputs.Add(name, RetrievedInputs.Count);
            }

            return RetrievedInputs[name];
        }

        public bool[] Process()
        {
            bool[] result;
            if ((result = Cache.Get(UID + getInputState())) != null)
            {
                return result;
            }

            NodeProcessContext context;
            if (Inputs.Count == 1 && Circuit.OutputNodes.Count == 1)
            {
                context = new NodeProcessContext(new OneToOneInputStrategy());
            }
            else if (Circuit.OutputNodes.Count == 1)
            {
                context = new NodeProcessContext(new NToOneInputStrategy());
            }
            else
            {
                context = new NodeProcessContext(new NToNInputStrategy());
            }

            result = context.ProcessInput(this);
            Cache.Push(UID + getInputState(), result);
            return result;
        }

        public Brush GetDisplayableValue(Brush ifTrue, Brush ifFalse, Brush ifMixed)
        {
            bool[] res = Process();

            if (res.Length == 1)
            {
                return res[0] ? ifTrue : ifFalse;
            }
            else
            {
                bool allTheSame = true;

                for (int i = 1; i < res.Length; i++)
                {
                    if (res[i] != res[i - 1])
                    {
                        allTheSame = false;
                        break;
                    }
                }

                if (allTheSame)
                {
                    return res[0] ? ifTrue : ifFalse;
                }
                else
                {
                    return ifMixed;
                }
            }
        }

        private string getInputState()
        {
            string state = "_";

            foreach (INode input in Inputs)
            {
                state += input.Process();
            }

            return state;
        }

        public INode Clone()
        {
            return new CircuitNode(this.Name, this.Type, CircuitFactory.GetCircuit(Circuit.Name));
        }
    }
}
