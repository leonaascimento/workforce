using System.Collections.Generic;
using System.IO;

namespace Workforce.FixedWidthExtractor
{
    public static class TemplateReader
    {
        private const char separator = ';';

        public static List<(int, int, string)> Read(string path)
        {
            var template = new List<(int, int, string)>();

            using (var reader = new StreamReader(path, true))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var columns = line.Split(separator);

                    var startIndex = int.Parse(columns[0].Trim()) - 1;
                    var endIndex = int.Parse(columns[1].Trim()) - 1;
                    var name = columns[2].Trim();

                    template.Add((startIndex, endIndex, name));
                }
            }

            return template;
        }
    }
}
