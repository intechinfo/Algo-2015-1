using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ITI.Parsing.Tests
{
    [TestFixture]
    public class SimpleParserTests
    {
        [TestCase( "6 - 4 + 5" )]
        [TestCase( "6 - (4 + 5)" )]
        public void simple_expression( string expression )
        {
            Analyser a = new Analyser();
            Node e = a.Expression( expression );
            Assert.That( e, Is.InstanceOf<BinaryOperatorNode>() );
            var root = (BinaryOperatorNode)e;
            Assert.That( root.Operator, Is.EqualTo( TokenType.Minus ) );
            Assert.That( root.Left, Is.InstanceOf<ConstantNode>() );
            Assert.That( ((ConstantNode)root.Left).Value, Is.EqualTo( 6 ) );
            Assert.That( root.Right, Is.InstanceOf<BinaryOperatorNode>() );
            var right = (BinaryOperatorNode)root.Right;
            Assert.That( right.Operator, Is.EqualTo( TokenType.Plus ) );
        }
    }
}
