using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public abstract class AbstractVisitor : IAbstractVisitor<Node>
    {
        public virtual Node Visit( ConstantNode n )
        {
            return n;
        }

        public virtual Node Visit( BinaryOperatorNode n )
        {
            this.VisitNode( n.Left );
            this.VisitNode( n.Right );
            return n;
        }

        public virtual Node Visit( ErrorNode n )
        {
            return n;
        }

        public virtual Node Visit( UnaryOperatorNode n )
        {
            return n;
        }

    }
}
