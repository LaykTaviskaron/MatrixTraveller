
namespace Graph
{
    public class DirectedEdge<T> : Edge
    {
        public DirectedVertex<T> Tail { get; set; }

        public DirectedVertex<T> Head { get; set; }
    }
}
