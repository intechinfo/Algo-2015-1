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

        public Node Expression( Tokenizer tokenizer )
        {
            Node term = Term( tokenizer );
            if( !(term is ErrorNode) )
            {
                TokenType oper = tokenizer.CurrentToken;
                if( oper == TokenType.Plus || oper == TokenType.Minus )
                {
                    tokenizer.GetNextToken();
                    return new BinaryOperatorNode( oper, term, Expression( tokenizer ) );
                }
            }
            return term;
        }

        public Node Term( Tokenizer tokenizer )
        {
            Node factor = Factor( tokenizer );
            if( factor is ErrorNode ) return factor;
            if( tokenizer.Match( TokenType.Mult ) ) return new BinaryOperatorNode( TokenType.Mult, factor, Term( tokenizer ) );
            if( tokenizer.Match( TokenType.Div ) ) return new BinaryOperatorNode( TokenType.Div, factor, Term( tokenizer ) );
            return factor;
        }

        public Node Factor( Tokenizer tokenizer )
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
