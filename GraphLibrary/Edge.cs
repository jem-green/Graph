using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLibrary
{
    public class Edge<TVertex> : IEdge<TVertex>
    {
        #region Fields

        private bool _visited = false;
        int _weight = 0;
        Vertex<TVertex> _from;
        Vertex<TVertex> _to;

        #endregion
        #region Constructor

        public Edge(Vertex<TVertex> from, Vertex<TVertex> to)
        {
            _from = from;
            _to = to;
        }

        #endregion
        #region Properties

        public Vertex<TVertex> From
        {
            set
            {
                _from = value;
            }
            get
            {
                return (_from);
            }
        }

        public Vertex<TVertex> To
        {
            set
            {
                _to = value;
            }
            get
            {
                return (_to);
            }
        }

        public bool Visited
        {
            set
            {
                _visited = value;
            }
            get
            {
                return (_visited);
            }
        }

        public int Weight
        {
            set
            {
                _weight = value;
            }
            get
            {
                return (_weight);
            }
        }

        #endregion

    }
}
