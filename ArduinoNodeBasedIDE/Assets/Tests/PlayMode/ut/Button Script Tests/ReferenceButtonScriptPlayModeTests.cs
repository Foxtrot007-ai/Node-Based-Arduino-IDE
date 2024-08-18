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
    
    public class ReferenceButtonScriptPlayModeTests
    {
        /*
        [UnityTest]
        public IEnumerator SetNodeBlockTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("ReferenceButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("ReferenceButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            ReferenceButtonScript button = buttonObject.GetComponent<ReferenceButtonScript>();

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
            var asyncLoadLevel = SceneManager.LoadSceneAsync("ReferenceButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("ReferenceButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            ReferenceButtonScript button = buttonObject.GetComponent<ReferenceButtonScript>();
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
            var asyncLoadLevel = SceneManager.LoadSceneAsync("ReferenceButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("ReferenceButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            ReferenceButtonScript button = buttonObject.GetComponent<ReferenceButtonScript>();
            IFunctionManage function = SimpleNodeBlockMaker.MakeFunction("a", Backend.Type.EType.Int);
            manager.languageReferenceParser.functions.Add(function);
            //when

            button.SetNodeBlock(function);
            button.DeleteNodeBlock();

            //then
            yield return new WaitForSeconds(1);
            //can't delete from reference list
            Assert.AreEqual(manager.languageReferenceParser.functions.Contains(function), true);
        }
        [UnityTest]
        public IEnumerator EditNodeBlockTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("ReferenceButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("ReferenceButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            ReferenceButtonScript button = buttonObject.GetComponent<ReferenceButtonScript>();
            IFunctionManage function = SimpleNodeBlockMaker.MakeFunction("a", Backend.Type.EType.Int);
            manager.languageReferenceParser.functions.Add(function);
            //when

            button.SetNodeBlock(function);
            button.EditMyNodeBlock();

            //then
            yield return new WaitForSeconds(1);
            GameObject editor = GameObject.Find("NodeBlockEditor");
            
            //can't edit node from reference list
            Assert.AreNotEqual(editor.GetComponent<NodeBlockEditor>().currentNodeBlock, function);
        }*/
    }
}