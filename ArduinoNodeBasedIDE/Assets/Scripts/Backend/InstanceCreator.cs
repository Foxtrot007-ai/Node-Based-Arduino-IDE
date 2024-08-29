using System;
using Backend.API;
using Backend.MyFunction;
using Backend.Variables;
using UnityEngine;

namespace Backend
{
    public class InstanceCreator : IInstanceCreator
    {
        private IBackendManager _backendManager;

        public InstanceCreator(IBackendManager backendManager)
        {
            _backendManager = backendManager;
        }

        private INode GetVariable(PathName pathName, IVariablesManager variablesManager)
        {
            var variable = ((VariablesManager)variablesManager).GetVariableByPn(pathName.GetParent());

            return pathName.GetClassName() switch
            {
                "GET" => variable.CreateGetNode(),
                "SET" => variable.CreateSetNode(),
                _ => throw new Exception(),
            };
        }

        private INode GetTemplate(PathName pathName, IFunction function)
        {
            return ((TemplateManager)_backendManager.Templates)
                .GetTemplateByPn(pathName)
                .CreateNodeInstance(function);
        }

        private INode GetFunction(PathName pathName)
        {

            if (pathName.GetClassName() == "USER_FUNCTION")
            {
                return ((UserFunctionManager)_backendManager.Functions)
                    .GetFunctionByPn(pathName)
                    .CreateNode();
            }

            var userFunction = ((UserFunctionManager)_backendManager.Functions)
                .GetFunctionByPn(pathName.GetParent().GetParent());

            return pathName.GetParent().GetClassName() switch
            {
                "LOCAL_VAR" => GetVariable(pathName, userFunction.Variables),
                "PARAM_VAR" => GetVariable(pathName, userFunction.InputList),
                _ => throw new Exception(),
            };
        }

        public INode CreateNodeInstance(string id, IFunction function)
        {
            var pathName = new PathName(id);
            Debug.Log(pathName);
            if (pathName.GetFirstPath().ToString() != "ROOT-1")
            {
                throw new ArgumentException();
            }

            return pathName.GetNextPath().GetFirstPath().GetClassName() switch
            {
                "GLOBAL_VAR" => GetVariable(pathName, _backendManager.GlobalVariables),
                "TEMPLATE" => GetTemplate(pathName, function),
                "SETUP" => GetVariable(pathName, _backendManager.Setup.Variables),
                "LOOP" => GetVariable(pathName, _backendManager.Loop.Variables),
                "USER_FUNCTION" => GetFunction(pathName),
                _ => throw new Exception(),
            };
        }
    }
}
