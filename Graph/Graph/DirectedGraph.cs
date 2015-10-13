using System;
using System.Collections.Generic;

namespace Graph
{
    public class DirectedGraph<T> 
    {
        public List<DirectedEdge<T>> Edges { get; set; }

        public List<DirectedVertex<T>> Vertices { get; set; }

        public DirectedGraph()
        {
            this.Edges = new List<DirectedEdge<T>>();
            this.Vertices = new List<DirectedVertex<T>>();
        }

        public static DirectedGraph<T> CreateFromAdjacencyList(string[][] adjacencyList)
        {
            var graph = new DirectedGraph<T>();

            graph.Edges = new List<DirectedEdge<T>>();
            graph.Vertices = new List<DirectedVertex<T>>();

            for (int i = 0; i < adjacencyList.Length; i++)
            {
                for (int j = 0; j < adjacencyList[i].Length; j++)
                {
                    var arr = adjacencyList[i][j].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    var name = arr[0];

                    if (!graph.Vertices.Exists(_ => name.Equals(_.Name)))
                    {
                        graph.AddVertex(name);
                    }  
 
                    if (j != 0)
                    {
                        var tailArr = adjacencyList[i][0].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        var tail = graph.Vertices.Find(_ => tailArr[0].Equals(_.Name));
                        var head = graph.Vertices.Find(_ => name.Equals(_.Name));
                        int cost = 0;
                        if (arr.Length == 2)
                        {
                            cost = Convert.ToInt32(arr[1]);
                        }
                        graph.AddEdge(tail, head, cost);
                    }
                }
            }

            return graph;
        }

        public static DirectedGraph<double> CreateFromMatrix(string[,] matrix)
        {
            var graph = new DirectedGraph<double>();

            graph.Edges = new List<DirectedEdge<double>>();
            graph.Vertices = new List<DirectedVertex<double>>();

	        var numberOfVertices = 0;

			while (numberOfVertices < matrix.Length)
	        {
			    graph.AddVertex(numberOfVertices.ToString());
			    numberOfVertices++;
	        }

			// Create edges

            return graph;
        }

        public bool AddEdge(DirectedVertex<T> tail, DirectedVertex<T> head, int cost)
        {
            if (tail == null)
            {
                throw new ArgumentNullException("tail");
            }

            if (head == null)
            {
                throw new ArgumentNullException("head");
            }

            if (!this.HasVertex(tail.Name))
            {
                return false;
            }

            if (!this.HasVertex(head.Name))
            {
                return false;
            }

            var edge = new DirectedEdge<T>();
            edge.Tail = tail;
            edge.Head = head;
            edge.Cost = cost;

            if (this.Edges.Exists((x) => { return x.Cost.Equals(edge.Cost) && x.Head.Name.Equals(edge.Head.Name) && x.Tail.Name.Equals(edge.Tail.Name); }))
            {
                return false;
            } 
           
            this.Edges.Add(edge);
            tail.Edges.Add(edge);
            return true;
        }

        public bool AddEdge(DirectedVertex<T> tail, DirectedVertex<T> head)
        {
            return AddEdge(tail, head, 0);
        }

        public bool AddEdge(string tailName, string headName, int cost)
        {
            var tail = this.Vertices.Find(_ => tailName.Equals(_.Name));
            var head = this.Vertices.Find(_ => headName.Equals(_.Name));

            return this.AddEdge(tail, head, cost);
        }

        public bool AddEdge(string tailName, string headName)
        {
            return this.AddEdge(tailName, headName, 0);
        }

        public bool AddVertex(string name, T value)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (this.Vertices.Exists(_ => name.Equals(_.Name)))
            {
                return false;
            }

            var vertex = new DirectedVertex<T>();
            vertex.Name = name;
            vertex.Value = value;
            vertex.Edges = new List<DirectedEdge<T>>();
            this.Vertices.Add(vertex);

            return true;
        }

        public bool AddVertex(string name)
        {
            return this.AddVertex(name, default(T));
        }

        public bool HasVertex(string name)
        {
            return this.Vertices.Exists(_ => name.Equals(_.Name));
        }
        
        public bool RemoveVertex(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (!HasVertex(name))
            {
                return false;
            }

            var vertex = this.Vertices.Find(_ => name.Equals(_.Name));
            this.Edges.RemoveAll((_) => { return _.Head.Name.Equals(vertex.Name) || _.Tail.Name.Equals(vertex.Name); });

            foreach (var v in this.Vertices)
            {
                v.Edges.RemoveAll((_) => { return _.Head.Name.Equals(vertex.Name) || _.Tail.Name.Equals(vertex.Name); });
            }

            this.Vertices.Remove(vertex);

            return true;
        }

        public bool RemoveEdge(DirectedVertex<T> tail, DirectedVertex<T> head, int cost)
        {
            if (tail == null)
            {
                throw new ArgumentNullException("tail");
            }

            if (head == null)
            {
                throw new ArgumentNullException("head");
            }

            if (!this.HasVertex(tail.Name))
            {
                return false;
            }

            if (!this.HasVertex(head.Name))
            {
                return false;
            }

            var edge = this.Edges.Find((x) => { return x.Cost.Equals(cost) && x.Head.Name.Equals(head.Name) && x.Tail.Name.Equals(tail.Name); });

            if (edge == null)
            {
                return false;
            }

            this.Edges.Remove(edge);
            edge.Head.Edges.Remove(edge);
            edge.Tail.Edges.Remove(edge);

            return true;
        }

        public bool RemoveEdge(DirectedVertex<T> tail, DirectedVertex<T> head)
        {
            return RemoveEdge(tail, head, 0);
        }

        public bool RemoveEdge(string tailName, string headName, int cost)
        {
            var tail = this.Vertices.Find(_ => tailName.Equals(_.Name));
            var head = this.Vertices.Find(_ => headName.Equals(_.Name));

            return this.RemoveEdge(tail, head, cost);
        }

        public bool RemoveEdge(string tailName, string headName)
        {
            return this.RemoveEdge(tailName, headName, 0);
        }

        public DirectedVertex<T> GetVertex(string name)
        {
            return this.Vertices.Find(_ => _.Name.Equals(name));
        }
    }
}
