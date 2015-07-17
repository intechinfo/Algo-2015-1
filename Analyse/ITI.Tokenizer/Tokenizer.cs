using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class Tokenizer
    {
        IEnumerator<char> _input;
        int _pos;
        int _maxPos;
        TokenType _type;

        public Tokenizer( string s )
            : this( s, 0, s.Length )
        {
        }

        public Tokenizer( string s, int startIndex )
            : this( s, startIndex, s.Length )
        {
        }

        public Tokenizer( string s, int startIndex, int count )
            : this( (IEnumerable<char>)s, startIndex, count )
        {
        }

        public Tokenizer( IEnumerable<char> input, int startIndex, int count )
        {
            if( input == null ) throw new ArgumentNullException();
            if( count < 0 ) throw new ArgumentException();
            _input = input.GetEnumerator();
            _type = TokenType.None;
            _pos = startIndex;
            _maxPos = startIndex + count;
        }

        public bool MatchInteger( int expected )
        {

        }

        public bool MatchInteger( out int value )
        {

        }

    }
}
