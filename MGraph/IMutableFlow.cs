namespace MGraph
{
    /// <summary>
    /// Mutable flow.
    /// </summary>
	public interface IMutableFlow<TVertex, TEdge> : IMutableDirectedGraph<TVertex, TEdge>
		where TEdge : IEdge<TVertex>
		where TVertex : IVertex
	{
		TVertex Source { get; }
		TVertex Sink { get; }
	}
}
