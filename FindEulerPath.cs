C# Program::

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Euler_Circuit_Application
{
    class EulerCircuit
    {
        Stack tempPath = new Stack();
        ArrayList finalPath = new ArrayList();//to store the final path
        char[] nodeList;//to store the nodes
        char[,] GraphMatrix;//to store the edge representation of Graph
        int total, count;//total->total no of nodes, count->no of even degree node
        //To Get the all input from user
        private void GetInput()
        {
            Console.WriteLine("Enter the number of Nodes");
            try
            {//Get the number of nodes in a Graph
                total = int.Parse(Console.ReadLine());
                GraphMatrix = new char[total, total];
                nodeList = new char[total];               

                Console.WriteLine("Enter the Nodes");
                //To get the node/vertices to nodeList array
                for (int i = 0; i < total; i++)
                {                    
                    nodeList[i] = char.Parse(Console.ReadLine());
                }

                Console.WriteLine("Enter the Graph representattion in Matrix");
                Console.WriteLine("If there is an edge between the two vertices then enter 'y' else 'n'");             
                //To get the edge details in a Graph
                for (int i = 0; i < total; i++)
                {
                   
                    for (int j = 0; j < total; j++)
                    {
                        Console.Write("{0}----{1}==> ",nodeList[i],nodeList[j]);
                        GraphMatrix[i, j] = char.Parse(Console.ReadLine());                        
                    }
                    Console.WriteLine("");
                }
            }
            catch
            {
                Console.WriteLine("Invalid number");
            }
        }

        //To get the number of edges connected to vertex at index i of nodeList array
        private int GetDegree(int i)
        {
            int j, deg = 0;
            for (j = 0; j < total; j++)
            {
                if (GraphMatrix[i,j] == 'y') deg++;
            }
            return deg;
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
        private int FindRoot()
        {
            int root = 1; //Assume root as 1
            count = 0;
            for (int i = 0; i < total; i++)
            {
                if (GetDegree(i) % 2 != 0)
                {
                    count++;
                    root = i;//Store the node which has odd degree to root variable
                }
            }
            //If count is not exactly 2 then euler path/circuit not possible so return 0
            if (count != 0 && count != 2)
            {
                return 0;
            }
            else return root;// if exactly 2 nodes have odd degree, 
            //it will return one of those node as root otherwise return 1 as root  as assumed
        }

        //To get the current index of node in the array nodeList[] of nodes
        private int GetIndex(char c)
        {
            int index = 0;
            while (c != nodeList[index])
                index++;
            return index;
        }

        //To check weather all adjecent vertices/nodes are visited or not
        
        private Boolean AllVisited(int node)
        {            
            for (int l = 0; l < total; l++)
            {
                if (GraphMatrix[node, l] == 'y')
                    return false;
            }
            return true;
        }

        //To find the Euler circuit/path and store it in finalPath arrayList
        private void FindEuler(int root)
        {
            int ind;
            tempPath.Clear();
            //push root into the stack
            tempPath.Push(nodeList[root]);
            while(tempPath.Count!=0)//until Stack going to empty
            {
                //get the array index of top of the stack
                ind = GetIndex((char)tempPath.Peek());                
                if (AllVisited(ind))
                {
                    //If all adjacent nodes are already visited
                    //pop element from stack and store it in finalpath arrayList
                    finalPath.Add(tempPath.Pop());
                }
                else
                {
                    //If any unvisited node available push that node into stack
                    //mark that edge as already visited by marking 'n' in GraphMatrix[][]
                    //break the iteration
                    for (int j = 0; j < total; j++)
                    {
                        if (GraphMatrix[ind,j] == 'y')
                        {
                            GraphMatrix[ind, j] = 'n';
                            GraphMatrix[j, ind] = 'n';
                            tempPath.Push(nodeList[j]);
                            break;
                        }
                    }
                }
            }
        }

        //THis is the Main Program
        public void FindEulerCircuit()
        {
            //Get the Graph representation from user
            GetInput();
            //Decide the root
            int root = FindRoot();
            //findRoot() will return 0 if euler path/circuit not possible
            //otherwise it will return array index of any node as root
            if(root!=0)
            {
              if(count!=0) Console.WriteLine("Available Euler Path is");
              else  Console.WriteLine("Available Euler circuit is");
                //Find the Euler circuit
              FindEuler(root);
                //Print the euler Circuit
              PrintEulerCircuit();
            }
            else
            {
                Console.WriteLine("Euler Path or Circuit not Possible");
            }           
            
        }

        public void PrintEulerCircuit()
        {
            for (int i = 0; i < finalPath.Count;i++ )
            {
                Console.Write("{0}--->", finalPath[i]);
            }
        }
    }

    class ExecuteEulerCircuit
    {
        static void Main(string[] args)
        {          
            //Create oblect for EulerCircuit class and call FindEulerCircuit()
            EulerCircuit ec = new EulerCircuit();
            ec.FindEulerCircuit();           

            Console.ReadKey();
        }
    }
}