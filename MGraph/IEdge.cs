namespace MGraph
{
    /// <summary>
    /// Edge main interface.
    /// </summary>
    public interface IEdge<TVertex> : ILabledObject
    {
        TVertex source
        {
            get;
        }

        TVertex target
        {
            get;
        }
    }
}
