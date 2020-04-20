using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.BaseNodes
{
    public class InputNode : INode
    {
        public string Name { get; set; }

        public bool Value { get; set; }
        public bool DefaultValue { get; set; }

        public bool Process()
        {
            return Value;
        }

        public void Reset()
        {
            Value = DefaultValue;
        }
    }
}
