using System.Collections.Generic;

namespace MGraph
{
    /// <summary>
    /// Represents an immutable undirected graph.
    /// </summary>
    public interface IUndirectedGraph<TVertex, TEdge> : IGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TVertex : IVertex
    {
        IEnumerable<TEdge> AdjacentEdges(TVertex vertex);
        int Degree(TVertex vertex);
    }
}
