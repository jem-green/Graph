using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace GraphLibrary
{
    public class Vertex<TVertex> : IVertex<TVertex>, IComparable<Vertex<TVertex>>
    {
        #region Fields

        private bool _visited = false;
        private TVertex _data = default(TVertex);
        private List<Edge<TVertex>> _edges;

        #endregion
        #region Constructor

        public Vertex(TVertex data)
        {
            _data = data;
            _edges = new List<Edge<TVertex>>();
        }

        #endregion
        #region Properties

        public TVertex Data
        {
            set
            {
                _data = value;
            }
            get
            {
                return (_data);
            }
        }

        public int Degree
        {
            get
            {
                return (_edges.Count);
            }
        }

        public List<Edge<TVertex>> Edges
        {
            get
            {
                return (_edges);
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

        public int Compare([AllowNull] Vertex<TVertex> x, [AllowNull] Vertex<TVertex> y)
        {
            IComparable obj1 = (IComparable)x.Data;
            IComparable obj2 = (IComparable)y.Data;
            return (obj1.CompareTo(obj2));
        }

        #endregion
        #region Methods

        public void AddEdge(Edge<TVertex> edge)
        {
            _edges.Add(edge);
        }

        public override bool Equals(object obj)
        {
            TVertex vertex = (TVertex)obj;
            if (_data.Equals(vertex) != true)
            {
                return (false);
            }
            else
            {
                return (true);
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_data);
        }

        public int CompareTo([AllowNull] Vertex<TVertex> other)
        {
            IComparable obj1 = (IComparable)this.Data;
            IComparable obj2 = (IComparable)other.Data;
            return (obj1.CompareTo(obj2));
        }

        #endregion

    }
}
