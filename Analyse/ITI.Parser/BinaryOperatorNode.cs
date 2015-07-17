using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class BinaryOperatorNode : Node
    {
        public BinaryOperatorNode( TokenType oper, Node left, Node right )
        {
            if( (oper & TokenType.IsBinaryOperator) == 0 )  throw new ArgumentException( "Must be e binary operator", "oper" );
            if( left == null ) throw new ArgumentNullException( "left" );
            if( right == null ) throw new ArgumentNullException( "right" );
            Operator = oper;
            Left = left;
            Right = right;
        }

        public TokenType Operator { get; private set; }
        
        public Node Left { get; private set; }
        
        public Node Right { get; private set; }
    }
}
