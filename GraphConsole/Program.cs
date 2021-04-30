using System;
using System.Collections.Generic;
using GraphLibrary;

namespace GraphConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //BoxTest();
            //DisconnectedTes();
            //ConnectedTest();
            //EulerPathTest();
            //EulerTest();
            EulerCircuitTest();

        }
        static void BoxTest()
        {
            Console.WriteLine("Box Test");

            // need to create a list of points and how these are linked to lines
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

            Vertex<int> v1 = new Vertex<int>(1);
            Vertex<int> v2 = new Vertex<int>(2);
            Vertex<int> v3 = new Vertex<int>(3);
            Vertex<int> v4 = new Vertex<int>(4);
            Vertex<int> v5 = new Vertex<int>(5);
            Vertex<int> v6 = new Vertex<int>(6);

            Graph<int, int> g = new Graph<int, int>();
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

        static void ConnectedTest()
        {
            Console.WriteLine("Connected Test");

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
            g.AddEdge(v1, v2);
            g.AddEdge(v2, v3);
            g.AddEdge(v3, v4);
            g.AddEdge(v4, v1);
            g.AddEdge(v3, v6);
            g.AddEdge(v5, v6);
            g.AddEdge(v4, v5);

            g.Reset();

            Console.WriteLine("Eulerian=" + g.IsEulerian(g.Vertices));

            Vertex<int> rv = g.FindRoot(g.Vertices);

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
                Vertex<int> rv = g.FindRoot(g.Vertices);
                Console.WriteLine("Start=" + rv.Data);
                List<Vertex<int>> e = g.GetEuler(g.Vertices, rv);

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
                Vertex<int> rv = g.FindRoot(g.Vertices);
                Console.WriteLine("Start=" + rv.Data);
                List<Vertex<int>> e = g.GetEuler(g.Vertices, rv);

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
                List<Vertex<int>> e = g.GetEuler(g.Vertices, rv);

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
                Vertex<int> rv = g.FindRoot(g.Vertices);
                rv = v2; // Force this to match sample page from internet

                Console.WriteLine("Start=" + rv.Data);
                List<Vertex<int>> e = g.GetEuler(g.Vertices, rv);

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


