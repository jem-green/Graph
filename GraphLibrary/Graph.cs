using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLibrary
{

    public class Graph<TEdge,TVertex>
    {
        #region Fields
        
        List<Edge<TVertex>> _edges;
        List<Vertex<TVertex>> _vertices;
        
        #endregion
        #region Constructors
        public Graph()
        {
            _edges = new List<Edge<TVertex>>();
            _vertices = new List<Vertex<TVertex>>();
        }

        #endregion
        #region Properties

        public int Order
        {
            get
            {
                return (_vertices.Count);
            }
        }

        public int Size
        {
            get
            {
                return (_edges.Count);
            }
        }

        public List<Vertex<TVertex>> Vertices
        {
            get
            {
                return (_vertices);
            }
        }

        public List<Edge<TVertex>> Edges
        {
            get
            {
                return (_edges);
            }
        }

        #endregion
        #region Methods

        public void AddVertex(Vertex<TVertex> vertex)
        {
            _vertices.Add(vertex);
        }

        public Vertex<TVertex> GetVertex(TVertex data)
        {
            Vertex<TVertex> vertex = new Vertex<TVertex>(data);
            int pos = _vertices.BinarySearch(vertex);
            if (pos < 0)
            {
                _vertices.Add(vertex);
            }
            else
            {
                vertex = _vertices[pos];
            }
            return (vertex);
        }

        public void AddEdge(Edge<TVertex> edge)
        {
            edge.From.AddEdge(edge);
            edge.To.AddEdge(edge);
            _edges.Add(edge);

            // Problem here is tha the verticies
            // are added twice, for each edge added

        }

        public void AddEdge(Vertex<TVertex> from, Vertex<TVertex> to)
        {
            Edge<TVertex> edge = new Edge<TVertex>(from, to);
            from.AddEdge(edge);
            to.AddEdge(edge);
            _edges.Add(edge);

            // Problem here is tha the verticies
            // are added twice, for each edge added
        }

        /// <summary>
        /// Reset the visited status of the entire graph
        /// </summary>
        public void Reset()
        {
            ResetVerticies();
            ResetEdges();
        }

        public void ResetVerticies()
        {
            foreach (Vertex<TVertex> anyVertex in _vertices)
            {
                anyVertex.Visited = false;
            }
        }

        public void ResetVerties(List<Vertex<TVertex>> subgraph)
        {
            foreach (Vertex<TVertex> anyVertex in subgraph)
            {
                anyVertex.Visited = false;
            }
        }

        public void ResetEdges()
        {
            foreach (Edge<TVertex> anyEdge in _edges)
            {
                anyEdge.Visited = false;
            }
        }

        /// <summary>
        /// Resets the visited status of a subgraph
        /// </summary>
        /// <param name="subgraph"></param>
        public void ResetEdges(List<Vertex<TVertex>> subgraph)
        {
            foreach (Vertex<TVertex> anyVertex in subgraph)
            {
                // Only reset the connected edges within the subgraph.

                foreach(Edge<TVertex> anyEdge in anyVertex.Edges)
                {
                    anyEdge.Visited = false;
                }
            }
        }

        /// <summary>
        /// Search through the verticies to determine 
        /// which verticies are connected in the graph.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public List<Vertex<TVertex>> GetConnected(Vertex<TVertex> vertex)
        {
            // Search through the verticies to determine 
            // which verticies are connected - Depth first search.

            List<Vertex<TVertex>> connected = new List<Vertex<TVertex>>();
            if (vertex.Visited == false)
            {
                DepthFirstSearch(connected, vertex);
            }
            return (connected);
        }

        /// <summary>
        /// Search through the verticies to determine 
        /// which verticies are connected in the subgraph.
        /// </summary>
        /// <param name="subgraph"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public List<Vertex<TVertex>> GetConnected(List<Vertex<TVertex>> subgraph, Vertex<TVertex> vertex)
        {
            // Search through the verticies to determine 
            // which verticies are connected - Depth first search.

            List<Vertex<TVertex>> connected = new List<Vertex<TVertex>>();
            if (vertex.Visited == false)
            {
                DepthFirstSearch(connected, vertex);
            }
            return (connected);
        }

        /// <summary>
        /// Identify if the subgraph is Eulerian
        /// </summary>
        /// <param name="subgraph"></param>
        /// <returns>
        /// 0 --> If graph is not Eulerian
        /// 1 --> If graph has an Euler path(Semi-Eulerian)
        /// 2 --> If graph has an Euler Circuit(Eulerian)
        /// </returns>
        public int IsEulerian(List<Vertex<TVertex>> subgraph)
        {
            // Count vertices with odd degree
            int odd = 0;
            foreach (Vertex<TVertex> vertex in subgraph)
            {
                if (vertex.Degree % 2 != 0)
                {
                    odd++;
                }
            }

            // If count is more than 2, then graph is not Eulerian
            if (odd > 2)
            {
                return 0;
            }

            // If odd count is 2, then semi-eulerian.
            // If odd count is 0, then eulerian
            // Note that odd count can never be 1 for undirected graph
            return (odd == 2) ? 1 : 2;
        }

        //To assign the root of the graph
        //Condition 1: If all Nodes have even degree, there should be a euler Circuit/Cycle
        //We can start path from any node
        //Condition 2: If exactly 2 nodes have odd degree, there should be euler path.
        //We must start from node which has odd degree
        //Condition 3: If more than 2 nodes or exactly one node have odd degree, 
        //euler path/circuit not possible.

        //findRoot() will return 0 if euler path/circuit not possible
        //otherwise it will return array index of any node as root


        /// <summary>
        /// Finds the 
        /// </summary>
        /// <param name="subgraph"></param>
        /// <returns></returns>
        public Vertex<TVertex> FindStart(List<Vertex<TVertex>> subgraph)
        {
            Vertex<TVertex> root = null;
            int count = 0;
            foreach(Vertex<TVertex> vertex in subgraph)
            {
                if (vertex.Degree % 2 !=0)
                {
                    count++;
                    root = vertex;  //Store the node which has odd degree to root variable
                }
            }

            //If count is not exactly 2 then euler path/circuit not possible so return 0
            if (count != 0 && count != 2)
            {
                return (null);
            }
            else
            {
                return(root);// if exactly 2 nodes have odd degree, 
            }
            //it will return one of those node as root otherwise return 1 as root  as assumed
        }

        /// <summary>
        /// Check if all adjacent verticies are visited
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        private bool AllVisited(Vertex<TVertex> vertex)
        {
            bool visited = true;
            foreach (Edge<TVertex> edge in vertex.Edges)
            {
                if (edge.Visited == false)
                {
                    visited = false;
                    break;
                }
            }
            return (visited);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subgraph"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public List<Vertex<TVertex>> GetEuler(Vertex<TVertex> start)
        {
            //To find the Euler circuit/path and store it in euler List<>

            List<Vertex<TVertex>> euler = new List<Vertex<TVertex>>();
            Stack<Vertex<TVertex>> stack = new Stack<Vertex<TVertex>>();
            Vertex<TVertex> current;

            // Push start into the stack
            stack.Push(start);
            while (stack.Count != 0) // Keep checking while the stack is not empty
            {
                // Get item from the top of the stack
                current = stack.Peek();
                if (AllVisited(current))
                {
                    // If all adjacent edges are already visited
                    // pop the verticie from stack and add it in the list

                    euler.Add(stack.Pop());
                }
                else
                {
                    // If there are any unvisited edges available then push
                    // the end verticie onto the stack.
                    // Mark that edge as visited
                    // break the iteration

                    foreach(Edge<TVertex> edge in current.Edges)
                    {
                        if (edge.Visited == false)
                        {
                            edge.Visited = true;
                            if (edge.From == current)
                            {
                                stack.Push(edge.To);
                                break;
                            }
                            else
                            {
                                stack.Push(edge.From);
                                break;
                            }
                        }
                    }
                }
            }
            return (euler);
        }

        /// <summary>
        /// Flags the verties that are connected
        /// </summary>
        public void ConnectedComponents()
        {
            // Search through the verticies to determine 
            // which verticies are connected - Depth first search

            foreach (Vertex<TVertex> vertex in _vertices)
            {
                vertex.Visited = false;
            }

            foreach (Vertex<TVertex> vertex in _vertices)
            {
                List<Vertex<TVertex>> connected = new List<Vertex<TVertex>>();
                if (vertex.Visited == false)
                {
                    DepthFirstSearch(connected, vertex);
                }
            }
        }

        /// <summary>
        /// Recursive depth first search
        /// </summary>
        /// <param name="connected"></param>
        /// <param name="vertex"></param>
        private void DepthFirstSearch(List<Vertex<TVertex>> connected, Vertex<TVertex> vertex)
        {
            // Return a list of Vertices?

            vertex.Visited = true;
            connected.Add(vertex);

            foreach(Edge<TVertex> edge in vertex.Edges)
            {
                if (vertex == edge.From)
                {
                    if (edge.To.Visited == false)
                    {
                        DepthFirstSearch(connected, edge.To);
                    }
                }
                else
                {
                    if (edge.From.Visited == false)
                    {
                        DepthFirstSearch(connected, edge.From);
                    }
                }
            }
        }

        #endregion
    }
}
