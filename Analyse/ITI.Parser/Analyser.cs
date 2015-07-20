using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class Analyser
    {

        public Node Expression( string s )
        {
            return Expression( new Tokenizer( s ) );
        }

        // Before (from TD):      expression --> term +/- expression | term
        // After (some thoughts): expression --> term [+/- term]*
        public Node Expression( Tokenizer tokenizer )
        {
            Node left = Term( tokenizer );
            while( !(left is ErrorNode) )
            {
                TokenType oper = tokenizer.CurrentToken;
                if( !tokenizer.Match( TokenType.Plus ) && !tokenizer.Match( TokenType.Minus ) ) break;
                left = new BinaryOperatorNode( oper, left, Term( tokenizer ) );
            }
            return left;
        }

        public Node Term( Tokenizer tokenizer )
        {
            Node left = Factor( tokenizer );
            while( !(left is ErrorNode) )
            {
                TokenType oper = tokenizer.CurrentToken;
                if( !tokenizer.Match( TokenType.Mult ) && !tokenizer.Match( TokenType.Div ) ) break;
                left = new BinaryOperatorNode( oper, left, Factor( tokenizer ) );
            }
            return left;
        }

        public Node Factor( Tokenizer tokenizer )
        {
            if( tokenizer.Match( TokenType.Minus ) ) return new UnaryOperatorNode( TokenType.Minus, PositiveFactor( tokenizer ) );
            return PositiveFactor( tokenizer );
        }
        public Node PositiveFactor( Tokenizer tokenizer )
        {
            if( tokenizer.CurrentToken == TokenType.Number ) return Constant( tokenizer );
            if( !tokenizer.Match( TokenType.OpenPar ) ) return new ErrorNode( "Expected constant or opening parenthesis." );
            Node e = Expression( tokenizer );
            if( e is ErrorNode ) return e;
            if( !tokenizer.Match( TokenType.ClosePar ) ) return new ErrorNode( "Expected closing parenthesis." );
            return e;
        }

        public Node Constant( Tokenizer tokenizer )
        {
            int value;
            if( !tokenizer.MatchInteger( out value ) ) return new ErrorNode( "Constant expected." );
            return new ConstantNode( value );
        }

    }
}
