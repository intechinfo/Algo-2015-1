using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class EvalVisitor : IAbstractVisitor<double>
    {
        public double Visit( ConstantNode n )
        {
            return n.Value;
        }

        public double Visit( BinaryOperatorNode n )
        {
            double left = this.VisitNode( n.Left );
            double right = this.VisitNode( n.Right );
            switch( n.Operator )
            {
                case TokenType.Plus: return left + right;
                case TokenType.Minus: return left - right;
                case TokenType.Mult: return left * right;
                default: return left / right;
            }
        }

        public double Visit( UnaryOperatorNode n )
        {
            Debug.Assert( n.Operator == TokenType.Minus, "Only Minus is currently supported as UnaryOperator." );
            return -this.VisitNode( n.Right );
        }

        public double Visit( ErrorNode n )
        {
            return Double.NaN;
        }

        public static double Evaluate( Node n )
        {
            return new EvalVisitor().VisitNode( n );
        }

    }
}
