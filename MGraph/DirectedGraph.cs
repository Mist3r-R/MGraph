using System.Collections.Generic;

namespace MGraph
{
    /// <summary>
    /// Represents mutable directed graph. 
    /// </summary>
    public class DirectedGraph<TVertex, TEdge> : 
        IMutableDirectedGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TVertex : IVertex
    {
        protected Label _label;
        protected Dictionary<TVertex, List<TEdge>> vertexInEdges;
        protected Dictionary<TVertex, List<TEdge>> vertexOutEdges;
        protected int edgeCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MGraph.DirectedGraph`2"/> class.
        /// </summary>
        public DirectedGraph() : this(0) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MGraph.DirectedGraph`2"/> class.
        /// </summary>
        /// <param name="edges">Edge count.</param>
        public DirectedGraph(int edges)
        {
            edgeCount = edges;
            vertexInEdges = new Dictionary<TVertex, List<TEdge>>();
            vertexOutEdges = new Dictionary<TVertex, List<TEdge>>();
            _label = new Label();
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:MGraph.DirectedGraph`2"/> class.
		/// </summary>
		/// <param name="vertices">Set of vertices.</param>
		public DirectedGraph(IEnumerable<TVertex> vertices)
        {
            vertexInEdges = new Dictionary<TVertex, List<TEdge>>();
            vertexOutEdges = new Dictionary<TVertex, List<TEdge>>();
            foreach (var v in vertices)
            {
                vertexInEdges.Add(v, new List<TEdge>());
                vertexOutEdges.Add(v, new List<TEdge>());
            }
            _label = new Label();
            edgeCount = 0;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:MGraph.DirectedGraph`2"/> class.
		/// </summary>
		/// <param name="lbl">Graph label</param>
		public DirectedGraph(Label lbl)
        {
            vertexInEdges = new Dictionary<TVertex, List<TEdge>>();
            vertexOutEdges = new Dictionary<TVertex, List<TEdge>>();
            _label = lbl;
            edgeCount = 0;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:MGraph.DirectedGraph`2"/> class.
		/// </summary>
		/// <param name="text">Text of label.</param>
		public DirectedGraph(string text)
        {
            vertexInEdges = new Dictionary<TVertex, List<TEdge>>();
            vertexOutEdges = new Dictionary<TVertex, List<TEdge>>();
            _label = new Label(text);
            edgeCount = 0;
        }

        #region IGraph
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:MGraph.DirectedGraph`2"/> is directed.
        /// </summary>
        /// <value><c>true</c> in all cases.</value>
        public bool IsDirected
        {
            get { return true; }
        }
        #endregion

        #region IMutableDirectedGraph
        /// <summary>
        /// Returns in edges of an asked vertex.
        /// </summary>
        /// <returns>The set of edges.</returns>
        /// <param name="vertex"></param>
        public IEnumerable<TEdge> InEdges(TVertex vertex)
        {
            return vertexInEdges[vertex];
        }

        /// <summary>
        /// Returns out edges of an asked vertex.
        /// </summary>
        /// <returns>The set of edges.</returns>
        /// <param name="vertex"></param>
        public IEnumerable<TEdge> OutEdges(TVertex vertex)
        {
            return vertexOutEdges[vertex];
        }

        /// <summary>
        /// Degree of the specified vertex.
        /// </summary>
        /// <returns>The degree.</returns>
        /// <param name="vertex"></param>
        public int Degree(TVertex vertex)
        {
            return this.InDegree(vertex) + this.OutDegree(vertex);
        }

		/// <summary>
		/// In degree of the specified vertex.
		/// </summary>
		/// <returns>The in degree.</returns>
		/// <param name="vertex"></param>
		public int InDegree(TVertex vertex)
        {
            return vertexInEdges[vertex].Count;
        }

		/// <summary>
		/// Out degree of the specified vertex.
		/// </summary>
		/// <returns>The out degree.</returns>
		/// <param name="vertex"></param>
		public int OutDegree(TVertex vertex)
        {
            return vertexOutEdges[vertex].Count;
        }
        #endregion

        #region ILabeledOgject
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        public Label label
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
            }
        }
        #endregion

        #region IMutableGraph
        /// <summary>
        /// Clears this graph.
        /// </summary>
        /// <returns>True in case of successfull clear.</returns>
        public bool Clear()
        {
            this.vertexInEdges.Clear();
            this.vertexOutEdges.Clear();
            _label.Text = "";
            edgeCount = 0;
            return true;
        }
        #endregion

        #region IMutableVertexAndEdgeSet
        /// <summary>
        /// Adds the vertex to a graph.
        /// </summary>
        /// <returns><c>true</c>, if vertex was added, <c>false</c> otherwise.</returns>
        /// <param name="vertex">Vertex to be added.</param>
        public virtual bool AddVertex(TVertex vertex)
        {
            //Check if we are trying to add the vertex that is already in graph
            if (this.ContainsVertex(vertex))
                return false;

            vertexInEdges.Add(vertex, new List<TEdge>());
            vertexOutEdges.Add(vertex, new List<TEdge>());
            return true;
        }

        /// <summary>
        /// Removes the vertex from a graph.
        /// </summary>
        /// <returns><c>true</c>, if vertex was removed, <c>false</c> otherwise.</returns>
        /// <param name="vertex">Vertex to be removed.</param>
        public virtual bool RemoveVertex(TVertex vertex)
        {
            // Check if we are trying to remove nonexistent vertex
            if (!this.ContainsVertex(vertex))
                return false;

            // Collect edges to remove
            var edgesToRemove = new List<TEdge>();
            foreach (var outEdge in this.OutEdges(vertex))
            {
                this.vertexInEdges[outEdge.target].Remove(outEdge);
                edgesToRemove.Add(outEdge);
            }
            foreach (var inEdge in this.InEdges(vertex))
            {
                // Might already have been removed
                if (this.vertexOutEdges[inEdge.source].Remove(inEdge))
                    edgesToRemove.Add(inEdge);
            }

            this.vertexOutEdges.Remove(vertex);
            this.vertexInEdges.Remove(vertex);
            this.edgeCount -= edgesToRemove.Count;

            return true;
        }

        /// <summary>
        /// Returns the vertex count.
        /// </summary>
        /// <value>The vertex count.</value>
        public int VertexCount
        {
            get { return vertexOutEdges.Count; }
        }

        /// <summary>
        /// Returns the set of vertices.
        /// </summary>
        /// <value>The vertices.</value>
        public virtual IEnumerable<TVertex> Vertices
        {
            get { return vertexOutEdges.Keys; }
        }

        /// <summary>
        /// Returns a value indicating whether this <see cref="T:MGraph.DirectedGraph`2"/> has no vertices.
        /// </summary>
        /// <value><c>true</c> if no vertices; otherwise, <c>false</c>.</value>
        public virtual bool IsVerticesEmpty
        {
            get { return vertexOutEdges.Count == 0; }
        }

        /// <summary>
        /// Adds the edge to a graph.
        /// </summary>
        /// <returns><c>true</c>, if edge was added, <c>false</c> otherwise.</returns>
        /// <param name="edge">Edge to be added.</param>
        public virtual bool AddEdge(TEdge edge)
        {
            // We do not accept parallel edges
            if (this.ContainsEdge(edge))
                return false;

            this.vertexOutEdges[edge.source].Add(edge);
            this.vertexInEdges[edge.target].Add(edge);
            this.edgeCount++;
            return true;
        }

        /// <summary>
        /// Removes the edge from a graph.
        /// </summary>
        /// <returns><c>true</c>, if edge was removed, <c>false</c> otherwise.</returns>
        /// <param name="edge">Edge to be removed.</param>
        public virtual bool RemoveEdge(TEdge edge)
        {
            // Check if we are trying to remove nonexisting edge
            if (!this.ContainsEdge(edge))
                return false;
            
            if (this.vertexOutEdges[edge.source].Remove(edge))
            {
                this.vertexInEdges[edge.target].Remove(edge);
                this.edgeCount--;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the edge count.
        /// </summary>
        /// <value>The edge count.</value>
        public int EdgeCount
        {
            get { return edgeCount; }
        }

        public virtual IEnumerable<TEdge> Edges
        {
            get
            {
                foreach (var edges in vertexOutEdges.Values)
                    foreach (var edge in edges)
                        yield return edge;
            }
        }

        /// <summary>
        /// Checks if the edge exists.
        /// </summary>
        /// <returns><c>true</c>, if edge exists, <c>false</c> otherwise.</returns>
        /// <param name="edge">Edge to be checked.</param>
        public virtual bool ContainsEdge(TEdge edge)
        {
            List<TEdge> outEdges;
            return this.vertexOutEdges.TryGetValue(edge.source, out outEdges) &&
                outEdges.Contains(edge);
        }

        /// <summary>
        /// Indicates whether this <see cref="T:MGraph.DirectedGraph`2"/> has no edges.
        /// </summary>
        /// <value><c>true</c> if no edges; otherwise, <c>false</c>.</value>
        public bool IsEdgesEmpty
        {
            get { return edgeCount == 0; }
        }
        #endregion

        /// <summary>
        /// Checks if the vertex is in graph.
        /// </summary>
        /// <returns><c>true</c>, if vertex exists, <c>false</c> otherwise.</returns>
        /// <param name="vertex">Vertex to be checked.</param>
        public virtual bool ContainsVertex(TVertex vertex)
        {
            return vertexOutEdges.ContainsKey(vertex);
        }

    }
}
