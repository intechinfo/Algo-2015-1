using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class ToStringVisitor : AbstractVisitor
    {
        StringBuilder _buffer;

        public ToStringVisitor()
        {
            _buffer = new StringBuilder();
        }

        public override void Visit( ConstantNode n )
        {
            _buffer.Append( n.Value );
            base.Visit( n );
        }

        public override void Visit( BinaryOperatorNode n )
        {
            _buffer.Append( '(' );
            Visit( n.Left );
            _buffer.Append( n.OperatorString );
            Visit( n.Right );
            _buffer.Append( ')' );
        }

        public override string ToString()
        {
            return _buffer.ToString();
        }

        public static string Stringify( Node n )
        {
            var v = new ToStringVisitor();
            v.Visit( n );
            return v.ToString();
        }

    }
}
