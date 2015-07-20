using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Parsing
{
    public class ConstantNode : Node
    {
        public ConstantNode( int value )
        {
            Value = value;
        }

        public int Value { get; private set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override T Accept<T>( IAbstractVisitor<T> v )
        {
            return v.Visit( this );
        }

    }
}
