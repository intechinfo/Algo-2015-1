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
            var newLeft = this.VisitNode( n.Left );
            var newRight = this.VisitNode( n.Right );
            if( newLeft == n.Left && newRight == n.Right ) return n;
            return new BinaryOperatorNode( n.Operator, newLeft, newRight );
        }

        public virtual Node Visit( ErrorNode n )
        {
            return n;
        }

        public virtual Node Visit( UnaryOperatorNode n )
        {
            var newRight = this.VisitNode( n.Right );
            if( newRight == n.Right ) return n;
            return new UnaryOperatorNode( n.Operator, newRight );
        }

        public virtual Node Visit( VariableNode n )
        {
            return n;
        }

    }
}
