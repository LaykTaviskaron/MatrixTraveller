using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Dijkstra<T>
    {
        private const double MAX_PATH = double.PositiveInfinity;

        private HashSet<DirectedVertex<T>> explored = new HashSet<DirectedVertex<T>>();

        private Dictionary<DirectedVertex<T>, double> paths = new Dictionary<DirectedVertex<T>, double>();

        public Dictionary<DirectedVertex<T>, double> FindShortestPaths(DirectedGraph<T> graph, string start)
        {
            if (start == null)
            {
                throw new ArgumentNullException("start");
            }

            if (graph == null)
            {
                throw new ArgumentNullException("graph");
            }

            return FindShortestPaths(graph, graph.GetVertex(start));
        }

        public Dictionary<DirectedVertex<T>, double> FindShortestPaths(DirectedGraph<T> graph, DirectedVertex<T> start)
        {
            if (graph == null)
            {
                throw new ArgumentNullException("graph");
            }

            if (start == null)
            {
                throw new ArgumentNullException("start");
            }

            if (!graph.HasVertex(start.Name))
            {
                throw new ArgumentException("There is no such vertex");
            }

            explored.Add(start);

            foreach (var vertex in graph.Vertices)
            {
                paths.Add(vertex, MAX_PATH);
            }

            paths[start] = 0;

            while (explored.Count != graph.Vertices.Count)
            {
                var crossingEdges = graph.Edges.Where((edge) => { return explored.Contains(edge.Tail) && (!explored.Contains(edge.Head)); });
                var greedyCriteria = new Dictionary<DirectedEdge<T>, double>();

                foreach (var edge in crossingEdges)
                {
                    var greedyCriterion = paths[edge.Tail] + edge.Cost;
                    greedyCriteria.Add(edge, greedyCriterion);
                }

                var min = greedyCriteria.ElementAt(0).Value;
                var bestEdge = greedyCriteria.ElementAt(0).Key;

                foreach (var item in greedyCriteria)
                {
                    if (item.Value < min)
                    {
                        min = item.Value;
                        bestEdge = item.Key;
                    }
                }

                explored.Add(bestEdge.Head);
                paths[bestEdge.Head] = paths[bestEdge.Tail] + bestEdge.Cost;
            }

            return paths;
        }
    }
}
