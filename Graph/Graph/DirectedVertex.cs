using System.Collections.Generic;

namespace Graph
{
    public class DirectedVertex<T>
    {
        public T Value { get; set; }

        public string Name { get; set; }

        public List<DirectedEdge<T>> Edges { get; set; }
    }
}
