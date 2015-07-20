using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ITI.Parsing.Tests
{
    [TestFixture]
    public class EvaluationTests
    {
        [TestCase( "6 - 4 + 5", 7.0 )]
        [TestCase( "6 - (4 + 5)", -3.0 )]
        [TestCase( "(6 - 4) + 5", 7.0 )]
        public void simple_expression( string expression, double expected )
        {
            Analyser a = new Analyser();
            Node e = a.Expression( expression );
            double result = EvalVisitor.Evaluate( e );
            Assert.That( result, Is.EqualTo( expected ) );
        }

        [TestCase( "6 + -4", 2.0 )]
        [TestCase( "6 * -(4+6)", -60.0 )]
        [TestCase( "6 + - 4 - -(-5)", -3.0 )]
        public void expression_with_unary_minus( string expression, double expected )
        {
            Analyser a = new Analyser();
            Node e = a.Expression( expression );
            double result = EvalVisitor.Evaluate( e );
            Assert.That( result, Is.EqualTo( expected ) );
        }
    }
}
