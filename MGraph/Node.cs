namespace MGraph
{
    /// <summary>
    /// Represents a tree node.
    /// </summary>
    public class Node : INode
    {
        protected Label _label;
        protected bool _isRoot;

        protected INode parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MGraph.Node"/> class.
        /// Implying the current node is a root.
        /// </summary>
        /// <param name="text">Text.</param>
        public Node(string text)
        {
            this._isRoot = true;
            parent = null;
            _label = new Label(text);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MGraph.Node"/> class.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="par">Parent.</param>
        public Node(string text, INode par)
        {
            this._isRoot = false;
            this.parent = par;
            _label = new Label(text);
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public INode Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        /// <summary>
        /// Show if current node is root or not.
        /// </summary>
        /// <returns><c>true</c>, if is root, <c>false</c> otherwise.</returns>
        public bool IsRoot()
        {
            return _isRoot;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:MGraph.Node"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:MGraph.Node"/>.</returns>
        public override string ToString()
        {
            if (_isRoot)
                return "root: ( " + _label.Text + " )";
            return "( " + _label.Text + " )";
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
        /// Compares two nodes.
        /// </summary>
        /// <returns>The comparison result.</returns>
        /// <param name="other">Other.</param>
        public int CompareTo(IVertex other)
        {
            return this.label.Text.CompareTo(other.label.Text);
        }
    }
}
