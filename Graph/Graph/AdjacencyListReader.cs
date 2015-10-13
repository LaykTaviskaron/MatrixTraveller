using System;
using System.IO;

namespace Graph
{
    public static class AdjacencyListReader
    {
        public static string[,] ReadListFromFile(string path)
        {         
            var lines = File.ReadAllLines(path);

            if (lines.Length == 0)
            {
                throw new InvalidOperationException("File content is not valid");
            }

			var firstLine = lines[0].Split(new []{","}, StringSplitOptions.RemoveEmptyEntries);
			var model = new string[lines.Length,firstLine.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                var separator = new string[] { "," };
                var line = lines[i].Split(separator, StringSplitOptions.RemoveEmptyEntries);

	            for (int j = 0; j < line.Length; j++)
	            {
		            model[i,j] = line[j];
	            }
            }

            return model;
        }
    }
}
