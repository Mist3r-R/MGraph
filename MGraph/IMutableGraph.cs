namespace MGraph
{
    /// <summary>
    /// Mutable graph.
    /// Can be cleared.
    /// </summary>
    public interface IMutableGraph<TVertex, TEdge> : IGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TVertex : IVertex
    {
        bool Clear();
    }
}
