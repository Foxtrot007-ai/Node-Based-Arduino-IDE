using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Backend.API;
using TMPro;
using UnityEngine.SceneManagement;
using Backend.Type;

namespace ut.UIClasses.Editors
{
    public class VariableEditorTests
    {
        [UnityTest]
        public IEnumerator LoadTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("VariableEditorTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject editorObject = GameObject.Find("VariableEditor");

            Assert.NotNull(managerObject);
            Assert.NotNull(editorObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            VariableEditor editor = editorObject.GetComponent<VariableEditor>();
            IVariable variable = manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            //when

            editor.InstantiateEditor(variable);

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(editor.variableNameField.GetComponent<TMP_InputField>().text, variable.Name);
            Assert.AreEqual(editor.typeField.GetComponent<DropdownTypesScript>().option, "Int");
        }
        [UnityTest]
        public IEnumerator UpdateTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("VariableEditorTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject editorObject = GameObject.Find("VariableEditor");

            Assert.NotNull(managerObject);
            Assert.NotNull(editorObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            VariableEditor editor = editorObject.GetComponent<VariableEditor>();
            IVariable variable = manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            //when

            editor.InstantiateEditor(variable);
            editor.variableNameField.GetComponent<TMP_InputField>().text = "newName";
            editor.typeField.GetComponent<DropdownTypesScript>().option = "Bool";
            editor.UpdateVariable();

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(variable.Name, "newName");
            Assert.AreEqual(variable.Type.TypeName, "bool");
        }
        [UnityTest]
        public IEnumerator SwitchTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("VariableEditorTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject editorObject = GameObject.Find("VariableEditor");

            Assert.NotNull(managerObject);
            Assert.NotNull(editorObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            VariableEditor editor = editorObject.GetComponent<VariableEditor>();
            IVariable variable = manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            //when

            editor.InstantiateEditor(variable);

            yield return new WaitForSeconds(1);
            Assert.AreEqual(editor.variableNameField.GetComponent<TMP_InputField>().text, variable.Name);
            Assert.AreEqual(editor.typeField.GetComponent<DropdownTypesScript>().option, "Int");

            IVariable otherVariable = manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "b",
                Type = new Backend.Type.PrimitiveType(EType.Bool)
            });
            editor.InstantiateEditor(otherVariable);
            //then

            yield return new WaitForSeconds(1);
            Assert.AreEqual(editor.variable, otherVariable);
            Assert.AreEqual(editor.variableNameField.GetComponent<TMP_InputField>().text, otherVariable.Name);
            Assert.AreEqual(editor.typeField.GetComponent<DropdownTypesScript>().option, "Bool");
        }
    }
}