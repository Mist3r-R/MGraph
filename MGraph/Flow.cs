namespace MGraph
{
    /// <summary>
    /// Represents a mutable Flow.
    /// </summary>
    public class Flow<TVertex, TEdge> :  DirectedGraph<TVertex, TEdge>, IMutableFlow<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TVertex : IVertex
    {
        TVertex sink;
        TVertex source;

        /// <summary>
        /// Finds the sink.
        /// </summary>
        void FindSink()
        {
            foreach (var v in vertexOutEdges.Keys)
                if (vertexOutEdges[v].Count == 0)
                    sink = v;
        }

        /// <summary>
        /// Finds the source.
        /// </summary>
        void FindSource()
        {
            foreach (var v in vertexInEdges.Keys)
                if (vertexInEdges[v].Count == 0)
                    source = v;
        }

        /// <summary>
        /// Returns the sink.
        /// </summary>
        /// <value>The sink.</value>
        public TVertex Sink
        {
            get 
            {
                if (sink == null)
                    FindSink();
                return sink;
            }
        }

        /// <summary>
        /// Returns the source.
        /// </summary>
        /// <value>The source.</value>
        public TVertex Source
        {
            get
            {
                if (source == null)
                    FindSource();
                return source;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MGraph.Flow`2"/> class.
        /// </summary>
        /// <param name="src">Source.</param>
        /// <param name="snk">Sink.</param>
        public Flow(TVertex src, TVertex snk)
        {
            source = src;
            AddVertex(source);
            sink = snk;
            AddVertex(sink);
        }

        /// <summary>
        /// Removes the vertex considering it is neither sink nor source.
        /// </summary>
        /// <returns><c>true</c>, if vertex was removed, <c>false</c> otherwise.</returns>
        /// <param name="vertex">Vertex.</param>
        public override bool RemoveVertex(TVertex vertex)
        {
            if (sink.CompareTo(vertex) != 0 && source.CompareTo(vertex) != 0)
                return base.RemoveVertex(vertex);
            else 
                return false;
        }
    }
}
