using System.Collections.Generic;

namespace MGraph
{
    /// <summary>
    /// Mutable undirected graph.
    /// </summary>
	public interface IMutableUndirectedGraph<TVertex, TEdge>
		: IMutableGraph<TVertex, TEdge>, IMutableVertexAndEdgeSet<TVertex, TEdge>
		where TEdge : IEdge<TVertex>
		where TVertex : IVertex
    {
		IEnumerable<TEdge> AdjacentEdges(TVertex vertex);
		int Degree(TVertex vertex);
    }
}
