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
    public class FunctionButtonScriptPlayModeTests
    {

        [UnityTest]
        public IEnumerator SetNodeBlockTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("FunctionButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("FunctionButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            FunctionButtonScript button = buttonObject.GetComponent<FunctionButtonScript>();

            //when

            button.SetNodeBlock(SimpleNodeBlockMaker.MakeFunction("a", Backend.Type.EType.Int));


            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(button.text.GetComponent<TMP_Text>().text, "a");
        }
        [UnityTest]
        public IEnumerator SpawnNodeBlockTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("FunctionButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("FunctionButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            FunctionButtonScript button = buttonObject.GetComponent<FunctionButtonScript>();
            IFunctionManage function = SimpleNodeBlockMaker.MakeFunction("a", Backend.Type.EType.Int);
            //when

            button.SetNodeBlock(function);
            button.SpawnNodeBlock();

            //then
            yield return new WaitForSeconds(1);
            GameObject[] nodeBlock = GameObject.FindGameObjectsWithTag("NodeBlock");
            List<GameObject> nodeBlocks = new List<GameObject>(nodeBlock);
            Assert.NotNull(nodeBlocks.Find(obj => obj.GetComponent<NodeBlockController>().nodeBlock.NodeName == function.Name));
        }
        [UnityTest]
        public IEnumerator DeleteNodeBlockTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("FunctionButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("FunctionButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            FunctionButtonScript button = buttonObject.GetComponent<FunctionButtonScript>();
            IFunctionManage function = SimpleNodeBlockMaker.MakeFunction("a", Backend.Type.EType.Int);
            manager.myFunctionList.Add(function);
            manager.viewsManager.AddNewView(function);
            
            //when

            button.SetNodeBlock(function);
            button.DeleteNodeBlock();

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(manager.myFunctionList.Contains(function), false);
        }

        [UnityTest]
        public IEnumerator EditNodeBlockTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("FunctionButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("FunctionButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            FunctionButtonScript button = buttonObject.GetComponent<FunctionButtonScript>();
            IFunctionManage function = SimpleNodeBlockMaker.MakeFunction("a", Backend.Type.EType.Int);
            manager.myFunctionList.Add(function);
            manager.viewsManager.AddNewView(function);

            //when

            button.SetNodeBlock(function);
            button.EditMyNodeBlock();

            //then
            yield return new WaitForSeconds(1);
            GameObject editor = GameObject.Find("NodeBlockEditor");
            Assert.AreEqual(editor.GetComponent<NodeBlockEditor>().currentNodeBlock, function);
        }
    }
}