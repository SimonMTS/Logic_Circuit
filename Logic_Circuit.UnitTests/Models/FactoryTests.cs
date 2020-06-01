using System;
using Logic_Circuit.Models.BaseNodes;
using Logic_Circuit.Models.Factories;
using Logic_Circuit.Models.Nodes;
using Logic_Circuit.Models.Nodes.NodeInputTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logic_Circuit.UnitTests.Models
{
    [TestClass]
    public class FactoryTests
    {
        #region specificNodeFactories

        [TestMethod]
        public void NandNodeFactoryGeneral_Positive()
        {
            INodeFactory factory = new NandNodeFactory();

            INode node = factory.GetNode("testName", "irrelevant");

            Assert.AreEqual(true, node is NandNode);
            Assert.AreEqual(true, node is IMultipleInputs);
            Assert.AreEqual(false, node is ISingleInput);
            Assert.AreEqual("testName", node.Name);
        }

        [TestMethod]
        public void InputNodeFactoryGeneral_Positive()
        {
            INodeFactory factory = new InputNodeFactory();

            INode node = factory.GetNode("testName", "INPUT_HIGH");

            Assert.AreEqual(true, node is InputNode);
            Assert.AreEqual(false, node is IMultipleInputs);
            Assert.AreEqual(false, node is ISingleInput);
            Assert.AreEqual("testName", node.Name);
            Assert.AreEqual(true, ((InputNode)node).Value);
            Assert.AreEqual(true, ((InputNode)node).DefaultValue);
        }

        [TestMethod]
        public void OutputNodeFactoryGeneral_Positive()
        {
            INodeFactory factory = new OutputNodeFactory();

            INode node = factory.GetNode("testName", "irrelevant");

            Assert.AreEqual(true, node is OutputNode);
            Assert.AreEqual(false, node is IMultipleInputs);
            Assert.AreEqual(true, node is ISingleInput);
            Assert.AreEqual("testName", node.Name);
        }

        #endregion specificNodeFactories

        #region circuitNodeFactories

        [TestMethod]
        public void CircuitNodeFactory_And_Positive()
        {
            TestHelper.SetTestPaths();
            INodeFactory factory = new CircuitNodeFactory();

            INode node = factory.GetNode("testName", "AND");

            Assert.AreEqual(true, node is CircuitNode);
            Assert.AreEqual(true, node is IMultipleInputs);
            Assert.AreEqual(false, node is ISingleInput);
            Assert.AreEqual("testName", node.Name);
            Assert.AreEqual("AND.txt", ((CircuitNode)node).Circuit.Name);
        }

        [TestMethod]
        public void CircuitNodeFactory_Decoder_Positive()
        {
            TestHelper.SetTestPaths();
            INodeFactory factory = new CircuitNodeFactory();

            INode node = factory.GetNode("testName", "DECODER");

            Assert.AreEqual(true, node is CircuitNode);
            Assert.AreEqual(true, node is IMultipleInputs);
            Assert.AreEqual(false, node is ISingleInput);
            Assert.AreEqual("testName", node.Name);
            Assert.AreEqual("DECODER.txt", ((CircuitNode)node).Circuit.Name);
        }

        [TestMethod]
        public void CircuitNodeFactory_Encoder_Positive()
        {
            TestHelper.SetTestPaths();
            INodeFactory factory = new CircuitNodeFactory();

            INode node = factory.GetNode("testName", "ENCODER");

            Assert.AreEqual(true, node is CircuitNode);
            Assert.AreEqual(true, node is IMultipleInputs);
            Assert.AreEqual(false, node is ISingleInput);
            Assert.AreEqual("testName", node.Name);
            Assert.AreEqual("ENCODER.txt", ((CircuitNode)node).Circuit.Name);
        }

        [TestMethod]
        public void CircuitNodeFactory_Nor_Positive()
        {
            TestHelper.SetTestPaths();
            INodeFactory factory = new CircuitNodeFactory();

            INode node = factory.GetNode("testName", "NOR");

            Assert.AreEqual(true, node is CircuitNode);
            Assert.AreEqual(true, node is IMultipleInputs);
            Assert.AreEqual(false, node is ISingleInput);
            Assert.AreEqual("testName", node.Name);
            Assert.AreEqual("NOR.txt", ((CircuitNode)node).Circuit.Name);
        }

        [TestMethod]
        public void CircuitNodeFactory_Not_Positive()
        {
            TestHelper.SetTestPaths();
            INodeFactory factory = new CircuitNodeFactory();

            INode node = factory.GetNode("testName", "NOT");

            Assert.AreEqual(true, node is CircuitNode);
            Assert.AreEqual(true, node is IMultipleInputs);
            Assert.AreEqual(false, node is ISingleInput);
            Assert.AreEqual("testName", node.Name);
            Assert.AreEqual("NOT.txt", ((CircuitNode)node).Circuit.Name);
        }

        [TestMethod]
        public void CircuitNodeFactory_Or_Positive()
        {
            TestHelper.SetTestPaths();
            INodeFactory factory = new CircuitNodeFactory();

            INode node = factory.GetNode("testName", "OR");

            Assert.AreEqual(true, node is CircuitNode);
            Assert.AreEqual(true, node is IMultipleInputs);
            Assert.AreEqual(false, node is ISingleInput);
            Assert.AreEqual("testName", node.Name);
            Assert.AreEqual("OR.txt", ((CircuitNode)node).Circuit.Name);
        }

        [TestMethod]
        public void CircuitNodeFactory_Xor_Positive()
        {
            TestHelper.SetTestPaths();
            INodeFactory factory = new CircuitNodeFactory();

            INode node = factory.GetNode("testName", "XOR");

            Assert.AreEqual(true, node is CircuitNode);
            Assert.AreEqual(true, node is IMultipleInputs);
            Assert.AreEqual(false, node is ISingleInput);
            Assert.AreEqual("testName", node.Name);
            Assert.AreEqual("XOR.txt", ((CircuitNode)node).Circuit.Name);
        }

        #endregion circuitNodeFactories

        #region generalNodeFactory

        [TestMethod]
        public void NodeFactory_Nand_Positive()
        {
            TestHelper.SetTestPaths();
            INodeFactory factory = new NodeFactory();

            INode node = factory.GetNode("testName", "NAND");

            Assert.AreEqual(true, node is NandNode);
            Assert.AreEqual(true, node is IMultipleInputs);
            Assert.AreEqual(false, node is ISingleInput);
            Assert.AreEqual("testName", node.Name);
        }

        [TestMethod]
        public void NodeFactory_InputHigh_Positive()
        {
            TestHelper.SetTestPaths();
            INodeFactory factory = new NodeFactory();

            INode node = factory.GetNode("testName", "INPUT_HIGH");

            Assert.AreEqual(true, node is InputNode);
            Assert.AreEqual(false, node is IMultipleInputs);
            Assert.AreEqual(false, node is ISingleInput);
            Assert.AreEqual("testName", node.Name);
        }

        [TestMethod]
        public void NodeFactory_InputLow_Positive()
        {
            TestHelper.SetTestPaths();
            INodeFactory factory = new NodeFactory();

            INode node = factory.GetNode("testName", "INPUT_LOW");

            Assert.AreEqual(true, node is InputNode);
            Assert.AreEqual(false, node is IMultipleInputs);
            Assert.AreEqual(false, node is ISingleInput);
            Assert.AreEqual("testName", node.Name);
        }

        [TestMethod]
        public void NodeFactory_Output_Positive()
        {
            TestHelper.SetTestPaths();
            INodeFactory factory = new NodeFactory();

            INode node = factory.GetNode("testName", "PROBE");

            Assert.AreEqual(true, node is OutputNode);
            Assert.AreEqual(false, node is IMultipleInputs);
            Assert.AreEqual(true, node is ISingleInput);
            Assert.AreEqual("testName", node.Name);
        }

        [TestMethod]
        public void NodeFactory_CircuitWrongName_Negative()
        {
            TestHelper.SetTestPaths();
            INodeFactory factory = new NodeFactory();

            Assert.ThrowsException<System.IO.FileNotFoundException>(() => factory.GetNode("testName", "AAND"));
        }

        [TestMethod]
        public void NodeFactory_Circuit_Positive()
        {
            TestHelper.SetTestPaths();
            INodeFactory factory = new NodeFactory();

            INode node = factory.GetNode("testName", "AND");

            Assert.AreEqual(true, node is CircuitNode);
            Assert.AreEqual(true, node is IMultipleInputs);
            Assert.AreEqual(false, node is ISingleInput);
            Assert.AreEqual("testName", node.Name);
            Assert.AreEqual("AND.txt", ((CircuitNode)node).Circuit.Name);
        }

        #endregion generalNodeFactory
    }
}
