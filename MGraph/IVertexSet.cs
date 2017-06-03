using System.Collections.Generic;

namespace MGraph
{
    /// <summary>
    /// Immutable vertex set.
    /// </summary>
    public interface IVertexSet<TVertex>
        where TVertex : IVertex
    {
        int VertexCount { get; }
        IEnumerable<TVertex> Vertices { get; }
        bool IsVerticesEmpty { get; }
    }
}
