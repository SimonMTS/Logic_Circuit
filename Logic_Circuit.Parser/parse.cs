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
        private static List<string> NodeNames;

        public static (bool, Circuit, string) Try( string content )
        {
            Circuit circuit = new Circuit();
            NodeNames = new List<string>();

            if (!IsValidFormat(content))
            {
                return (false, null, "Missing Enter between description blocks.");
            }

            using (StringReader reader = new StringReader(content))
            {
                bool blockOne = true;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if ( line.StartsWith("#") ) { continue; }
                    if ( line.Equals("") ) { blockOne = false; continue; }
                    var isValidLine = IsValidLine(line);
                    if (!isValidLine.Item1) { return isValidLine; }

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

            var isValidCircuit = IsValidCircuit(circuit);
            if (!isValidCircuit.Item1) { return isValidCircuit; }

            return (true, circuit, "");
        }

        #region helper

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

        #endregion helper

        #region validation

        private static bool IsValidFormat(string content)
        {
            using (StringReader reader = new StringReader(content))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Equals("")) { return true; }
                }
                return false;
            }
        }

        private static (bool, Circuit, string) IsValidLine(string line)
        {
            // valid line syntax
            var regex = new Regex(@"(^#.*$)|((^\w+):(\s*)(\w+,)*(\w+;$))");
            if (!regex.Match(line).Success)
            {
                return (false, null, "Line doesn't conform to syntax: '" + line + "'"); ;
            }

            // valid node type
            string type = line.Split(':')[1].Trim().Replace(";", "");
            string[] types = { "INPUT_HIGH", "INPUT_LOW", "PROBE", "NAND", "AND", "OR", "NOT" }; // todo get from files
            if (
                !types.Contains(type) && 
                !NodeNames.Contains(type.Split(',')[0])
            )
            {
                return (false, null, "'" + type + "' is not a valid node type."); ;
            }
            
            // add name to NodeNames, if line is node declaration
            if (types.Contains(type))
            {
                NodeNames.Add(line.Split(':')[0]);
            }

            return (true, null, "");
        }

        private static (bool, Circuit, string) IsValidCircuit(Circuit circuit)
        {
            foreach (INode node in circuit.Nodes.Values)
            {
                if (node is OutputNode || node is InputNode) continue;

                bool nodeHasOutput = false;
                foreach (INode nodeAsOutput in circuit.Nodes.Values)
                {
                    if (nodeAsOutput is CircuitNode && ((CircuitNode)nodeAsOutput).Inputs.Contains(node))
                    {
                        nodeHasOutput = true;
                        break;
                    }

                    if (nodeAsOutput is OutputNode && ((OutputNode)nodeAsOutput).Input == node)
                    {
                        nodeHasOutput = true;
                        break;
                    }
                }

                if (!nodeHasOutput)
                {
                    return (false, null, node.Name + " has no outputs.");
                }


                if (!RecurseToInput(node, node, circuit.Nodes.Count, 0))
                {
                    return (false, null, node.Name + " has itself as input."); ;
                }
            }

            return (true, null, "");
        }

        private static bool RecurseToInput(INode constantNode, INode tmpNode, int maxDepth, int depth)
        {
            if (tmpNode is InputNode) return true;

            if (depth > maxDepth) return false;
            if (depth > 0 && tmpNode == constantNode) return false;

            if (tmpNode is CircuitNode)
            {
                foreach (INode input in ((CircuitNode)tmpNode).Inputs)
                {
                    if (!RecurseToInput(constantNode, input, maxDepth, depth + 1))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return RecurseToInput(constantNode, ((OutputNode)tmpNode).Input, maxDepth, depth + 1);
            }
        }

        #endregion validation
    }
}
