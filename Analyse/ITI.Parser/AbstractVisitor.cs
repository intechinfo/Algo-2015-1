using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public abstract class AbstractVisitor
    {
        public void Visit( Node n )
        {
            n.Accept( this );
        }

        public virtual void Visit( ConstantNode n )
        {
        }

        public virtual void Visit( BinaryOperatorNode n )
        {
            Visit( n.Left );
            Visit( n.Right );
        }

        public virtual void Visit( ErrorNode n )
        {
        }

    }
}
