namespace MGraph
{
    /// <summary>
    /// Immutable Vertex and Edge set.
    /// Can be applied to immutable graphs.
    /// </summary>
    public interface IVertexAndEdgeSet<TVertex, TEdge> : 
        IVertexSet<TVertex>, IEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TVertex : IVertex
    {
    }
}
