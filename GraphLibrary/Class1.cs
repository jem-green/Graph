using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLibrary
{
    class GFG
    {
        // Function to find out the path
        // It takes the adjacency matrix
        // representation of the graph
        // as input
        static void findpath(int[,] graph,
                             int n)
        {
            List<int> numofadj =
                      new List<int>();

            // Find out number of edges
            // each vertex has
            for (int i = 0; i < n; i++)
                numofadj.Add(accumulate(graph,
                                        i, 0));

            // Find out how many vertex has
            // odd number edges
            int startPoint = 0, numofodd = 0;
            for (int i = n - 1; i >= 0; i--)
            {
                if (numofadj[i] % 2 == 1)
                {
                    numofodd++;
                    startPoint = i;
                }
            }

            // If number of vertex with odd
            // number of edges is greater than
            // two return "No Solution".
            if (numofodd > 2)
            {
                Console.WriteLine("No Solution");
                return;
            }

            // If there is a path find the path
            // Initialize empty stack and path
            // take the starting current as
            // discussed
            Stack<int> stack = new Stack<int>();
            List<int> path = new List<int>();
            int cur = startPoint;

            // Loop will run until there is element
            // in the stack or current edge has some
            // neighbour.
            while (stack.Count != 0 ||
                   accumulate(graph, cur, 0) != 0)
            {

                // If current node has not any
                // neighbour add it to path and
                // pop stack set new current to
                // the popped element
                if (accumulate(graph, cur, 0) == 0)
                {
                    path.Add(cur);
                    cur = stack.Pop();

                    // If the current vertex has at
                    // least one neighbour add the
                    // current vertex to stack, remove
                    // the edge between them and set the
                    // current to its neighbour.
                }
                else
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (graph[cur, i] == 1)
                        {
                            stack.Push(cur);
                            graph[cur, i] = 0;
                            graph[i, cur] = 0;
                            cur = i;
                            break;
                        }
                    }
                }
            }

            // print the path
            foreach (int ele in path)
                Console.Write(ele + " -> ");
            Console.WriteLine(cur);
        }

        static int accumulate(int[,] matrix,
                              int row, int sum)
        {
            int[] arr = GetRow(matrix,
                               row);

            foreach (int i in arr)
                sum += i;
            return sum;
        }

        public static int[] GetRow(int[,] matrix,
                                   int row)
        {
            var rowLength = matrix.GetLength(1);
            var rowVector = new int[rowLength];

            for (var i = 0; i < rowLength; i++)
                rowVector[i] = matrix[row, i];

            return rowVector;
        }
    }
}
