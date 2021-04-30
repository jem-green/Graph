/Written and Compiled by M.Karuppasamy Pandiyan
#include<stdio.h>
#include<conio.h>
char stack[20];
int top=-1, n;
char b[20],finalPath[20];
char ajMat[20][20];
int fp=0,count;

//Push into Stack Operation
void push(char val)
{
top=top+1;
stack[top]=val;
}

//Pop from Stack Operation
char pop()
{
return stack[top--];
}

//To check weather all adjecent vertices/nodes are visited 
//or not
int allVisited(int i)
{
    int j;
    for(j=0;j<n;j++)
    {
       if(ajMat[i][j]=='y')
       return 0;
    }
    return 1;
}

//To get the current index of node in the array b[] of nodes
int getNo(char c)
{
    int l=0;
    while(c!=b[l])
    l++;
    return l;

}

//Display the Euler circuit/path
void displayPath()
{
     int i;
     for(i=0;i<fp;i++)
     {
       printf("%c ->",finalPath[i]);
     }
}

//To find the Euler circuit/path and store it in finalPath[] array
void eularFind(int root)
{
     int l,j;
     //push root into the stack
    push(b[root]);
    //Run upto stock becomes empty i.e top=-1
    while(top!=-1)
    {
      
      //get the array index of top of the stack
      l=getNo(stack[top]);
      //If all adjacent nodes are already visited
      //pop element from stack and store it in finalpath[] array
      if(allVisited(l))
      {
        finalPath[fp++]=pop();
        
      }
      //If any unvisited node available push that node into stack
      //mark that edge as already visited by marking 'n' in adjMat[][]
      //break the iteration
      else
      {
        for(j=0;j<n;j++)
        {
        if(ajMat[l][j]=='y')
        {
            
             ajMat[l][j]='n';
             ajMat[j][l]='n';
             push(b[j]);
            
             break;
           
        }
        }
       }
     }
}

//To get the degree of node i.e no of edges currently connected to the node
int getDegree(int i)
{
    int j,deg=0;
    for(j=0;j<n;j++)
    {
      if(ajMat[i][j]=='y') deg++;
    }
    return deg;
}

//To assign the root of the graph
//Condition 1: If all Nodes have even degree, there should be a euler Circuit/Cycle
//We can start path from any node
//Condition 2: If exactly 2 nodes have odd degree, there should be euler path.
//We must start from node which has odd degree
//Condition 3: If more than 2 nodes or exactly one node have odd degree, euler path/circuit not possible.

//findRoot() will return 0 if euler path/circuit not possible
//otherwise it will return array index of any node as root
int findRoot()
{
     
     int i,cur=1;//Assume root as 1
     for(i=0;i<n;i++)
     {
        if(getDegree(i)%2!=0)
        {
           count++;
           cur=i;//Store the node which has odd degree to cur variable
        }
     }
     //If count is not exactly 2 then euler path/circuit not possible so return 0
     if(count!=0 && count!=2)
     {
        return 0;
     }
     else return cur;// if exactly 2 nodes have odd degree, it will return one of those node as root otherwise return 1 as root  as assumed
}

int main()
{
    char v;
    int i,j,l;
    printf("Enter the number of nodes in a graph\n");
    scanf("%d",&n);
    printf("Enter the value of node of graph\n");
    for( i=0; i<n; i++)
    {
     scanf("%s",&b[i]);//store the nodes in b[] array
    }
    
    //Get the Graph details by using adjacency matrix
    printf("Enter the value in adjancency matrix in from of 'Y' or 'N'\n");
    printf("\nIf there is an edge between the two vertices then enter 'Y' or 'N'\n");
    for( i=0; i<n; i++)
    printf(" %c ",b[i]);
    for( i=0;i<n; i++)
    {
     printf("\n%c ",b[i]);
     for( j=0; j<n; j++)
     {
      printf("%c ",v=getch());
      ajMat[i][j]=v;
     }
      printf("\n\n");
    }
    
//findRoot() will return 0 if euler path/circuit not possible
//otherwise it will return array index of any node as root
    int root;
    if(root=findRoot())
    {
      if(count) printf("Available Euler Path is\n");
      else printf("Available Euler Circuit is\n");
      eularFind(root);
      displayPath();
    }
    else printf("Euler path or circuit not available\n");
    getch();
}
