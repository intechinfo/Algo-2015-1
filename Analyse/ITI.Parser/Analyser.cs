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
            return null;
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
