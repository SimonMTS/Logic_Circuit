using System;
using Logic_Circuit.Parser.Validation.VisitorObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logic_Circuit.UnitTests.Parser
{
    [TestClass]
    public class CheckerTests
    {

        #region FormatChecker

        [TestMethod]
        public void FormatChecker_Positive()
        {
            string file = "# Test" + Environment.NewLine +
                "A:	INPUT_HIGH;" + Environment.NewLine +
                "B:	INPUT_HIGH;" + Environment.NewLine +
                "S:	PROBE;" + Environment.NewLine +
                "NODE1:	NAND;" + Environment.NewLine +
                "" + Environment.NewLine +
                "A:	NODE1;" + Environment.NewLine +
                "B:	NODE1;" + Environment.NewLine +
                "NODE1:	S;";

            FileAsObject o = new FileAsObject();
            FormatChecker formatChecker = new FormatChecker(file);

            o.Attach(new NodeLine("A:	INPUT_HIGH;"));
            o.Attach(new NodeLine("B:	INPUT_HIGH;"));
            o.Attach(new NodeLine("S:	PROBE;"));
            o.Attach(new NodeLine("NODE1:	NAND;"));

            o.Attach(new ConnectionLine("A:	NODE1;"));
            o.Attach(new ConnectionLine("B:	NODE1;"));
            o.Attach(new ConnectionLine("NODE1:	S;"));

            (bool success, string validationError) res = o.Accept(formatChecker);
            Assert.AreEqual(true, res.success);
        }

        [TestMethod]
        public void FormatChecker_Negative()
        {
            string file = "# Test" + Environment.NewLine +
                "A:	INPUT_HIGH;" + Environment.NewLine +
                "B:	INPUT_HIGH;" + Environment.NewLine +
                "S:	PROBE;" + Environment.NewLine +
                "NODE1:	NAND;" + Environment.NewLine +
                "A:	NODE1;" + Environment.NewLine +
                "B:	NODE1;" + Environment.NewLine +
                "NODE1:	S;";

            FileAsObject o = new FileAsObject();
            FormatChecker formatChecker = new FormatChecker(file);

            o.Attach(new NodeLine("A:	INPUT_HIGH;"));
            o.Attach(new NodeLine("B:	INPUT_HIGH;"));
            o.Attach(new NodeLine("S:	PROBE;"));
            o.Attach(new NodeLine("NODE1:	NAND;"));

            o.Attach(new ConnectionLine("A:	NODE1;"));
            o.Attach(new ConnectionLine("B:	NODE1;"));
            o.Attach(new ConnectionLine("NODE1:	S;"));

            (bool success, string validationError) res = o.Accept(formatChecker);
            Assert.AreEqual(false, res.success);
            Assert.AreEqual("Encountered connection before empty line. (line: 'A:	NODE1;')", res.validationError);
        }

        #endregion FormatChecker

        #region HangingConnection

        [TestMethod]
        public void HangingConnection_Positive()
        {
            string file = "# Test" + Environment.NewLine +
                "A:	INPUT_HIGH;" + Environment.NewLine +
                "B:	INPUT_HIGH;" + Environment.NewLine +
                "S:	PROBE;" + Environment.NewLine +
                "NODE1:	NAND;" + Environment.NewLine +
                "" + Environment.NewLine +
                "A:	NODE1;" + Environment.NewLine +
                "B:	NODE1;" + Environment.NewLine +
                "NODE1:	S;";

            FileAsObject o = new FileAsObject();
            HangingConnectionChecker hangingConnectionChecker = new HangingConnectionChecker(file);

            o.Attach(new NodeLine("A:	INPUT_HIGH;"));
            o.Attach(new NodeLine("B:	INPUT_HIGH;"));
            o.Attach(new NodeLine("S:	PROBE;"));
            o.Attach(new NodeLine("NODE1:	NAND;"));

            o.Attach(new ConnectionLine("A:	NODE1;"));
            o.Attach(new ConnectionLine("B:	NODE1;"));
            o.Attach(new ConnectionLine("NODE1:	S;"));

            (bool success, string validationError) res = o.Accept(hangingConnectionChecker);
            Assert.AreEqual(true, res.success);
        }

        [TestMethod]
        public void HangingConnection_Negative()
        {
            string file = "# Test" + Environment.NewLine +
                "A:	INPUT_HIGH;" + Environment.NewLine +
                "B:	INPUT_HIGH;" + Environment.NewLine +
                "S:	PROBE;" + Environment.NewLine +
                "NODE1:	NAND;" + Environment.NewLine +
                "NODE2:	NAND;" + Environment.NewLine +
                "" + Environment.NewLine +
                "A:	NODE1,NODE2;" + Environment.NewLine +
                "B:	NODE1;" + Environment.NewLine +
                "NODE1:	S;";

            FileAsObject o = new FileAsObject();
            HangingConnectionChecker hangingConnectionChecker = new HangingConnectionChecker(file);

            o.Attach(new NodeLine("A:	INPUT_HIGH;"));
            o.Attach(new NodeLine("B:	INPUT_HIGH;"));
            o.Attach(new NodeLine("S:	PROBE;"));
            o.Attach(new NodeLine("NODE1:	NAND;"));
            o.Attach(new NodeLine("NODE2:	NAND;"));

            o.Attach(new ConnectionLine("A:	NODE1,NODE2;"));
            o.Attach(new ConnectionLine("B:	NODE1;"));
            o.Attach(new ConnectionLine("NODE1:	S;"));

            (bool success, string validationError) res = o.Accept(hangingConnectionChecker);
            Assert.AreEqual(false, res.success);
            Assert.AreEqual("NODE2 has no outputs.", res.validationError);
        }

        #endregion HangingConnection

        #region Line

        [TestMethod]
        public void Line_Positive()
        {
            FileAsObject o = new FileAsObject();
            LineChecker lineChecker = new LineChecker();
            lineChecker.SetInternalCircuitNamesForTests(new string[] { "INPUT_HIGH", "INPUT_LOW", "PROBE", "NAND" });

            o.Attach(new NodeLine("A:	INPUT_HIGH;"));
            o.Attach(new NodeLine("B:	INPUT_HIGH;"));
            o.Attach(new NodeLine("S:	PROBE;"));
            o.Attach(new NodeLine("NODE1:	NAND;"));

            o.Attach(new ConnectionLine("A:	NODE1;"));
            o.Attach(new ConnectionLine("B:	NODE1;"));
            o.Attach(new ConnectionLine("NODE1:	S;"));

            (bool success, string validationError) res = o.Accept(lineChecker);
            Assert.AreEqual(true, res.success);
        }

        [TestMethod]
        public void Line_Negative()
        {
            FileAsObject o = new FileAsObject();
            LineChecker lineChecker = new LineChecker();
            lineChecker.SetInternalCircuitNamesForTests(new string[] { "INPUT_HIGH", "INPUT_LOW", "PROBE", "NAND" });

            o.Attach(new NodeLine("A:	INPUT_HIGH;"));
            o.Attach(new NodeLine("B:	INPUT_HIGH;"));
            o.Attach(new NodeLine("S:	PROBE;"));
            o.Attach(new NodeLine("NODE1:	NONEXISTENT;"));

            o.Attach(new ConnectionLine("A:	NODE1;"));
            o.Attach(new ConnectionLine("B:	NODE1;"));
            o.Attach(new ConnectionLine("NODE1:	S;"));

            (bool success, string validationError) res = o.Accept(lineChecker);
            Assert.AreEqual(false, res.success);
            Assert.AreEqual("'NONEXISTENT' is not a valid node type.", res.validationError);
        }

        #endregion Line

        #region Loop

        [TestMethod]
        public void Loop_Positive()
        {
            string file = "# Test" + Environment.NewLine +
                "A:	INPUT_HIGH;" + Environment.NewLine +
                "B:	INPUT_HIGH;" + Environment.NewLine +
                "S:	PROBE;" + Environment.NewLine +
                "NODE1:	NAND;" + Environment.NewLine +
                "" + Environment.NewLine +
                "A:	NODE1;" + Environment.NewLine +
                "B:	NODE1;" + Environment.NewLine +
                "NODE1:	S;";

            FileAsObject o = new FileAsObject();
            LoopChecker loopChecker = new LoopChecker(file);

            o.Attach(new NodeLine("A:	INPUT_HIGH;"));
            o.Attach(new NodeLine("B:	INPUT_HIGH;"));
            o.Attach(new NodeLine("S:	PROBE;"));
            o.Attach(new NodeLine("NODE1:	NAND;"));

            o.Attach(new ConnectionLine("A:	NODE1;"));
            o.Attach(new ConnectionLine("B:	NODE1;"));
            o.Attach(new ConnectionLine("NODE1:	S;"));

            (bool success, string validationError) res = o.Accept(loopChecker);
            Assert.AreEqual(true, res.success);
        }

        [TestMethod]
        public void Loop_Negative()
        {
            string file = "# Test" + Environment.NewLine +
                "A:	INPUT_HIGH;" + Environment.NewLine +
                "B:	INPUT_HIGH;" + Environment.NewLine +
                "S:	PROBE;" + Environment.NewLine +
                "NODE1:	NAND;" + Environment.NewLine +
                "NODE2:	NAND;" + Environment.NewLine +
                "NODE3:	NAND;" + Environment.NewLine +
                "" + Environment.NewLine +
                "A:	NODE1;" + Environment.NewLine +
                "B:	NODE1;" + Environment.NewLine +
                "NODE1:	NODE2;" + Environment.NewLine +
                "NODE2:	NODE1;" + Environment.NewLine +
                "NODE3:	S;";

            FileAsObject o = new FileAsObject();
            LoopChecker loopChecker = new LoopChecker(file);

            o.Attach(new NodeLine("A:	INPUT_HIGH;"));
            o.Attach(new NodeLine("B:	INPUT_HIGH;"));
            o.Attach(new NodeLine("S:	PROBE;"));
            o.Attach(new NodeLine("NODE1:	NAND;"));
            o.Attach(new NodeLine("NODE2:	NAND;"));
            o.Attach(new NodeLine("NODE3:	NAND;"));

            o.Attach(new ConnectionLine("A:	NODE1;"));
            o.Attach(new ConnectionLine("B:	NODE1;"));
            o.Attach(new ConnectionLine("NODE1:	NODE2;"));
            o.Attach(new ConnectionLine("NODE2:	NODE1;"));
            o.Attach(new ConnectionLine("NODE3:	S;"));

            (bool success, string validationError) res = o.Accept(loopChecker);
            Assert.AreEqual(false, res.success);
            Assert.AreEqual("Node: 'A' leads to an infinite loop.", res.validationError);
        }

        #endregion Loop

    }
}
