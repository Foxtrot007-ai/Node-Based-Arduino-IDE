using Backend;
using Backend.API.DTO;
using NSubstitute;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.mocks
{
    public abstract class VariableMock : Variable
    {
        public VariableMock(VariablesManager variablesManager, VariableManageDto variableManageDto) : base(variablesManager, variableManageDto)
        {
        }

        public VariableMock() : base(Substitute.For<VariablesManager>(), VariableHelper.CreateDto())
        {

        }
    }
}
