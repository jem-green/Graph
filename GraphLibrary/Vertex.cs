using System;
using System.Collections.Generic;

namespace GraphLibrary
{
    public class Vertex<TVertex> : IVertex<TVertex>
    {
        #region Fields

        private bool _visited = false;
        private TVertex _vertex;
        private List<Edge<TVertex>> _edges;

        #endregion
        #region Constructor

        public Vertex(TVertex vertex)
        {
            _vertex = vertex;
            _edges = new List<Edge<TVertex>>();
        }

        #endregion
        #region Properties

        public TVertex Data
        {
            set
            {
                _vertex = value;
            }
            get
            {
                return (_vertex);
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

        #endregion
        #region Methods

        public void AddEdge(Edge<TVertex> edge)
        {
            _edges.Add(edge);
        }

        #endregion

    }
}
