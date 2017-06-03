using System;
using System.Collections.Generic;

namespace MGraph
{
    /// <summary>
    /// Represents a tree structure. Should be initialized with root.
    /// </summary>
    public class Tree<TVertex, TEdge> : IHierarchy<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TVertex : INode
    {
        protected Label _label;
        protected TVertex root;
        protected readonly bool directed;
        protected Dictionary<TVertex, List<TEdge>> childrenEdges;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MGraph.Tree`2"/> class.
        /// </summary>
        /// <param name="root">Root of tree.</param>
        public Tree(TVertex root) : this(root, false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MGraph.Tree`2"/> class.
        /// </summary>
        /// <param name="root">Root of tree.</param>
        /// <param name="directed">If set to <c>true</c> edges are directed.</param>
        public Tree(TVertex root, bool directed) : this(root, directed, "") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MGraph.Tree`2"/> class.
        /// </summary>
        /// <param name="root">Root of tree.</param>
        /// <param name="directed">If set to <c>true</c> edges are directed.</param>
        /// <param name="text">Label text.</param>
        public Tree(TVertex root, bool directed, string text) : this(root, directed, new Label(text)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MGraph.Tree`2"/> class.
        /// </summary>
        /// <param name="root">Root of tree.</param>
        /// <param name="directed">If set to <c>true</c> edges are directed.</param>
        /// <param name="lbl">Label of tree.</param>
        public Tree(TVertex root, bool directed, Label lbl)
        {
            this.directed = directed;
            this.root = root;
            _label = lbl;
            childrenEdges = new Dictionary<TVertex, List<TEdge>>
            {
                { root, new List<TEdge>() }
            };
        }

        /// <summary>
        /// Check if the given vertex is a part of tree.
        /// </summary>
        /// <returns><c>true</c>, if it is, <c>false</c> otherwise.</returns>
        /// <param name="v">Vertex.</param>
        public bool ContainsVertex(TVertex v)
        {
            return childrenEdges.ContainsKey(v);
        }

        /// <summary>
        /// Check if the tree contains the given edge.
        /// </summary>
        /// <returns><c>true</c>, if it does, <c>false</c> otherwise.</returns>
        /// <param name="edge">Edge to check.</param>
        public bool ContainsEdge(TEdge edge)
        {
            List<TEdge> outEdges;
            return this.childrenEdges.TryGetValue(edge.source, out outEdges) &&
                outEdges.Contains(edge);
        }

        /// <summary>
        /// Returns the number of nodes.
        /// </summary>
        /// <value>The vertex count.</value>
        public int VertexCount
        {
            get { return childrenEdges.Keys.Count; }
        }

        /// <summary>
        /// Indicates whether this <see cref="T:MGraph.Tree`2"/> has no vertices.
        /// </summary>
        /// <value><c>true</c> if there are no vertices; otherwise, <c>false</c>.</value>
        public bool IsVerticesEmpty
        {
            get { return childrenEdges.Keys.Count == 0; }
        }

        /// <summary>
        /// Returns the number of edges.
        /// </summary>
        /// <value>The edge count.</value>
        public int EdgeCount
        {
            get { return childrenEdges.Values.Count; }
        }

        /// <summary>
        /// Indicates whether this <see cref="T:MGraph.Tree`2"/> has no edges.
        /// </summary>
        /// <value><c>true</c> if there are no edges; otherwise, <c>false</c>.</value>
        public bool IsEdgesEmpty
        {
            get { return EdgeCount == 0; }
        }

        /// <summary>
        /// Clear this instance.
        /// </summary>
        /// <returns>The clear.</returns>
        public bool Clear()
        {
            childrenEdges.Clear();
            root = default(TVertex);
            _label.Text = "";
            return true;
        }

        /// <summary>
        /// Returns children edges of a vertex.
        /// </summary>
        /// <returns>The set of edges.</returns>
        /// <param name="v">Vertex.</param>
        public IEnumerable<TEdge> ChildrenEdges(TVertex v)
        {
            return childrenEdges[v];
        }

