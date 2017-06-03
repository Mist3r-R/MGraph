namespace MGraph
{
    /// <summary>
    /// Main Graph interface.
    /// Contains the info if graph is directed or not.
    /// </summary>
    public interface IGraph<TVertex, TEdge> : ILabledObject
        where TEdge : IEdge<TVertex>
        where TVertex : IVertex
    {
        bool IsDirected
        {
            get;
        }
    }
}
