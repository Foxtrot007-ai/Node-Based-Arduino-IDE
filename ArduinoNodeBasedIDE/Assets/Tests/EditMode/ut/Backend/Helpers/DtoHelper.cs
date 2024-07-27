using Backend.API.DTO;
using Backend.Type;

namespace Tests.EditMode.ut.Backend.Helpers
{
    public static class DtoHelper
    {
        public static VariableManageDto CreateVariableManage(string varName = "test")
        {
            return new VariableManageDto
            {
                Type = MockHelper.DefaultType,
                VariableName = varName
            };
        }

        public static VariableManageDto CreateVariableManage(EType eType, string varName)
        {
            return new VariableManageDto
            {
                Type = MockHelper.CreateType(eType),
                VariableName = varName,
            };
        }

        public static FunctionManageDto CreateFunctionManage(string name = "test")
        {
            return new FunctionManageDto
            {
                FunctionName = name,
                OutputType = MockHelper.DefaultType,
            };
        }
        
        public static FunctionManageDto CreateFunctionManage(string name, EType eType)
        {
            return new FunctionManageDto
            {
                FunctionName = name,
                OutputType = MockHelper.CreateType(eType),
            };
        }
    }
}
