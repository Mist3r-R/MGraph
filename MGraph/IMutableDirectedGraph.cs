using System.Collections.Generic;

namespace MGraph
{
    /// <summary>
    /// Mutable directed graph.
    /// </summary>
    public interface IMutableDirectedGraph<TVertex, TEdge> 
        : IMutableGraph<TVertex, TEdge>, IMutableVertexAndEdgeSet<TVertex, TEdge>
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
