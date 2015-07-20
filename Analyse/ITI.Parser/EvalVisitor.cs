using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class EvalVisitor : AbstractVisitor
    {
        double _curentValue;

        public override void Visit( ConstantNode n )
        {
            _curentValue = n.Value;
        }

        public override void Visit( BinaryOperatorNode n )
        {
            Visit( n.Left );
            double left = _curentValue;
            Visit( n.Right );
            double right = _curentValue;
            switch( n.Operator )
            {
                case TokenType.Plus: _curentValue = left + right; break;
                case TokenType.Minus: _curentValue = left - right; break;
                case TokenType.Mult: _curentValue = left * right; break;
                case TokenType.Div: _curentValue = left / right; break;
            }
        }

        public static double Evaluate( Node n )
        {
            var v = new EvalVisitor();
            v.Visit( n );
            return v._curentValue;
        }
    }
}
