using System;
using Logic_Circuit.Parser.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logic_Circuit.UnitTests.Parser
{
    [TestClass]
    public class ValidatorTests
    {
        [TestMethod]
        public void General_Positive()
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

            Validator.InternalCircuitNamesForTests = new string[] { "INPUT_HIGH", "INPUT_LOW", "PROBE", "NAND" };
            (bool success, string validationError) res = Validator.Validate(file);
            Assert.AreEqual(true, res.success);
        }

        [TestMethod]
        public void BadSyntax_Negative()
        {
            string file = "# Test" + Environment.NewLine +
                "A:	INPUT_HIGH;" + Environment.NewLine +
                "B:	INPUT_HIGH;" + Environment.NewLine +
                "S:	PROBE;" + Environment.NewLine +
                "NODE1: NAND" + Environment.NewLine +
                "" + Environment.NewLine +
                "A:	NODE1;" + Environment.NewLine +
                "B:	NODE1;" + Environment.NewLine +
                "NODE1:	S;";

            Validator.InternalCircuitNamesForTests = new string[] { "INPUT_HIGH", "INPUT_LOW", "PROBE", "NAND" };
            (bool success, string validationError) res = Validator.Validate(file);
            Assert.AreEqual(false, res.success);
            Assert.AreEqual("Line doesn't conform to syntax: 'NODE1: NAND'", res.validationError);
        }

        [TestMethod]
        public void BadFormat_Negative()
        {
            string file = "# Test" + Environment.NewLine +
                "A:	INPUT_HIGH;" + Environment.NewLine +
                "B:	INPUT_HIGH;" + Environment.NewLine +
                "S:	PROBE;" + Environment.NewLine +
                "NODE1: NAND;" + Environment.NewLine +
                "A:	NODE1;" + Environment.NewLine +
                "B:	NODE1;" + Environment.NewLine +
                "NODE1:	S;";

            Validator.InternalCircuitNamesForTests = new string[] { "INPUT_HIGH", "INPUT_LOW", "PROBE", "NAND" };
            (bool success, string validationError) res = Validator.Validate(file);
            Assert.AreEqual(false, res.success);
            Assert.AreEqual("'NODE1' is not a valid node type.", res.validationError);
        }

        [TestMethod]
        public void BadContent_Negative()
        {
            string file = "just some text";

            Validator.InternalCircuitNamesForTests = new string[] { "INPUT_HIGH", "INPUT_LOW", "PROBE", "NAND" };
            (bool success, string validationError) res = Validator.Validate(file);
            Assert.AreEqual(false, res.success);
            Assert.AreEqual("Line doesn't conform to syntax: 'just some text'", res.validationError);
        }
    }
}
