using System.Collections.Generic;
using Backend.Exceptions.InOut;
using Backend.IO;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.IO
{
    [TestFixture]
    [TestOf(typeof(FlowIO))]
    [Category("IO")]
    public class FlowIOTest
    {

        private FlowIO _flowIn;

        [SetUp]
        public void Init()
        {
            _flowIn = IOHelper.CreateFlowIO(IOSide.Input);
        }

        private static List<BaseIO> _wrong = new()
        {
            IOHelper.CreateBaseMock(),
            IOHelper.CreateTypeIO(),
        };

        [Test]
        [TestCaseSource(nameof(_wrong))]
        public void WrongConnectionTypeException(BaseIO output)
        {
            Assert.Throws<WrongConnectionTypeException>(() => _flowIn.Connect(output));
        }

        [Test]
        public void ConnectionOk()
        {
            //given
            var output = IOHelper.CreateFlowIO();
            //when
            _flowIn.Connect(output);
            //then
            IOHelper.ExpectAreConnected(_flowIn, output);
        }

    }
}
