using System.Collections.Generic;

namespace MGraph
{
    /// <summary>
    /// Represents a mutable undirected graph.
    /// </summary>
    public class UndirectedGraph<TVertex, TEdge> : IMutableUndirectedGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TVertex : IVertex
    {
        protected Label _label;
        protected Dictionary<TVertex, List<TEdge>> adjacentEdges;
        protected int edgeCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MGraph.UndirectedGraph`2"/> class.
        /// </summary>
        public UndirectedGraph() : this(0) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MGraph.UndirectedGraph`2"/> class.
        /// </summary>
        /// <param name="edgesCount">Edges count.</param>
        public UndirectedGraph(int edgesCount)
        {
            this.edgeCount = edgesCount;
            _label = new Label();
            adjacentEdges = new Dictionary<TVertex, List<TEdge>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MGraph.UndirectedGraph`2"/> class.
        /// </summary>
        /// <param name="vertices">Set of Vertices.</param>
        public UndirectedGraph(IEnumerable<TVertex> vertices) 
        {
            this.edgeCount = 0;
            _label = new Label();
            adjacentEdges = new Dictionary<TVertex, List<TEdge>>();
            foreach (var v in vertices)
                adjacentEdges.Add(v, new List<TEdge>());

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MGraph.UndirectedGraph`2"/> class.
        /// </summary>
        /// <param name="text">Label text.</param>
        public UndirectedGraph(string text) 
        {
            this.edgeCount = 0;
            _label = new Label(text);
            adjacentEdges = new Dictionary<TVertex, List<TEdge>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MGraph.UndirectedGraph`2"/> class.
        /// </summary>
        /// <param name="lbl">Graph Label.</param>
        public UndirectedGraph(Label lbl) 
        {
            this.edgeCount = 0;
            _label = lbl;
            adjacentEdges = new Dictionary<TVertex, List<TEdge>>();
        }

        #region IUndirectedGraph
        /// <summary>
        /// Returns adjacent edges of a vertex.
        /// </summary>
        /// <returns>The set of edges.</returns>
        /// <param name="vertex"></param>
        public IEnumerable<TEdge> AdjacentEdges(TVertex vertex)
        {
            return this.adjacentEdges[vertex];
        }

        /// <summary>
        /// Returns degree of the specified vertex.
        /// </summary>
        /// <returns>The degree.</returns>
        /// <param name="vertex"></param>
        public int Degree(TVertex vertex)
        {
            return this.adjacentEdges[vertex].Count;
        }
        #endregion

        #region IGraph
        /// <summary>
        /// Indicates whether this <see cref="T:MGraph.UndirectedGraph`2"/> is directed.
        /// </summary>
        /// <value><c>false</c> in all cases.</value>
        public bool IsDirected
        {
            get { return false; }
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

        #region IMutableVertexAndEdgeSet
        /// <summary>
        /// Adds the vertex to a graph.
        /// </summary>
        /// <returns><c>true</c>, if vertex was added, <c>false</c> otherwise.</returns>
        /// <param name="vertex">Vertex to be added.</param>
        public bool AddVertex(TVertex vertex)
        {
            // Check if we are trying to add vertex that is already in graph
            if (ContainsVertex(vertex))
                return false;

            var edges = new List<TEdge>();
            adjacentEdges.Add(vertex, edges);
            return true;
        }

        /// <summary>
        /// Removes the vertex from a graph.
        /// </summary>
        /// <returns><c>true</c>, if vertex was removed, <c>false</c> otherwise.</returns>
        /// <param name="vertex">Vertex to be removed.</param>
        public bool RemoveVertex(TVertex vertex)
        {
            // Chech if we are trying to remove nonexisting vertex
            if (!ContainsVertex(vertex))
                return false;

            this.ClearAdjacentEdges(vertex);
            bool result = this.adjacentEdges.Remove(vertex);

            return result;
        }

        /// <summary>
        /// Returns the number of vertices.
        /// </summary>
        /// <value>The vertex count.</value>
        public int VertexCount
        {
            get { return adjacentEdges.Count; }
        }

        /// <summary>
        /// Returns the set of vertices.
        /// </summary>
        /// <value>The vertices.</value>
        public IEnumerable<TVertex> Vertices
        {
            get { return this.adjacentEdges.Keys; }
        }

        /// <summary>
        /// Indicates whether this <see cref="T:MGraph.UndirectedGraph`2"/> has no vertices.
        /// </summary>
        /// <value><c>true</c> if no vertices; otherwise, <c>false</c>.</value>
        public bool IsVerticesEmpty
        {
            get { return this.adjacentEdges.Count == 0; }
        }

        /// <summary>
        /// Adds the edge to a graph.
        /// </summary>
        /// <returns><c>true</c>, if edge was added, <c>false</c> otherwise.</returns>
        /// <param name="edge">Edge to be added.</param>
        public bool AddEdge(TEdge edge)
        {
            // We do not accept parallel edges
            if (ContainsEdge(edge))
                return false;

            var sourceEdges = this.adjacentEdges[edge.source];
            sourceEdges.Add(edge);
            var targetEdges = this.adjacentEdges[edge.target];
            targetEdges.Add(edge);
            this.edgeCount++;
            return true;
        }

        /// <summary>
        /// Removes the edge from a graph.
        /// </summary>
        /// <returns><c>true</c>, if edge was removed, <c>false</c> otherwise.</returns>
        /// <param name="edge">Edge to be removed.</param>
        public bool RemoveEdge(TEdge edge)
        {
            // Check if we are trying to remove nonexisting edge
            if (!ContainsEdge(edge))
                return false;

            bool removed = this.adjacentEdges[edge.source].Remove(edge);
            if (removed)
            {
                this.adjacentEdges[edge.target].Remove(edge);
                this.edgeCount--;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Returns the number of edges.
        /// </summary>
        /// <value>The edge count.</value>
        public int EdgeCount
        {
            get { return this.edgeCount; }
        }

        /// <summary>
        /// Returnts the set of edges.
        /// </summary>
        /// <value>The edges.</value>
        public IEnumerable<TEdge> Edges
        {
            get 
            {
                List<TEdge> list = new List<TEdge>();
                foreach (var edges in adjacentEdges.Values)
                    foreach (var e in edges)
                    {
                        if (!list.Contains(e))
                            list.Add(e);
                    }
                return list;
            }
        }

        /// <summary>
        /// Indicates whether this <see cref="T:MGraph.UndirectedGraph`2"/> has no edges.
        /// </summary>
        /// <value><c>true</c> if there isn't any edges; otherwise, <c>false</c>.</value>
        public bool IsEdgesEmpty
        {
            get { return this.edgeCount == 0; }
        }

        /// <summary>
        /// Checks if there is specific edge in a graph.
        /// </summary>
        /// <returns><c>true</c>, if there is, <c>false</c> otherwise.</returns>
        /// <param name="edge">Edge.</param>
        public bool ContainsEdge(TEdge edge)
        {
            foreach (var e in adjacentEdges[edge.source])
                if (CompareEdges(edge, e.source, e.target))
                    return true;
            return false;
        }
        #endregion

        #region IMutableGraph
        /// <summary>
        /// Clears the graph.
        /// </summary>
        /// <returns><c>true</c> if the graph was successfully cleared.</returns>
        public bool Clear()
        {
            this.adjacentEdges.Clear();
            this.edgeCount = 0;
            _label.Text = "";
            return true;
        }
        #endregion

        /// <summary>
        /// Checks if graph contains the vertex.
        /// </summary>
        /// <returns><c>true</c>, if vertex is in graph, <c>false</c> otherwise.</returns>
        /// <param name="v">Vertex to be checked.</param>
        public virtual bool ContainsVertex(TVertex v)
        {
            return this.adjacentEdges.ContainsKey(v);
        }

        /// <summary>
        /// Compares the edges.
        /// </summary>
        /// <returns><c>true</c>, if edges are the same, <c>false</c> otherwise.</returns>
        /// <param name="edge">Edge.</param>
        /// <param name="source">Source.</param>
        /// <param name="target">Target.</param>
        public virtual bool CompareEdges(TEdge edge, TVertex source, TVertex target)
        {
            return (edge.source.CompareTo(source) == 0 && edge.target.CompareTo(target) == 0) ||
                (edge.source.CompareTo(target) == 0 && edge.target.CompareTo(source) == 0);
        }

        /// <summary>
        /// Clears the adjacent edges of specific vertex.
        /// </summary>
        /// <param name="v">Vertex.</param>
        public void ClearAdjacentEdges(TVertex v)
        {
            var edges = this.adjacentEdges[v].ToArray();
            this.edgeCount -= edges.Length;

            foreach (var edge in edges)
            {
                List<TEdge> aEdges;
                if (this.adjacentEdges.TryGetValue(edge.target, out aEdges))
                    aEdges.Remove(edge);
                if (this.adjacentEdges.TryGetValue(edge.source, out aEdges))
                    aEdges.Remove(edge);
            }
        }
    }
}
