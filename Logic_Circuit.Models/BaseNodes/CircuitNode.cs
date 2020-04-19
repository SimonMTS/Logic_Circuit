using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.BaseNodes
{
    public class CircuitNode : INode
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public List<INode> Inputs { get; set; } = new List<INode>();

        public bool Process()
        {
            if ( Type.Equals("NAND") )
            {
                return !(Inputs[0].Process() && Inputs[1].Process());
            }

            if (Type.Equals("NOT"))
            {
                return !Inputs[0].Process();
            }

            if (Type.Equals("OR"))
            {
                foreach (INode input in Inputs)
                {
                    if (input.Process())
                    {
                        return true;
                    }
                }

                return false;
            }

            if (Type.Equals("AND"))
            {
                foreach (INode input in Inputs)
                {
                    if (!input.Process())
                    {
                        return false;
                    }
                }

                return true;
            }


            /*throw new NotImplementedException();*/
            return false;
        }
    }
}
