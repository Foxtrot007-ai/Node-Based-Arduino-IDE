using System.Collections;
using System.Collections.Generic;
using Backend.API;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Backend.Type;



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
            IUserFunction function = manager.backendManager.Functions.AddFunction(new Backend.API.DTO.FunctionManageDto
            {
                FunctionName = "a",
                OutputType = new VoidType()
            });

            //when

            button.SetNodeBlock(function);


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
            IUserFunction function = manager.backendManager.Functions.AddFunction(new Backend.API.DTO.FunctionManageDto
            {
                FunctionName = "a",
                OutputType = new VoidType()
            });

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
            IUserFunction function = manager.backendManager.Functions.AddFunction(new Backend.API.DTO.FunctionManageDto
            {
                FunctionName = "a",
                OutputType = new VoidType()
            });

            //when

            button.SetNodeBlock(function);
            button.function.Delete();

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(manager.backendManager.Functions.Functions.Contains(function), false);
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
            IUserFunction function = manager.backendManager.Functions.AddFunction(new Backend.API.DTO.FunctionManageDto
            {
                FunctionName = "a",
                OutputType = new VoidType()
            });

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