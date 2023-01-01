using System;
using System.Collections.Generic;
using GraphLibrary;

namespace GraphTerminal
{
    class Program
    {
        static void Main(string[] args)
        {
            //BoxTest();
            DisconnectedTest();
            //ConnectedTest();
            //EulerPathTest();
            //EulerTest();
            //EulerCircuitTest();

        }
        static void BoxTest()
        {
            Console.WriteLine("Box Test");

            // need to create a list of verticies and how these are linked to edges
            // Initial method to return a list of connected verticies, given a starting vertex

            /*
             * v1  v2 
             * +---+
             * |   |
             * +---+
             * v4  v3
             * 
             */

            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);

            Graph<int, int> g = new Graph<int, int>();

            g.AddVertex(v1);
            g.AddVertex(v2);
            g.AddVertex(v3);
            g.AddVertex(v4);

            g.AddEdge(v1, v2);
            g.AddEdge(v2, v3);
            g.AddEdge(v3, v4);
            g.AddEdge(v4, v1);

            g.Reset();

            List<Vertex<int>> c = g.GetConnected(v1);

            foreach (Vertex<int> v in c)
            {
                Console.WriteLine(v.Data);
            }
        }

        static void DisconnectedTest()
        {
            Console.WriteLine("Disconnected Test");

            // need to create a list of points and how these are linked to lines
            /*
             * v1  v2 
             * +---+   + v5
             * |   |   |
             * +---+   + v6
             * v4  v3
             * 
             */

            Graph<int, int> g = new Graph<int, int>();

            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);
            Vertex<int> v5 = new Vertex<int>(5);
            Vertex<int> v6 = new Vertex<int>(6);

            g.AddVertex(v1);
            g.AddVertex(v2);
            g.AddVertex(v3);
            g.AddVertex(v4);
            g.AddVertex(v5);
            g.AddVertex(v6);

            g.AddEdge(v1, v2);
            g.AddEdge(v2, v3);
            g.AddEdge(v3, v4);
            g.AddEdge(v4, v1);
            g.AddEdge(v5, v6);

            g.Reset();

