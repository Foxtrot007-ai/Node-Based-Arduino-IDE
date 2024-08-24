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
            var variable = ((VariablesManager)variablesManager).GetVariableByPn(pathName);

            return pathName.GetNextPath().GetClassName() switch
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
            var userFunction = ((UserFunctionManager)_backendManager.Functions).GetFunctionByPn(pathName);
            var nextPn = pathName.GetNextPath();
            if (nextPn is null)
            {
                return userFunction.CreateNode();
            }
            return nextPn.GetClassName() switch
            {
                "LOCAL_VAR" => GetVariable(nextPn, userFunction.Variables),
                "PARAM_VAR" => GetVariable(nextPn, userFunction.InputList),
                _ => throw new Exception(),
            };
        }

        // DEPRECATED
        public INode CreateNodeInstance(string id)
        {
            var pathName = new PathName(id);
            if (pathName.GetFirstPath().ToString() != "ROOT-1")
            {
                throw new ArgumentException();
            }

            var nextPn = pathName.GetNextPath();

            return nextPn.GetClassName() switch
            {
                "GLOBAL_VAR" => GetVariable(nextPn, _backendManager.GlobalVariables),
                "TEMPLATE" => GetTemplate(nextPn, null),
                "SETUP" => GetVariable(nextPn.GetNextPath(), _backendManager.Setup.Variables),
                "LOOP" => GetVariable(nextPn.GetNextPath(), _backendManager.Loop.Variables),
                "USER_FUNCTION" => GetFunction(nextPn),
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

            var nextPn = pathName.GetNextPath();

            return nextPn.GetClassName() switch
            {
                "GLOBAL_VAR" => GetVariable(nextPn, _backendManager.GlobalVariables),
                "TEMPLATE" => GetTemplate(pathName, function),
                "SETUP" => GetVariable(nextPn.GetNextPath(), _backendManager.Setup.Variables),
                "LOOP" => GetVariable(nextPn.GetNextPath(), _backendManager.Loop.Variables),
                "USER_FUNCTION" => GetFunction(nextPn),
                _ => throw new Exception(),
            };
        }
    }
}
