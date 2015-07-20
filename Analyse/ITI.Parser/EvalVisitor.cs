using System;
using System.Collections.Generic;
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
            double left = this.Visit( n.Left );
            double right = this.Visit( n.Right );
            switch( n.Operator )
            {
                case TokenType.Plus: return left + right;
                case TokenType.Minus: return left - right;
                case TokenType.Mult: return left * right;
                default: return left / right;
            }
        }

        public double Visit( ErrorNode n )
        {
            return Double.NaN;
        }

        public static double Evaluate( Node n )
        {
            return new EvalVisitor().Visit( n );
        }

    }
}
