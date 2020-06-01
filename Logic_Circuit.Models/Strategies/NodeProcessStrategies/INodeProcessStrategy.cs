using Logic_Circuit.Models.BaseNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.Strategies.NodeProcessStrategies
{
    public interface INodeProcessStrategy
    {
        bool[] ProcessInput(CircuitNode node);
    }
}
