using System.Collections.Generic;

namespace MGraph
{
    /// <summary>
    /// Hierarchical graph (e.g. Tree).
    /// Mutable by default.
    /// </summary>
    public interface IHierarchy<TVertex, TEdge> 
        : IMutableGraph<TVertex, TEdge>, IMutableVertexAndEdgeSet<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TVertex : INode
    {
        TVertex Root { get; }
        IEnumerable<TEdge> ChildrenEdges(TVertex v);
        IEnumerable<TVertex> Children(TVertex v);
    }
}
