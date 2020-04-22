using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Logic_Circuit.Parser
{
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

        private Dictionary<string, string> Files = new Dictionary<string, string>();

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

            var validation = IsValidFile();

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

        private (bool success, string validationError) IsValidFile()
        {
            // TODO todo to do TO DO : re-add validation

            return (
                success: true,
                validationError: ""
            );
        }
    }
}
