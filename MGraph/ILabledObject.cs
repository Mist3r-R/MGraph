using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGraph
{
    /// <summary>
    /// Describes object with label.
    /// </summary>
    public interface ILabledObject
    {
        Label label
        {
            get;
            set;
        }
    }
}
