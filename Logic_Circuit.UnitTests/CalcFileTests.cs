using System;
using System.IO;
using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Circuits;
using Logic_Circuit.Models.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Logic_Circuit.Parser.Validation;
using Logic_Circuit.Models;
using Logic_Circuit.UnitTests.Models;

namespace Logic_Circuit.UnitTests
{
    [TestClass]
    public class CalcFileTests
    {
        [TestMethod]
        public void AllPossibleInputs()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    bool[] x = { false, false, false, false, false, false, false, false };
                    bool[] y = { false, false, false, false, false, false, false, false };
                    bool[] s = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };

                    x[i] = true;
                    y[j] = true;
                    s[i + j] = true;

                    Cache.IncUserActionCounter();
                    CircuitNode node = GetCalcCircuit();

                    node.Inputs.Add(new FakeNode(x[0])); // X0
                    node.Inputs.Add(new FakeNode(x[1])); // X1
                    node.Inputs.Add(new FakeNode(x[2])); // X2
                    node.Inputs.Add(new FakeNode(x[3])); // X3
                    node.Inputs.Add(new FakeNode(x[4])); // X4
                    node.Inputs.Add(new FakeNode(x[5])); // X5
                    node.Inputs.Add(new FakeNode(x[6])); // X6
                    node.Inputs.Add(new FakeNode(x[7])); // X7

                    node.Inputs.Add(new FakeNode(y[0])); // Y0
                    node.Inputs.Add(new FakeNode(y[1])); // Y1
                    node.Inputs.Add(new FakeNode(y[2])); // Y2
                    node.Inputs.Add(new FakeNode(y[3])); // Y3
                    node.Inputs.Add(new FakeNode(y[4])); // Y4
                    node.Inputs.Add(new FakeNode(y[5])); // Y5
                    node.Inputs.Add(new FakeNode(y[6])); // Y6
                    node.Inputs.Add(new FakeNode(y[7])); // Y7

                    Assert.AreEqual(s[0], node.Process()[0]);   // S0
                    Assert.AreEqual(s[1], node.Process()[1]);   // S1
                    Assert.AreEqual(s[2], node.Process()[2]);   // S2
                    Assert.AreEqual(s[3], node.Process()[3]);   // S3
                    Assert.AreEqual(s[4], node.Process()[4]);   // S4
                    Assert.AreEqual(s[5], node.Process()[5]);   // S5
                    Assert.AreEqual(s[6], node.Process()[6]);   // S6
                    Assert.AreEqual(s[7], node.Process()[7]);   // S7
                    Assert.AreEqual(s[8], node.Process()[8]);   // S8
                    Assert.AreEqual(s[9], node.Process()[9]);   // S9
                    Assert.AreEqual(s[10], node.Process()[10]); // S10
                    Assert.AreEqual(s[11], node.Process()[11]); // S11
                    Assert.AreEqual(s[12], node.Process()[12]); // S12
                    Assert.AreEqual(s[13], node.Process()[13]); // S13
                    Assert.AreEqual(s[14], node.Process()[14]); // S14
                }
            }
        }


        private CircuitNode GetCalcCircuit()
        {
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Validator.InternalCircuitNamesForTests = new string[] {
                "INPUT_HIGH", "INPUT_LOW", "PROBE", "NAND", "OR", "AND", "NOT", "CALC", "DECODER", "ENCODER", "DECODER_ENABLABLE", "FULLADDER"
            };

            CircuitNodeFactory.DifferentPathForTests = filePath + "../../../../Internal_Circuits/";

            INodeFactory factory = new CircuitNodeFactory();
            return (CircuitNode)factory.GetNode("testName", "CALC");
        }
    }
}
