﻿using Logic_Circuit.Models.Circuits;
using Logic_Circuit.Parser;
using System.Collections.Generic;

namespace Logic_Circuit.Models.Factories
{
    /// <summary>
    /// Retrieves a Circuit from a file, or from internal cache.
    /// </summary>
    public class CircuitFactory
    {
        private static readonly Dictionary<string, Circuit> circuits = new Dictionary<string, Circuit>();

        public static Circuit GetCircuit(string name)
        {
            return circuits.ContainsKey(name) ? circuits[name].Clone() : null;
        }

        public static (bool success, Circuit circuit, string error) GetFromFile(string filePath)
        {
            CircuitParser parser = CircuitParser.GetParser();

            (bool success, string fileName, string error) result = parser.AddFile(filePath);

            if (result.success)
            {
                string fileName = result.fileName;

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
                return (true, circuits[fileName], "");
            }
            else
            {
                return (false, null, result.error);
            }
        }
    }
}