            foreach (Vertex<int> v in g.Vertices)
            {
                if (v.Visited == false)
                {
                    Console.WriteLine("Check");
                    List<Vertex<int>> c = g.GetConnected(v);

                    foreach (Vertex<int> cv in c)
                    {
                        Console.WriteLine(cv.Data);
                    }
                }
            }
        }

        /// <summary>
        /// Check if the graph is connected and Eulerian
        /// </summary>
        static void ConnectedTest()
        {
            Console.WriteLine("Connected Test");

            // need to create a list of verticies and linked to the edges
            /*
             * 
             *    v1  v2 
             *    +---+ 
             *    |   |
             * v4 +---+ v3
             *    |   |
             *    +---+
             *    v5  v6
             */

            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);
            Vertex<int> v5 = new Vertex<int>(5);
            Vertex<int> v6 = new Vertex<int>(6);

            Graph<int, int> g = new Graph<int, int>();

            g.AddVertex(v1);
            g.AddVertex(v2);
            g.AddVertex(v3);
            g.AddVertex(v4);
            g.AddVertex(v5);
            g.AddVertex(v6);

            g.AddEdge(v1, v2);
            g.AddEdge(v2, v3);
            g.AddEdge(v3, v4);
            g.AddEdge(v4, v1);
            g.AddEdge(v3, v6);
            g.AddEdge(v5, v6);
            g.AddEdge(v4, v5);

            g.Reset();

            Console.WriteLine("Eulerian=" + g.IsEulerian(g.Vertices));

            Vertex<int> rv = g.FindStart(g.Vertices);

            // Output the 

            foreach (Vertex<int> v in g.Vertices)
            {
                if (v.Visited == false)
                {
                    Console.WriteLine("Check");
                    List<Vertex<int>> c = g.GetConnected(v);

                    foreach (Vertex<int> cv in c)
                    {
                        Console.WriteLine(cv.Data);
                    }
                }
            }
        }

        /// <summary>
        /// Check if graph is Eulerian path
        /// </summary>
        static void EulerPathTest()
        {
            Console.WriteLine("Euler Path Test");

            // need to create a list of verticies and linked to the edges
            /*
             *    v1  v2 
             *    +---+ 
             *    |   |
             * v4 +---+ v3
             *    |   |
             *    +---+
             *    v5  v6
             */

            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);
            Vertex<int> v5 = new Vertex<int>(5);
            Vertex<int> v6 = new Vertex<int>(6);

            Graph<int, int> g = new Graph<int, int>();

            g.AddVertex(v1);
            g.AddVertex(v2);
            g.AddVertex(v3);
            g.AddVertex(v4);
            g.AddVertex(v5);
            g.AddVertex(v6);

            g.AddEdge(v1, v2);
            g.AddEdge(v2, v3);
            g.AddEdge(v3, v4);
            g.AddEdge(v4, v1);
            g.AddEdge(v3, v6);
            g.AddEdge(v5, v6);
            g.AddEdge(v4, v5);

            g.Reset();

            int euler = g.IsEulerian(g.Vertices);
            if (euler == 1)
            {
                Console.WriteLine("Euler Path");
                Vertex<int> rv = g.FindStart(g.Vertices);
                Console.WriteLine("Start=" + rv.Data);
                List<Vertex<int>> e = g.GetEuler(rv);

                foreach (Vertex<int> v in e)
                {
                    Console.WriteLine(v.Data);
                }
            }
            else if (euler == 2)
            {
                Console.WriteLine("Euler Circuit");

            }
            else
            {
                Console.WriteLine("Not Eulerian");
            }
        }

        /// <summary>
        /// Check if graph is Eulerian circuit
        /// </summary>
        static void EulerCircuitTest()
        {
            Console.WriteLine("Euler Circuit Test");

            // need to create a list of verticies and linked to the edges
            /*
             *    v1  v2 
             *    +---+ 
             *    |   |
             * v4 +   + v3
             *    |   |
             *    +---+
             *    v5  v6
             */

            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);
            Vertex<int> v5 = new Vertex<int>(5);
            Vertex<int> v6 = new Vertex<int>(6);

            Graph<int, int> g = new Graph<int, int>();

            g.AddVertex(v1);
            g.AddVertex(v2);
            g.AddVertex(v3);
            g.AddVertex(v4);
            g.AddVertex(v5);
            g.AddVertex(v6);

            g.AddEdge(v1, v2);
            g.AddEdge(v2, v3);
            g.AddEdge(v4, v1);
            g.AddEdge(v3, v6);
            g.AddEdge(v5, v6);
            g.AddEdge(v4, v5);

            g.Reset();

            int euler = g.IsEulerian(g.Vertices);
            if (euler == 1)
            {
                Console.WriteLine("Euler Path");
                Vertex<int> rv = g.FindStart(g.Vertices);
                Console.WriteLine("Start=" + rv.Data);
                List<Vertex<int>> e = g.GetEuler(rv);

                foreach (Vertex<int> v in e)
                {
                    Console.WriteLine("Vertex=" + v.Data);
                }
            }
            else if (euler == 2)
            {
                Console.WriteLine("Euler Circuit");
                Vertex<int> rv = g.Vertices[0]; // Any vertex
                Console.WriteLine("Start=" + rv.Data);
                List<Vertex<int>> e = g.GetEuler(rv);

                foreach (Vertex<int> v in e)
                {
                    Console.WriteLine("Vertex=" + v.Data);
                }

            }
            else
            {
                Console.WriteLine("Not Eulerian");
            }
        }

        /// <summary>
        /// Check if graph / subgraphs are Eulerian circuit, path or neither
        /// </summary>
        static void EulerDisconnectedTest()
        {
            Console.WriteLine("Euler Disconnected Test");

            // need to create a list of verticeis and how these are linked to edges
            // identify within a graph with disconnected subgraphs each of these and if
            // they are Eulerian
 
            /*
             * v1  v2 
             * +---+   + v5
             * |   |   |
             * +---+   + v6
             * v4  v3
             * 
             */

            Graph<int, int> g = new Graph<int, int>();

            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);
            Vertex<int> v5 = new Vertex<int>(5);
            Vertex<int> v6 = new Vertex<int>(6);

            g.AddVertex(v1);
            g.AddVertex(v2);
            g.AddVertex(v3);
            g.AddVertex(v4);
            g.AddVertex(v5);
            g.AddVertex(v6);

            g.AddEdge(v1, v2);
            g.AddEdge(v2, v3);
            g.AddEdge(v3, v4);
            g.AddEdge(v4, v1);
            g.AddEdge(v5, v6);

            g.Reset();

            foreach (Vertex<int> v in g.Vertices)
            {
                if (v.Visited == false)
                {
                    Console.WriteLine("Check");
                    List<Vertex<int>> c = g.GetConnected(v);

                    int euler = g.IsEulerian(c);

                    if (euler == 1)
                    {
                        Console.WriteLine("Euler Path");
                        Vertex<int> rv = g.FindStart(g.Vertices);
                        Console.WriteLine("Start=" + rv.Data);
                        List<Vertex<int>> e = g.GetEuler(rv);

                        foreach (Vertex<int> vt in e)
                        {
                            Console.WriteLine("Vertex=" + vt.Data);
                        }
                    }
                    else if (euler == 2)
                    {
                        Console.WriteLine("Euler Circuit");
                        Vertex<int> rv = g.Vertices[0]; // Any vertex
                        Console.WriteLine("Start=" + rv.Data);
                        List<Vertex<int>> e = g.GetEuler(rv);

                        foreach (Vertex<int> vt in e)
                        {
                            Console.WriteLine("Vertex=" + vt.Data);
                        }

                    }
                    else
                    {
                        Console.WriteLine("Not Eulerian");
                    }
                }
            }
        }

        /// <summary>
        /// Check if graph is Eulerian circuit, path or neither (from web sample)
        /// </summary>
        static void EulerTest()
        {
            Console.WriteLine("Euler Test");

            // need to create a list of verticies and linked to the edges
            /*
             *       v1  v2 
             *       .+--.+ 
             *     .*  .* |
             * v5 +   +--+ v4
             *        v3
             */

            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);
            Vertex<int> v5 = new Vertex<int>(5);

            Graph<int, int> g = new Graph<int, int>();

            g.AddVertex(v1);
            g.AddVertex(v2);
            g.AddVertex(v3);
            g.AddVertex(v4);
            g.AddVertex(v5);

            g.AddEdge(v1, v2);
            g.AddEdge(v2, v4);
            g.AddEdge(v3, v4);
            g.AddEdge(v3, v2);
            g.AddEdge(v5, v1);

            g.Reset();

            int euler = g.IsEulerian(g.Vertices);
            if (euler == 1)
            {
                Console.WriteLine("Euler Path");
                Vertex<int> rv = g.FindStart(g.Vertices);
                rv = v2; // Force this to match sample page from internet

                Console.WriteLine("Start=" + rv.Data);
                List<Vertex<int>> e = g.GetEuler(rv);

                foreach (Vertex<int> v in e)
                {
                    Console.WriteLine(v.Data);
                }
            }
            else if (euler == 2)
            {
                Console.WriteLine("Euler Circuit");

            }
            else
            {
                Console.WriteLine("Not Eulerian");
            }
        }
    }
}


