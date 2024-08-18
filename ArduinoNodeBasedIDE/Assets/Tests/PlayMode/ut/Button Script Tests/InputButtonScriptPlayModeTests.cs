using System.Collections;
using System.Collections.Generic;
using Backend.API;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;


namespace ut.UIClasses.Buttons
{
    public class InputButtonScriptPlayModeTests
    {
        /*
        [UnityTest]
        public IEnumerator SetNodeBlockTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("InputButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("InputButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            InputButtonScript button = buttonObject.GetComponent<InputButtonScript>();

            //when

            button.SetNodeBlock(SimpleNodeBlockMaker.MakeVariable("a", Backend.Type.EType.Int));


            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(button.text.GetComponent<TMP_Text>().text, "a");
            Assert.AreEqual(button.typeField.GetComponent<TMP_Text>().text, "Int");
        }
        [UnityTest]
        public IEnumerator SpawnNodeBlockTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("InputButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("InputButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            InputButtonScript button = buttonObject.GetComponent<InputButtonScript>();
            IVariableManage variable = SimpleNodeBlockMaker.MakeVariable("a", Backend.Type.EType.Int);
            //when

            button.SetNodeBlock(variable);
            button.SpawnNodeBlock();

            //then
            yield return new WaitForSeconds(1);
            GameObject[] nodeBlock = GameObject.FindGameObjectsWithTag("NodeBlock");
            List<GameObject> nodeBlocks = new List<GameObject>(nodeBlock);
            Assert.Null(nodeBlocks.Find(obj => obj.GetComponent<NodeBlockController>().nodeBlock.NodeName == variable.Name));
        }
        [UnityTest]
        public IEnumerator DeleteNodeBlockTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("InputButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("InputButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            InputButtonScript button = buttonObject.GetComponent<InputButtonScript>();
            IVariableManage variable = SimpleNodeBlockMaker.MakeVariable("a", Backend.Type.EType.Int);
            manager.viewsManager.AddVariableToView(variable);
            manager.variableList.Add(variable);
            //when

            button.SetNodeBlock(variable);
            button.DeleteNodeBlock();

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(manager.viewsManager.GetLocalVariables().Contains(variable), true);
            Assert.AreEqual(manager.variableList.Contains(variable), true);
        }
        [UnityTest]
        public IEnumerator EditNodeBlockTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("InputButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("InputButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            InputButtonScript button = buttonObject.GetComponent<InputButtonScript>();
            IVariableManage variable = SimpleNodeBlockMaker.MakeVariable("a", Backend.Type.EType.Int);
            manager.viewsManager.AddVariableToView(variable);
            manager.variableList.Add(variable);
            //when

            button.SetNodeBlock(variable);
            button.EditMyNodeBlock();

            //then
            yield return new WaitForSeconds(1);

            GameObject editor = GameObject.Find("VariableEditor");
            Assert.AreEqual(editor.GetComponent<VariableEditor>().variable, variable);
        }
        [UnityTest]
        public IEnumerator RemoveNodeBlockTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("InputButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("InputButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            InputButtonScript button = buttonObject.GetComponent<InputButtonScript>();
            IVariableManage variable = SimpleNodeBlockMaker.MakeVariable("a", Backend.Type.EType.Int);
            //when

            button.SetNodeBlock(variable);
            button.RemoveVariable();

            //then
            yield return new WaitForSeconds(1);
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("InputButton");
            List<GameObject> buttonList = new List<GameObject>(buttons);
            Assert.IsEmpty(buttonList);
        }*/
    }
}