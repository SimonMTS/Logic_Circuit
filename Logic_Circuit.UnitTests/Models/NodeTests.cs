using System;
using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Factories;
using Logic_Circuit.Models.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logic_Circuit.UnitTests.Models
{
    [TestClass]
    public class NodeTests
    {
        #region nandNode

        [TestMethod]
        public void NandNode_00_Positive()
        {
            INode f1 = new FakeNode(false);
            INode f2 = new FakeNode(false);

            NandNode node = new NandNode("testName");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(true, node.Process()[0]);
        }

        [TestMethod]
        public void NandNode_01_Positive()
        {
            INode f1 = new FakeNode(false);
            INode f2 = new FakeNode(true);

            NandNode node = new NandNode("testName");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(true, node.Process()[0]);
        }

        [TestMethod]
        public void NandNode_10_Positive()
        {
            INode f1 = new FakeNode(true);
            INode f2 = new FakeNode(false);

            NandNode node = new NandNode("testName");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(true, node.Process()[0]);
        }

        [TestMethod]
        public void NandNode_11_Positive()
        {
            INode f1 = new FakeNode(true);
            INode f2 = new FakeNode(true);

            NandNode node = new NandNode("testName");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(false, node.Process()[0]);
        }

        #endregion nandNode

        #region andNode

        [TestMethod]
        public void AndNode_00_Positive()
        {
            INode f1 = new FakeNode(false);
            INode f2 = new FakeNode(false);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "AND");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(false, node.Process()[0]);
        }

        [TestMethod]
        public void AndNode_01_Positive()
        {
            INode f1 = new FakeNode(false);
            INode f2 = new FakeNode(true);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "AND");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(false, node.Process()[0]);
        }

        [TestMethod]
        public void AndNode_10_Positive()
        {
            INode f1 = new FakeNode(true);
            INode f2 = new FakeNode(false);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "AND");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(false, node.Process()[0]);
        }

        [TestMethod]
        public void AndNode_11_Positive()
        {
            INode f1 = new FakeNode(true);
            INode f2 = new FakeNode(true);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "AND");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(true, node.Process()[0]);
        }

        #endregion andNode

        #region orNode

        [TestMethod]
        public void OrNode_00_Positive()
        {
            INode f1 = new FakeNode(false);
            INode f2 = new FakeNode(false);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "OR");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(false, node.Process()[0]);
        }

        [TestMethod]
        public void OrNode_01_Positive()
        {
            INode f1 = new FakeNode(false);
            INode f2 = new FakeNode(true);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "OR");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(true, node.Process()[0]);
        }

        [TestMethod]
        public void OrNode_10_Positive()
        {
            INode f1 = new FakeNode(true);
            INode f2 = new FakeNode(false);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "OR");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(true, node.Process()[0]);
        }

        [TestMethod]
        public void OrNode_11_Positive()
        {
            INode f1 = new FakeNode(true);
            INode f2 = new FakeNode(true);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "OR");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(true, node.Process()[0]);
        }

        #endregion orNode

        #region norNode

        [TestMethod]
        public void NorNode_00_Positive()
        {
            INode f1 = new FakeNode(false);
            INode f2 = new FakeNode(false);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "NOR");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(true, node.Process()[0]);
        }

        [TestMethod]
        public void NorNode_01_Positive()
        {
            INode f1 = new FakeNode(false);
            INode f2 = new FakeNode(true);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "NOR");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(false, node.Process()[0]);
        }

        [TestMethod]
        public void NorNode_10_Positive()
        {
            INode f1 = new FakeNode(true);
            INode f2 = new FakeNode(false);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "NOR");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(false, node.Process()[0]);
        }

        [TestMethod]
        public void NorNode_11_Positive()
        {
            INode f1 = new FakeNode(true);
            INode f2 = new FakeNode(true);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "NOR");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(false, node.Process()[0]);
        }

        #endregion norNode

        #region xorNode

        [TestMethod]
        public void XorNode_00_Positive()
        {
            INode f1 = new FakeNode(false);
            INode f2 = new FakeNode(false);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "XOR");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(false, node.Process()[0]);
        }

        [TestMethod]
        public void XorNode_01_Positive()
        {
            INode f1 = new FakeNode(false);
            INode f2 = new FakeNode(true);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "XOR");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(true, node.Process()[0]);
        }

        [TestMethod]
        public void XorNode_10_Positive()
        {
            INode f1 = new FakeNode(true);
            INode f2 = new FakeNode(false);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "XOR");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(true, node.Process()[0]);
        }

        [TestMethod]
        public void XorNode_11_Positive()
        {
            INode f1 = new FakeNode(true);
            INode f2 = new FakeNode(true);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "XOR");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            Assert.AreEqual(false, node.Process()[0]);
        }

        #endregion xorNode

        #region notNode

        [TestMethod]
        public void NotNode_0_Positive()
        {
            INode f1 = new FakeNode(false);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "NOT");
            node.Inputs.Add(f1);

            Assert.AreEqual(true, node.Process()[0]);
        }

        [TestMethod]
        public void NotNode_1_Positive()
        {
            INode f1 = new FakeNode(true);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "NOT");
            node.Inputs.Add(f1);

            Assert.AreEqual(false, node.Process()[0]);
        }

        #endregion xorNode

        #region inputNode

        [TestMethod]
        public void InputNode_Positive()
        {
            InputNode node = new InputNode("testName", true, true);
            Assert.AreEqual(true, node.Process()[0]);

            node.Value = false;
            Assert.AreEqual(false, node.Process()[0]);

            node.Reset();
            Assert.AreEqual(true, node.Process()[0]);
        }

        #endregion inputNode

        #region outputNode

        [TestMethod]
        public void OutputNode_Positive()
        {
            OutputNode node = new OutputNode("testName");
            node.Input = new FakeNode(true);

            Assert.AreEqual(true, node.Process()[0]);
        }

        #endregion outputNode
    }
}
