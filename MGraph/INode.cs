namespace MGraph
{
    /// <summary>
    /// Vertex that contains information about it's parent.
    /// Can be used in hierarchical graphs (e.g. tree).
    /// </summary>
    public interface INode : IVertex
    {
        INode Parent { get; set; }
        bool IsRoot();
    }
}
