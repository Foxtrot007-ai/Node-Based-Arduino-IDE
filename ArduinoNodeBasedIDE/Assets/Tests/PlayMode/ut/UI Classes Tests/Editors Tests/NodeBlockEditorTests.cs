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
            IFunctionManage function = SimpleNodeBlockMaker.MakeFunction("a", EType.Void);
            //when

            editor.SetNodeBlockToEdit(function);

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(editor.nodeBlockName.GetComponent<TMP_InputField>().text, function.Name);
            Assert.AreEqual(editor.outputTypeField.GetComponent<DropdownTypesScript>().option, function.OutputType.TypeName);
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
            IFunctionManage function = SimpleNodeBlockMaker.MakeFunction("a", EType.Void);
            //when

            editor.SetNodeBlockToEdit(function);
            editor.nodeBlockName.GetComponent<TMP_InputField>().text = "newName";
            editor.outputTypeField.GetComponent<DropdownTypesScript>().option = "Int";
            editor.UpdateFunction();

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(function.Name, "newName");
            Assert.AreEqual(function.OutputType.TypeName, "Int");
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
            IFunctionManage function = SimpleNodeBlockMaker.MakeFunction("a", EType.Void);
            //when

            editor.SetNodeBlockToEdit(function);
            editor.AddInput();
            editor.UpdateFunction();

            //then
            yield return new WaitForSeconds(1);
            Assert.NotNull(function.InputList.VariableManages.Find(v => v.Name == "var1" && v.Type.TypeName == "Int"));
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
            IFunctionManage function = SimpleNodeBlockMaker.MakeFunction("a", EType.Void);
            function.InputList.AddVariable(SimpleNodeBlockMaker.MakeVariable("var", EType.Bool));
            
            //when

            editor.SetNodeBlockToEdit(function);
            yield return new WaitForSeconds(1);
            Assert.IsNotEmpty(editor.inputObjects);
            
            GameObject inputButton = GameObject.FindGameObjectWithTag("InputButton");
            Assert.NotNull(inputButton);

            inputButton.GetComponent<InputButtonScript>().RemoveVariable();
            yield return new WaitForSeconds(1);

            editor.UpdateFunction();

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(function.InputList.VariableManages.Count, 0);
        }
    }
}
