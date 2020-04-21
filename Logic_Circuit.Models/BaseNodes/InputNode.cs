﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Logic_Circuit.Models.BaseNodes
{
    public class InputNode : INode
    {
        public string Name { get; set; }
        public int RealDepth { get => 0; set { } }

        public bool Value { get; set; }
        public bool DefaultValue { get; set; }

        public bool[] Process()
        {
            return new bool[] { Value };
        }

        public Brush GetDisplayableValue(Brush ifTrue, Brush ifFalse, Brush ifMixed)
        {
            return Value ? ifTrue : ifFalse;
        }

        public void Reset()
        {
            Value = DefaultValue;
        }
    }
}
