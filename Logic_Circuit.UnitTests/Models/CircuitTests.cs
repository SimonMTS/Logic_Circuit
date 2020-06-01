using System;
using Logic_Circuit.Models.Circuits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logic_Circuit.UnitTests.Models
{
    [TestClass]
    public class CircuitTests
    {
        [TestMethod]
        public void FullAdder_000()
        {
            Circuit fullAdder = TestHelper.GetFullAdderCircuit();

            fullAdder.InputNodes["A"].Value = false;
            fullAdder.InputNodes["B"].Value = false;
            fullAdder.InputNodes["Cin"].Value = false;

            Assert.AreEqual(false, fullAdder.OutputNodes["S"].Process()[0]);
            Assert.AreEqual(false, fullAdder.OutputNodes["Cout"].Process()[0]);
        }

        [TestMethod]
        public void FullAdder_001()
        {
            Circuit fullAdder = TestHelper.GetFullAdderCircuit();

            fullAdder.InputNodes["A"].Value = false;
            fullAdder.InputNodes["B"].Value = false;
            fullAdder.InputNodes["Cin"].Value = true;

            Assert.AreEqual(true, fullAdder.OutputNodes["S"].Process()[0]);
            Assert.AreEqual(false, fullAdder.OutputNodes["Cout"].Process()[0]);
        }

        [TestMethod]
        public void FullAdder_010()
        {
            Circuit fullAdder = TestHelper.GetFullAdderCircuit();

            fullAdder.InputNodes["A"].Value = false;
            fullAdder.InputNodes["B"].Value = true;
            fullAdder.InputNodes["Cin"].Value = false;

            Assert.AreEqual(true, fullAdder.OutputNodes["S"].Process()[0]);
            Assert.AreEqual(false, fullAdder.OutputNodes["Cout"].Process()[0]);
        }

        [TestMethod]
        public void FullAdder_011()
        {
            Circuit fullAdder = TestHelper.GetFullAdderCircuit();

            fullAdder.InputNodes["A"].Value = false;
            fullAdder.InputNodes["B"].Value = true;
            fullAdder.InputNodes["Cin"].Value = true;

            Assert.AreEqual(false, fullAdder.OutputNodes["S"].Process()[0]);
            Assert.AreEqual(true, fullAdder.OutputNodes["Cout"].Process()[0]);
        }

        [TestMethod]
        public void FullAdder_100()
        {
            Circuit fullAdder = TestHelper.GetFullAdderCircuit();

            fullAdder.InputNodes["A"].Value = true;
            fullAdder.InputNodes["B"].Value = false;
            fullAdder.InputNodes["Cin"].Value = false;

            Assert.AreEqual(true, fullAdder.OutputNodes["S"].Process()[0]);
            Assert.AreEqual(false, fullAdder.OutputNodes["Cout"].Process()[0]);
        }

        [TestMethod]
        public void FullAdder_101()
        {
            Circuit fullAdder = TestHelper.GetFullAdderCircuit();

            fullAdder.InputNodes["A"].Value = true;
            fullAdder.InputNodes["B"].Value = false;
            fullAdder.InputNodes["Cin"].Value = true;

            Assert.AreEqual(false, fullAdder.OutputNodes["S"].Process()[0]);
            Assert.AreEqual(true, fullAdder.OutputNodes["Cout"].Process()[0]);
        }

        [TestMethod]
        public void FullAdder_110()
        {
            Circuit fullAdder = TestHelper.GetFullAdderCircuit();

            fullAdder.InputNodes["A"].Value = true;
            fullAdder.InputNodes["B"].Value = true;
            fullAdder.InputNodes["Cin"].Value = false;

            Assert.AreEqual(false, fullAdder.OutputNodes["S"].Process()[0]);
            Assert.AreEqual(true, fullAdder.OutputNodes["Cout"].Process()[0]);
        }

        [TestMethod]
        public void FullAdder_111()
        {
            Circuit fullAdder = TestHelper.GetFullAdderCircuit();

            fullAdder.InputNodes["A"].Value = true;
            fullAdder.InputNodes["B"].Value = true;
            fullAdder.InputNodes["Cin"].Value = true;

            Assert.AreEqual(true, fullAdder.OutputNodes["S"].Process()[0]);
            Assert.AreEqual(true, fullAdder.OutputNodes["Cout"].Process()[0]);
        }
    }
}
