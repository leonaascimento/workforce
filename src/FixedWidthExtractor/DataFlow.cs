using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Workforce.FixedWidthExtractor
{
    public class DataFlow
    {
        private const char separator = ',';

        private readonly string sourcePath;
        private readonly string destinationPath;
        private readonly List<(int, int, string)> template;

        public DataFlow(
            string sourcePath,
            string destinationPath,
            List<(int, int, string)> template)
        {
            this.sourcePath = sourcePath;
            this.destinationPath = destinationPath;
            this.template = template;
        }

        public void Run()
        {
            using (var reader = new StreamReader(sourcePath))
            {
                using (var writer = new StreamWriter(destinationPath))
                {
                    writer.WriteLine(GetTemplateHeading());

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        writer.WriteLine(GetTemplateFormattedLine(line));
                    }
                }
            }
        }

        private string GetTemplateFormattedLine(string line)
        {
            var builder = new StringBuilder();

            var first = true;
            foreach (var field in template)
            {
                if (!first)
                    builder.Append(separator);

                var startIndex = field.Item1;
                var length = field.Item2 - field.Item1 + 1;

                builder.Append(line.Substring(startIndex, length).Trim());
                first = false;
            }

            return builder.ToString();
        }

        private string GetTemplateHeading()
        {
            var builder = new StringBuilder();
            var first = true;

            foreach (var field in template)
            {
                if (!first)
                    builder.Append(separator);

                builder.Append(field.Item3);
                first = false;
            }

            return builder.ToString();
        }
    }
}
