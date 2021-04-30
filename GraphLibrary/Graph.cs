using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLibrary
{

    public class Graph<TEdge,TVertex>
    {
        #region Fields
        
        List<Edge<TVertex>> _edges;
        HashSet<Vertex<TVertex>> _vertices;
        
        #endregion
        #region Constructors
        public Graph()
        {
            _edges = new List<Edge<TVertex>>();
            _vertices = new HashSet<Vertex<TVertex>>();
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

        public HashSet<Vertex<TVertex>> Vertices
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

        public void AddEdge(Edge<TVertex> edge)
        {
            edge.From.AddEdge(edge);
            edge.To.AddEdge(edge);
            _edges.Add(edge);
            _vertices.Add(edge.From);
            _vertices.Add(edge.To);
        }

        public void AddEdge(Vertex<TVertex> from, Vertex<TVertex> to)
        {
            Edge<TVertex> edge = new Edge<TVertex>(from, to);
            from.AddEdge(edge);
            to.AddEdge(edge);
            _edges.Add(edge);
            _vertices.Add(from);
            _vertices.Add(to);
        }

        /// <summary>
        /// Reset the visited status of the entire graph
        /// </summary>
        public void Reset()
        {
            foreach (Vertex<TVertex> anyVertex in _vertices)
            {
                anyVertex.Visited = false;
            }
        }

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

        //// Method to check if all non-zero degree vertices are
        //// connected. It mainly does DFS traversal starting from
        //bool isConnected()
        //{
        //    // Mark all the vertices as not visited
        //    bool[] visited = new bool[V];
        //    int i;
        //    for (i = 0; i < V; i++)
        //        visited[i] = false;

        //    // Find a vertex with non-zero degree
        //    for (i = 0; i < V; i++)
        //        if (adj[i].Count != 0)
        //            break;

        //    // If there are no edges in the graph, return true
        //    if (i == V)
        //        return true;

        //    // Start DFS traversal from a vertex with non-zero degree
        //    DFSUtil(i, visited);

        //    // Check if all non-zero degree vertices are visited
        //    for (i = 0; i < V; i++)
        //        if (visited[i] == false && adj[i].Count > 0)
        //            return false;

        //    return true;
        //}

        /// <summary>
        /// Identify if the graph is Eulerian
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns>
        /// The function returns one of the following values
        /// 0 --> If grpah is not Eulerian
        /// 1 --> If graph has an Euler path(Semi-Eulerian)
        /// 2 --> If graph has an Euler Circuit(Eulerian)
        /// </returns>
        public int IsEulerian(HashSet<Vertex<TVertex>> vertices)
        {
            // Count vertices with odd degree
            int odd = 0;
            foreach (Vertex<TVertex> vertex in vertices)
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
        public Vertex<TVertex> FindRoot(HashSet<Vertex<TVertex>> subGraph)
        {
            Vertex<TVertex> root = null;
            int count = 0;
            foreach(Vertex<TVertex> vertex in subGraph)
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
        /// <param name="node"></param>
        /// <returns></returns>
        private bool AllVisited(Vertex<TVertex> node)
        {
            bool visited = true;
            foreach (Edge<TVertex> edge in node.Edges)
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
        /// <param name="subGraph"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public List<Vertex<TVertex>> GetEuler1(HashSet<Vertex<TVertex>> subGraph, Vertex<TVertex> root)
        {
            //To find the Euler circuit/path and store it in finalPath arrayList

            List<Vertex<TVertex>> euler = new List<Vertex<TVertex>>();
            Stack<Vertex<TVertex>> tempPath = new Stack<Vertex<TVertex>>();
            Vertex<TVertex> current;

            //push root into the stack
            tempPath.Push(root);
            Console.WriteLine("Intial Push " + root.Data);
            while (tempPath.Count != 0) //until Stack going to empty
            {
                //get item top of the stack
                current = tempPath.Peek();
                Console.WriteLine("Peek " + current.Data);
                if (AllVisited(current))
                {
                    //If all adjacent nodes are already visited
                    //pop element from stack and store it in finalpath arrayList
                    Console.WriteLine("Pop " + tempPath.Peek().Data);
                    euler.Add(tempPath.Pop());
                }
                else
                {
                    //If any unvisited node available push that node into stack
                    //mark that edge as already visited by marking 'n' in GraphMatrix[][]
                    //break the iteration

                    foreach(Edge<TVertex> edge in current.Edges)
                    {
                        Console.WriteLine("Edge=" + edge.From.Data + " to " + edge.To.Data);
                        if (edge.Visited == false)
                        {
                            Console.WriteLine("Remove Edge " + edge.From.Data + " to " + edge.To.Data);
                            edge.Visited = true;
                            if (edge.From == current)
                            {
                                Console.WriteLine("push " + edge.To.Data);
                                tempPath.Push(edge.To);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("push " + edge.From.Data);
                                tempPath.Push(edge.From);
                                break;
                            }
                        }
                    }
                }
            }

            return (euler);
        }

        //public List<Vertex<TVertex>> GetEuler(HashSet<Vertex<TVertex>> subGraph, Vertex<TVertex> root)
        //{
        //    //To find the Euler circuit/path and store it in finalPath arrayList

        //    List<Vertex<TVertex>> euler = new List<Vertex<TVertex>>();
        //    Stack<Vertex<TVertex>> stack = new Stack<Vertex<TVertex>>();
        //    Vertex<TVertex> current = root;

        //    // Loop will run until there is element
        //    // in the stack or current edge has some
        //    // neighbour.

        //    while ((stack.Count != 0) || (AllVisited(current) == false)) //until Stack going to empty
        //    {
        //        if (AllVisited(current) == true)
        //        {
        //            // If current node has not any
        //            // neighbour add it to path and
        //            // pop stack set new current to
        //            // the popped element

        //            euler.Add(current);
        //            Console.WriteLine("Pop " + stack.Peek().Data);
        //            current = stack.Pop();
        //        }
        //        else
        //        {
        //            // If the current vertex has at
        //            // least one neighbour add the
        //            // current vertex to stack, remove
        //            // the edge between them and set the
        //            // current to its neighbour.

        //            foreach (Edge<TVertex> edge in current.Edges)
        //            {
        //                Console.WriteLine("Edge=" + edge.From.Data + " to " + edge.To.Data);
        //                if (edge.Visited == false)
        //                {
        //                    Console.WriteLine("Remove Edge " + edge.From.Data + " to " + edge.To.Data);
        //                    edge.Visited = true;
        //                    if (edge.From == current)
        //                    {
        //                        Console.WriteLine("push " + current.Data);
        //                        stack.Push(current);
        //                        current = edge.To;
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("push " + current.Data);
        //                        stack.Push(current);
        //                        current = edge.From;
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    // Some odd fix is needed here
        //    euler.Add(current);
        //    return (euler);
        //}

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
