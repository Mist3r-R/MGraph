namespace MGraph
{
    /// <summary>
    /// Edge set provided with add & remove operations.
    /// </summary>
    public interface IMutableEdgeSet<TVertex, TEdge> : IEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TVertex : IVertex
    {
        bool AddEdge(TEdge edge);
        bool RemoveEdge(TEdge edge);
    }
}
