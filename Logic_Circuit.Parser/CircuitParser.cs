using Logic_Circuit.Parser.Validation;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Logic_Circuit.Parser
{
    /// <summary>
    /// A singleton that exposes functions to read (relevant) lines of an input file, one-by-one.
    /// </summary>
    public class CircuitParser
    {
        private static CircuitParser ParserInstance = null;
        public static CircuitParser GetParser()
        {
            if (ParserInstance == null)
            {
                ParserInstance = new CircuitParser();
            }

            return ParserInstance;
        }

        private readonly Dictionary<string, string> Files = new Dictionary<string, string>();

        public IEnumerable<(string name, string type)> GetNodeString(string fileName)
        {
            using (StringReader reader = new StringReader(Files[fileName]))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("#")) { continue; }
                    if (line.Equals("")) break;

                    line = Regex.Replace(line, @"\s+", "").Replace(";", "");
                    string[] parsedLine = line.Split(':');

                    yield return (name: parsedLine[0], type: parsedLine[1]);
                }
            }
        }

        public IEnumerable<(string input, string[] outputs)> GetConnectionString(string fileName)
        {
            using (StringReader reader = new StringReader(Files[fileName]))
            {
                bool blockOneSkipped = false;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("#")) { continue; }
                    if (!blockOneSkipped) { blockOneSkipped = line.Equals(""); continue; }

                    line = Regex.Replace(line, @"\s+", "").Replace(";", "");
                    string[] parsedLine = line.Split(':');
                    string[] parsedOutputs = parsedLine[1].Split(',');

                    yield return (input: parsedLine[0], outputs: parsedOutputs);
                }
            }
        }

        public (bool success, string fileName, string error) AddFile(string filePath)
        {
            string onlyFileName = Path.GetFileName(filePath);
            string content = File.ReadAllText(filePath);

            var validation = Validator.Validate(content);

            if (!validation.success)
            {
                return (
                    success: false,
                    fileName: onlyFileName,
                    error: validation.validationError
                );
            }
            else
            {
                if (!Files.ContainsKey(onlyFileName))
                {
                    Files.Add(onlyFileName, content);
                }

                return (
                    success: true,
                    fileName: onlyFileName,
                    error: ""
                );
            }
        }
    }
}
