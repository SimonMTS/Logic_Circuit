using System;
using Logic_Circuit.Models;
using Logic_Circuit.Models.Circuits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logic_Circuit.UnitTests.Models
{
    [TestClass]
    public class BuilderTests
    {
        [TestMethod]
        public void General_Positive()
        {
            TestHelper.SetTestPaths();

            CircuitBuilder circuitBuilder = new CircuitBuilder();
            circuitBuilder.SetName("XOR");

            circuitBuilder.AddNode("A", "INPUT_HIGH");
            circuitBuilder.AddNode("B", "INPUT_HIGH");
            circuitBuilder.AddNode("C", "PROBE");
            circuitBuilder.AddNode("NODE1", "NAND");
            circuitBuilder.AddNode("NODE2", "NAND");
            circuitBuilder.AddNode("NODE3", "NAND");
            circuitBuilder.AddNode("NODE4", "NAND");
            circuitBuilder.AddNode("NODE5", "NAND");
            circuitBuilder.AddNode("NODE6", "NAND");

            circuitBuilder.AddConnection("A", "NODE1");
            circuitBuilder.AddConnection("A", "NODE4");

            circuitBuilder.AddConnection("B", "NODE2");
            circuitBuilder.AddConnection("B", "NODE4");

            circuitBuilder.AddConnection("NODE1", "NODE3");
            circuitBuilder.AddConnection("NODE2", "NODE3");
            circuitBuilder.AddConnection("NODE3", "NODE5");
            circuitBuilder.AddConnection("NODE4", "NODE5");
            circuitBuilder.AddConnection("NODE5", "NODE6");
            circuitBuilder.AddConnection("NODE6", "C");
                
            Circuit xor = circuitBuilder.GetCircuit();

            Cache.IncUserActionCounter();
            xor.InputNodes["A"].Value = false;
            xor.InputNodes["B"].Value = false;
            Assert.AreEqual(false, xor.OutputNodes["C"].Process()[0]);

            Cache.IncUserActionCounter();
            xor.InputNodes["A"].Value = true;
            xor.InputNodes["B"].Value = false;
            Assert.AreEqual(true, xor.OutputNodes["C"].Process()[0]);

            Cache.IncUserActionCounter();
            xor.InputNodes["A"].Value = true;
            xor.InputNodes["B"].Value = true;
            Assert.AreEqual(false, xor.OutputNodes["C"].Process()[0]);
        }

        [TestMethod]
        public void ConnectNonExistentNodes_Negative()
        {
            TestHelper.SetTestPaths();

            CircuitBuilder circuitBuilder = new CircuitBuilder();

            Assert.ThrowsException<System.InvalidOperationException>(() => circuitBuilder.AddConnection("NODE1", "NODE2"));
        }

        [TestMethod]
        public void FinishWithoutName_Negative()
        {
            TestHelper.SetTestPaths();

            CircuitBuilder circuitBuilder = new CircuitBuilder();

            Assert.ThrowsException<System.InvalidOperationException>(() => circuitBuilder.GetCircuit());
        }
    }
}
