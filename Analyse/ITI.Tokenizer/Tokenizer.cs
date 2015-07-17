using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class Tokenizer
    {
        readonly string _toParse;
        int _pos;
        int _maxPos;
        TokenType _type;

        int _curIntValue;

        public Tokenizer( string s )
            : this( s, 0, s.Length )
        {
        }

        public Tokenizer( string s, int startIndex )
            : this( s, startIndex, s.Length )
        {
        }

        public Tokenizer( string s, int startIndex, int count )
        {
            if( s == null ) throw new ArgumentNullException();
            if( count < 0 ) throw new ArgumentException();
            _toParse = s;
            _type = TokenType.None;
            _pos = startIndex;
            _maxPos = startIndex + count;
            GetNextToken();
        }

        #region Head (private)

        char Peek()
        {
            Debug.Assert( !IsEnd );
            return _toParse[_pos];
        }

        char Read()
        {
            Debug.Assert( !IsEnd );
            return _toParse[_pos++];
        }

        void Forward()
        {
            Debug.Assert( !IsEnd );
            _pos++;
        }

        bool IsEnd
        {
            get { return _pos >= _maxPos; }
        }

        #endregion

        public TokenType CurrentToken
        {
            get { return _type; }
        }

        public bool Match( TokenType t )
        {
            if( _type == t )
            {
                GetNextToken();
                return true;
            }
            return false;
        }

        public bool MatchInteger( int expected )
        {
            if( _type == TokenType.Number && _curIntValue == expected )
            {
                GetNextToken();
                return true;
            }
            return false;
        }

        public bool MatchInteger( out int value )
        {
            value = 0;
            if( _type == TokenType.Number )
            {
                value = _curIntValue;
                GetNextToken();
                return true;
            }
            return false;
        }

        public TokenType GetNextToken()
        {
            // 1 - The end.
            if( IsEnd ) return _type = TokenType.EndOfInput;
            // 2 - White space
            char c;
            while( Char.IsWhiteSpace( c = Read() ) )
            {
                if( IsEnd ) return _type = TokenType.EndOfInput;
            }
            // 3 - Terminals
            switch( c )
            {
                case '*': return _type = TokenType.Mult;
                case '/': return _type = TokenType.Div;
                case '+': return _type = TokenType.Plus;
                case '-': return _type = TokenType.Minus;
                case '(': return _type = TokenType.OpenPar;
                case ')': return _type = TokenType.ClosePar;
                default:
                    {
                        // 4 - Non terminals
                        if( Char.IsDigit( c ) )
                        {
                            _type = TokenType.Number;
                            _curIntValue = (int)(c - '0');
                            while( !IsEnd && Char.IsDigit( c = Peek() ) )
                            {
                                _curIntValue = _curIntValue * 10 + (int)(c - '0');
                                Forward();
                            }
                        }
                        else _type = TokenType.Error;
                        break;
                    }
            }
            return _type;
        }

    }
}
