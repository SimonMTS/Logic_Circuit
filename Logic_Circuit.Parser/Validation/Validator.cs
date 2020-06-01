using Logic_Circuit.Parser.Validation.VisitorObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Parser.Validation
{
    public class Validator
    {
        public static (bool success, string validationError) Validate(string content)
        {
            FileAsObject o = new FileAsObject();
            using (StringReader reader = new StringReader(content))
            {
                bool blockOne = true;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("#")) { continue; }
                    if (line.Equals("")) { blockOne = false; continue; }

                    if (blockOne)
                    {
                        o.Attach(new NodeLine(line));
                    }
                    else
                    {
                        o.Attach(new ConnectionLine(line));
                    }
                }
            }

            FormatChecker formatChecker = new FormatChecker(content);
            LineChecker lineChecker = new LineChecker(); 
                if (InternalCircuitNamesForTests != null) lineChecker.SetInternalCircuitNamesForTests(InternalCircuitNamesForTests);
            LoopChecker loopChecker = new LoopChecker(content);
            HangingConnectionChecker hangingConnectionChecker = new HangingConnectionChecker(content);

            (bool success, string validationError) res;
            if (!(res = o.Accept(formatChecker)).success) return res;
            if (!(res = o.Accept(lineChecker)).success) return res;
            if (!(res = o.Accept(hangingConnectionChecker)).success) return res;
            if (!(res = o.Accept(loopChecker)).success) return res;

            return (
                success: true,
                validationError: ""
            );
        }

        public static string[] InternalCircuitNamesForTests = null;
    }
}
