using System;

namespace MGraph
{
    /// <summary>
    /// Basic vertex class.
    /// </summary>
    public class SimpleVertex : IVertex
    {
        protected Label _label;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MGraph.SimpleVertex"/> class.
        /// </summary>
        /// <param name="text">Label text.</param>
        public SimpleVertex(string text)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException("Vertex label cannot be empty!");

            _label = new Label(text);
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
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:MGraph.SimpleVertex"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:MGraph.SimpleVertex"/>.</returns>
        public override string ToString()
        {
            return "( " + _label.Text + " )";
        }

        /// <summary>
        /// Compares labels of 2 verices.
        /// </summary>
        /// <returns>The result of comparison</returns>
        /// <param name="other">Other vertex.</param>
        public int CompareTo(IVertex other)
        {
            return this.label.Text.CompareTo(other.label.Text);
        }
    }
}
