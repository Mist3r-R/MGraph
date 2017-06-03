namespace MGraph
{
    /// <summary>
    /// Mutable vertex and edge set.
    /// Can be used to extend graph to a mutable one.
    /// </summary>
    public interface IMutableVertexAndEdgeSet<TVertex, TEdge> :
        IMutableVertexSet<TVertex>, IMutableEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TVertex : IVertex
    {
    }
}
