using System.Collections.Generic;

namespace MGraph
{
    /// <summary>
    /// Represents an immutable directed graph.
    /// </summary>
    public interface IDirectedGraph<TVertex, TEdge> : IGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TVertex : IVertex
    {
        IEnumerable<TEdge> InEdges(TVertex vertex);
        IEnumerable<TEdge> OutEdges(TVertex vertex);
        int Degree(TVertex vertex);
        int InDegree(TVertex vertex);
        int OutDegree(TVertex vertex);
    }
}
