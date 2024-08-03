using System.Collections.Generic;
using Backend;
using Backend.Json;
using Backend.Variables;

namespace Tests.EditMode.ut.Backend.Mocks
{
    public class VariablesManagerMock : VariablesManager
    {

        public VariablesManagerMock()
        {
        }
        public VariablesManagerMock(List<VariableJson> variableJsons) : base(variableJsons)
        {
        }
        public VariablesManagerMock(PathName parentPn) : base(parentPn)
        {
        }
        public override bool IsVariableDuplicate(string name)
        {
            return false;
        }
    }
}
