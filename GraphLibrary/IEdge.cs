using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLibrary
{
    interface IEdge<TVertex>
    {
        public bool Visited { set; get; }
        public Vertex<TVertex> From { get; set; }
        public Vertex<TVertex> To { get; set; }
    }
}
