using Logic_Circuit.Models;
using Logic_Circuit.Models.BaseNodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic_Circuit.Parser
{
    public class Parse
    {
        public static Circuit Try( string content )
        {
            Circuit circuit = new Circuit();

            using (StringReader reader = new StringReader(content))
            {
                bool blockOne = true;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if ( line.StartsWith("#") ) { continue; }

                    if ( line.Equals("") ) { blockOne = false; continue; }

                    line = Regex.Replace(line, @"\s+", "").Replace(";", "");
                    string[] parsedLine = line.Split(':');

                    if ( blockOne )
                    {
                        AddNode( parsedLine, circuit );
                    }
                    else
                    {
                        AddConnection( parsedLine, circuit );
                    }
                }
            }

            Console.WriteLine(circuit.Nodes.Count);

            foreach (var node in circuit.Nodes)
            {
                Console.Write( node.Value.Name + "(" + node.Value.Process() + ") - " );
                if (node.Value is OutputNode) Console.Write( ((OutputNode)node.Value).Input.Name );
                if (node.Value is CircuitNode)
                {
                    foreach (var n in ((CircuitNode)node.Value).Inputs)
                    {
                        Console.Write(n.Name + ", ");
                    }
                }
                Console.WriteLine("");
            }

            return circuit;
        }

        private static void AddNode( string[] parsedLine, Circuit circuit )
        {
            if ( !parsedLine[1].Contains("PROBE") && !parsedLine[1].Contains("INPUT"))
            {
                CircuitNode node = new CircuitNode()
                {
                    Name = parsedLine[0],
                    Type = parsedLine[1]
                };

                circuit.AddNode( node );
            }
            else
            {
                if ( parsedLine[1].StartsWith("INPUT") )
                {
                    Console.WriteLine(parsedLine);
                    InputNode node = new InputNode()
                    {
                        Name = parsedLine[0],
                        Value = parsedLine[1].Contains("HIGH")
                    };

                    circuit.AddNode(node);
                }
                else
                {
                    OutputNode node = new OutputNode()
                    {
                        Name = parsedLine[0]
                    };

                    circuit.AddNode(node);
                }
            }
        }

        private static void AddConnection( string[] parsedLine, Circuit circuit )
        {
            string[] outputs = parsedLine[1].Split(',');

            foreach (string output in outputs)
            {
                INode node = circuit.Nodes[output];

                if ( node is CircuitNode )
                {
                    ((CircuitNode)node).Inputs.Add(circuit.Nodes[parsedLine[0]]);
                }
                else
                {
                    ((OutputNode)node).Input = circuit.Nodes[parsedLine[0]];
                }

                
            }

        }

    }
}
