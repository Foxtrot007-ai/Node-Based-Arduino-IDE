using System.Collections.Generic;
using Backend.Exceptions.InOut;
using Backend.InOut;
using Backend.InOut.Base;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.InOut.Base
{
    
    [TestFixture]
    [TestOf(typeof(StringInOut))]
    [Category("InOut")]
    public class StringInOutTest
    {

        private StringInOut _stringIn;
        
        [SetUp]
        public void Setup()
        {
            _stringIn = (StringInOut) InOutHelper.CreateBaseInOut(InOutType.String, InOutSide.Input);
        }
        
        private static List<IInOut> _wrong = new()
        {
            InOutHelper.CreateBaseInOut(InOutType.Class),
            InOutHelper.CreateBaseInOut(InOutType.Flow),
            InOutHelper.CreateBaseInOut(InOutType.Void),
        };
        
        [Test]
        [TestCaseSource(nameof(_wrong))]
        public void WrongConnectionTypeException(IInOut @out)
        {
            Assert.Throws<WrongConnectionTypeException>(() => _stringIn.Connect(@out));
        }
        
        private static List<IInOut> _ok = new()
        {
            InOutHelper.CreateBaseInOut(InOutType.String),
            InOutHelper.CreateBaseInOut(InOutType.Primitive),
        };
        
        [Test]
        [TestCaseSource(nameof(_ok))]
        public void ConnectionOk(IInOut output)
        {
            //given
            //when
            _stringIn.Connect(output);
            //then
            InOutHelper.ExpectAreConnected(_stringIn, output);
        }
    }
}