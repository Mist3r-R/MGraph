namespace MGraph
{
    /// <summary>
    /// Represents a flow.
    /// </summary>
    public interface IFlow<TVertex, TEdge> : IDirectedGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TVertex : IVertex
    {
        TVertex Source { get; }
        TVertex Sink { get; }
    }
}
