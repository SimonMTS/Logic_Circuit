﻿using Logic_Circuit.Models.Circuits;
using Logic_Circuit.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.Factories
{
    public class CircuitFactory
    {
        private static Dictionary<string, Circuit> circuits = new Dictionary<string, Circuit>();

        public static Circuit GetCircuit(string name)
        {
            return circuits.ContainsKey(name) ? circuits[name] : null;
        }

        public static Circuit GetFromFile(string filePath)
        {
            CircuitParser parser = CircuitParser.GetParser();

            var result = parser.AddFile(filePath);

            if (result.success)
            {
                string fileName = result.fileName;

                if (circuits.ContainsKey(fileName))
                {
                    return circuits[fileName];
                }

                CircuitBuilder circuitBuilder = new CircuitBuilder();
                circuitBuilder.SetName(fileName);

                foreach (var nodeString in parser.GetNodeString(fileName))
                {
                    circuitBuilder.AddNode(nodeString.name, nodeString.type);
                }

                foreach (var connectionString in parser.GetConnectionString(fileName))
                {
                    foreach (string outputNode in connectionString.outputs)
                    {
                        circuitBuilder.AddConnection(connectionString.input, outputNode);
                    }
                }

                circuits[fileName] = circuitBuilder.GetCircuit();
                return circuits[fileName];
            }
            else
            {
                return null;
            }
        }
    }
}
