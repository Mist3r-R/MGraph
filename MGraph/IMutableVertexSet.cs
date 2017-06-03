namespace MGraph
{
    /// <summary>
    /// Vertex set provided with add & remove operations.
    /// </summary>
    public interface IMutableVertexSet<TVertex> : IVertexSet<TVertex>
        where TVertex : IVertex
    {
        bool AddVertex(TVertex vertex);
        bool RemoveVertex(TVertex vertex);
    }
}
