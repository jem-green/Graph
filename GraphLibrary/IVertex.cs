using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLibrary
{
    interface IVertex<TVertex>
   {
        public bool Visited { set; get; }
        public TVertex Data { set; get; }
        public int Degree { get; }
    }
}
