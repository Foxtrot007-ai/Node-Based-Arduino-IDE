using System.Collections;
using System.Collections.Generic;
using Backend.API;
using Backend.API.DTO;
using Backend.Type;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace ut.UIClasses.Editors
{
    public class NodeBlockEditorTests
    {
        [UnityTest]
        public IEnumerator LoadTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("NodeBlockEditorTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject editorObject = GameObject.Find("NodeBlockEditor");

            Assert.NotNull(managerObject);
            Assert.NotNull(editorObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            NodeBlockEditor editor = editorObject.GetComponent<NodeBlockEditor>();
            IUserFunction function = manager.backendManager.Functions.AddFunction(new Backend.API.DTO.FunctionManageDto
            {
                FunctionName = "Name1",
                OutputType = new VoidType()
            });
            //when

            editor.SetNodeBlockToEdit(function);

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(editor.nodeBlockName.GetComponent<TMP_InputField>().text, function.Name);
            Assert.AreEqual(editor.outputTypeField.GetComponent<DropdownTypesScript>().option, "Void");
        }

        [UnityTest]
        public IEnumerator ModifyTypeAndNameTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("NodeBlockEditorTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject editorObject = GameObject.Find("NodeBlockEditor");

            Assert.NotNull(managerObject);
            Assert.NotNull(editorObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            NodeBlockEditor editor = editorObject.GetComponent<NodeBlockEditor>();
            IUserFunction function = manager.backendManager.Functions.AddFunction(new Backend.API.DTO.FunctionManageDto
            {
                FunctionName = "Name1",
                OutputType = new VoidType()
            });
            //when

            editor.SetNodeBlockToEdit(function);
            editor.nodeBlockName.GetComponent<TMP_InputField>().text = "newName";
            editor.outputTypeField.GetComponent<DropdownTypesScript>().option = "Int";
            

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(function.Name, "newName");
            Assert.AreEqual(function.OutputType.TypeName, "int");
        }


        [UnityTest]
        public IEnumerator AddInputTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("NodeBlockEditorTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject editorObject = GameObject.Find("NodeBlockEditor");

            Assert.NotNull(managerObject);
            Assert.NotNull(editorObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            NodeBlockEditor editor = editorObject.GetComponent<NodeBlockEditor>();
            IUserFunction function = manager.backendManager.Functions.AddFunction(new Backend.API.DTO.FunctionManageDto
            {
                FunctionName = "Name1",
                OutputType = new VoidType()
            }); 
            //when

            editor.SetNodeBlockToEdit(function);
            editor.AddInput();

            //then
            yield return new WaitForSeconds(1);
            Assert.NotNull(function.InputList.Variables.Find(v => v.Name == "var1" && v.Type.TypeName == "int"));
        }

        [UnityTest]
        public IEnumerator DeleteInputTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("NodeBlockEditorTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject editorObject = GameObject.Find("NodeBlockEditor");

            Assert.NotNull(managerObject);
            Assert.NotNull(editorObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            NodeBlockEditor editor = editorObject.GetComponent<NodeBlockEditor>();
            IUserFunction function = manager.backendManager.Functions.AddFunction(new Backend.API.DTO.FunctionManageDto
            {
                FunctionName = "Name1",
                OutputType = new VoidType()
            });
            function.InputList.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "test1",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });

            //when

            editor.SetNodeBlockToEdit(function);
            yield return new WaitForSeconds(1);
            Assert.IsNotEmpty(editor.inputObjects);
            
            GameObject inputButton = GameObject.FindGameObjectWithTag("InputButton");
            Assert.NotNull(inputButton);

            inputButton.GetComponent<InputButtonScript>().RemoveVariable();
            yield return new WaitForSeconds(1);


            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(function.InputList.Variables.Count, 0);
        }
    }
}
