using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ITI.Parsing.Tests
{
    [TestFixture]
    public class OptimizationTests
    {
        [TestCase( "6 + - - 4", "(6+4)" )]
        [TestCase( "x + -(-y)", "(x+y)" )]
        [TestCase( "x + -4", "(x+-4)" )]
        public void useless_minus( string expression, string rewritten )
        {
            Analyser a = new Analyser();
            Node e = a.Expression( expression );
            var optimizer = new RemoveUselessUnaryMinusVisitor();
            Node eOptimized = optimizer.VisitNode( e );
            Assert.That( ToStringVisitor.Stringify( eOptimized ), Is.EqualTo( rewritten ) );
        }
    }
}
