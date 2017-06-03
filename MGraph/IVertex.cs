using System;

namespace MGraph
{
    /// <summary>
    /// Vertex main interface.
    /// </summary>
    public interface IVertex : ILabledObject, IComparable<IVertex>
    {
    }
}
