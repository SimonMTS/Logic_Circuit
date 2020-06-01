using System;
using System.IO;
using System.Reflection;
using Logic_Circuit.Parser;
using Logic_Circuit.Parser.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logic_Circuit.UnitTests.Parser
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void General_Positive()
        {
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            filePath += "../../../../Circuits/Circuit1_FullAdder.txt";
            Validator.InternalCircuitNamesForTests = new string[] { 
                "INPUT_HIGH", "INPUT_LOW", "PROBE", "NAND", "OR", "AND", "NOT"
            };

            CircuitParser parser = CircuitParser.GetParser();

            (bool success, string fileName, string error) result = parser.AddFile(filePath);

            Assert.AreEqual(true, result.success);

            int nodeCount = 0;
            foreach (var nodeString in parser.GetNodeString(result.fileName))
            {
                nodeCount++;
            }

            Assert.AreEqual(16, nodeCount);

            int connectionCount = 0;
            foreach (var connectionString in parser.GetConnectionString(result.fileName))
            {
                connectionCount++;
            }

            Assert.AreEqual(14, connectionCount);
        }

        [TestMethod]
        public void InfiniteLoop_Negative()
        {
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            filePath += "../../../../Circuits/Circuit4_InfiniteLoop.txt";
            Validator.InternalCircuitNamesForTests = new string[] {
                "INPUT_HIGH", "INPUT_LOW", "PROBE", "NAND", "OR", "AND", "NOT"
            };

            CircuitParser parser = CircuitParser.GetParser();

            (bool success, string fileName, string error) result = parser.AddFile(filePath);

            Assert.AreEqual(false, result.success);
            Assert.AreEqual("Node: 'S' leads to an infinite loop.", result.error);
        }

        [TestMethod]
        public void NotConnected_Negative()
        {
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            filePath += "../../../../Circuits/Circuit5_NotConnected.txt";
            Validator.InternalCircuitNamesForTests = new string[] {
                "INPUT_HIGH", "INPUT_LOW", "PROBE", "NAND", "OR", "AND", "NOT"
            };

            CircuitParser parser = CircuitParser.GetParser();

            (bool success, string fileName, string error) result = parser.AddFile(filePath);

            Assert.AreEqual(false, result.success);
            Assert.AreEqual("NODE2 has no outputs.", result.error);
        }
    }
}