        /// <summary>
        /// Return children of specific node.
        /// </summary>
        /// <returns>The children.</returns>
        /// <param name="v">Node</param>
        public IEnumerable<TVertex> Children(TVertex v)
        {
			foreach (var edge in childrenEdges[v])
				yield return edge.target;
        }

        /// <summary>
        /// Adds the vertex to a tree.
        /// </summary>
        /// <returns><c>true</c>, if vertex was added, <c>false</c> otherwise.</returns>
        /// <param name="vertex">Vertex to add.</param>
        public bool AddVertex(TVertex vertex)
        {
            // Check if we are trying to add root or
            if (ContainsVertex(vertex) || vertex.IsRoot())
                return false;
            if (!ContainsVertex((TVertex)vertex.Parent))
                return false;
            
            TEdge parentEdge = (TEdge)Activator.CreateInstance(typeof(TEdge), (TVertex)vertex.Parent, vertex);
            childrenEdges[(TVertex)vertex.Parent].Add(parentEdge);
            childrenEdges.Add(vertex,new List<TEdge>());
            return true;
        }

        /// <summary>
        /// Removes the vertex from a tree.
        /// </summary>
        /// <returns><c>true</c>, if vertex was removed, <c>false</c> otherwise.</returns>
        /// <param name="vertex">Vertex to be removed.</param>
        public bool RemoveVertex(TVertex vertex)
        {
            if (!ContainsVertex(vertex))
				return false;
            
            if (vertex.IsRoot())
                return Clear();
            
            foreach(var node in Children(vertex))
            {
                node.Parent = vertex.Parent;
                childrenEdges[(TVertex)node.Parent].Add((TEdge)Activator.CreateInstance(typeof(TEdge), (TVertex)node.Parent, node));
            }
            var EdgeToRemove = default(TEdge);
            foreach (var e in childrenEdges[(TVertex)vertex.Parent])
				if (e.source.CompareTo(vertex.Parent) == 0 && e.target.CompareTo(vertex) == 0)
				{
					EdgeToRemove = e;
					break;
				}

            RemoveEdge(EdgeToRemove);
            childrenEdges.Remove(vertex);
            return true;
        }

        /// <summary>
        /// Adding new edges is restricted as all adding logic is implemented in AddVertex().
        /// </summary>
        /// <returns><c>true</c>, if edge was added, <c>false</c> otherwise.</returns>
        /// <param name="edge">Edge.</param>
        public bool AddEdge(TEdge edge)
        {
            return false;
        }

        /// <summary>
        /// Removes the edge from a tree.
        /// Should only be used in RemoveVertex() method. Otherwise the tree structure can be damaged.
        /// </summary>
        /// <returns><c>true</c>, if edge was removed, <c>false</c> otherwise.</returns>
        /// <param name="edge">Edge.</param>
        public bool RemoveEdge(TEdge edge)
        {
            if (!ContainsEdge(edge))
                return false;

            childrenEdges[edge.source].Remove(edge);
            return true;
        }

        /// <summary>
        /// Indicates whether this <see cref="T:MGraph.Tree`2"/> has directed edges.
        /// </summary>
        /// <value><c>true</c> if it has; otherwise, <c>false</c>.</value>
        public bool IsDirected
        {
            get { return directed; }
        }

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

        /// <summary>
        /// Returns the root.
        /// </summary>
        /// <value>The root.</value>
        public TVertex Root
        {
            get
            {
                return root;
            }
        }

        /// <summary>
        /// Returns the set of vertices.
        /// </summary>
        /// <value>The vertices.</value>
        public IEnumerable<TVertex> Vertices
        {
			get
			{
				var list = new List<TVertex>();
				foreach (var n in childrenEdges.Keys)
				{
					if (!list.Contains(n))
						list.Add(n);
				}
				return list;
			}
			
        }


        /// <summary>
        /// Returns the set of edges.
        /// </summary>
        /// <value>The edges.</value>
        public IEnumerable<TEdge> Edges
        {
			get
			{
				foreach (var eds in childrenEdges.Values)
					foreach (var ed in eds)
						yield return ed;
			}
        }
    }
}
