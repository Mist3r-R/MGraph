using System.Collections.Generic;

namespace MGraph
{
    /// <summary>
    /// Immutable edge set.
    /// </summary>
    public interface IEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TVertex : IVertex
    {
        int EdgeCount { get; }
        IEnumerable<TEdge> Edges { get; }
        bool IsEdgesEmpty { get; }
        bool ContainsEdge(TEdge edge);
    }
}
