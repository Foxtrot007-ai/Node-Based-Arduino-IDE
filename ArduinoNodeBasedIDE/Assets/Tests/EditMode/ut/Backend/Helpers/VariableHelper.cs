using Backend.API.DTO;
using Backend.Type;

namespace Tests.EditMode.ut.Backend.Helpers
{
    public static class VariableHelper
    {
        public static VariableManageDto CreateDto(string varName = "test")
        {
            return new VariableManageDto
            {
                Type = TypeHelper.DefaultType,
                VariableName = varName
            };
        }

        public static VariableManageDto CreateDto(EType eType, string varName)
        {
            return new VariableManageDto
            {
                Type = TypeHelper.CreateType(eType),
                VariableName = varName
            };
        }
    }
}
