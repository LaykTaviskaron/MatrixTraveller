using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
			var graph = DirectedGraph<double>.CreateFromMatrix(AdjacencyListReader.ReadListFromFile("C:\\Users\\Artem.Sokolov\\Desktop\\test-matrix.txt"));
	        var c = graph.Vertices.Count;
        }
    }
}
