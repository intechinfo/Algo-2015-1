using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public interface IAbstractVisitor<T>
    {
        T Visit( ConstantNode n );

        T Visit( BinaryOperatorNode n );

        T Visit( ErrorNode n );

        T Visit( UnaryOperatorNode n );

        T Visit( VariableNode n );
   }

    public static class IAbstractVisitorExtensions
    {
        public static T VisitNode<T>( this IAbstractVisitor<T> @this, Node n )
        {
            return n.Accept( @this );
        }
    }
}
