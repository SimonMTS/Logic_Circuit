using System;
using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Factories;
using Logic_Circuit.Models.Strategies;
using Logic_Circuit.Models.Strategies.NodeProcessStrategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logic_Circuit.UnitTests.Models
{
    [TestClass]
    public class StrategyTests
    {
        [TestMethod]
        public void OneToOneInputStrategy_Positive()
        {
            INode f1 = new FakeNode(false);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "NOT");
            node.Inputs.Add(f1);

            NodeProcessContext context = new NodeProcessContext(new OneToOneInputStrategy());
            bool[] res = context.ProcessInput(node);

            Assert.AreEqual(true, res[0]);
        }

        [TestMethod]
        public void NToOneInputStrategy_Positive()
        {
            INode f1 = new FakeNode(false);
            INode f2 = new FakeNode(true);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "AND");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);

            NodeProcessContext context = new NodeProcessContext(new NToOneInputStrategy());
            bool[] res = context.ProcessInput(node);

            Assert.AreEqual(false, res[0]);
        }

        [TestMethod]
        public void NToNInputStrategy_Positive()
        {
            INode f1 = new FakeNode(false);
            INode f2 = new FakeNode(false);
            INode f3 = new FakeNode(false);
            INode f4 = new FakeNode(true);
            INode f5 = new FakeNode(false);
            INode f6 = new FakeNode(false);
            INode f7 = new FakeNode(false);
            INode f8 = new FakeNode(false);

            TestHelper.SetTestPaths();
            CircuitNode node = (CircuitNode)new CircuitNodeFactory().GetNode("testName", "ENCODER");
            node.Inputs.Add(f1);
            node.Inputs.Add(f2);
            node.Inputs.Add(f3);
            node.Inputs.Add(f4);
            node.Inputs.Add(f5);
            node.Inputs.Add(f6);
            node.Inputs.Add(f7);
            node.Inputs.Add(f8);

            NodeProcessContext context = new NodeProcessContext(new NToNInputStrategy());
            bool[] res = context.ProcessInput(node);

            Assert.AreEqual(true, res[0]);
            Assert.AreEqual(true, res[1]);
            Assert.AreEqual(false, res[2]);
        }
    }
}
