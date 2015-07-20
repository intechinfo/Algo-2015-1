using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public abstract class Node
    {
        public abstract void Accept( AbstractVisitor v );

    }
}
