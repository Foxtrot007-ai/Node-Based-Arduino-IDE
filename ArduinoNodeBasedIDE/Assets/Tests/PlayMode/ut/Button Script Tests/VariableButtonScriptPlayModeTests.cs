using System.Collections;
using System.Collections.Generic;
using Backend.API;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Backend;
using Backend.Type;



namespace ut.UIClasses.Buttons
{
    public class VariableButtonScriptPlayModeTests
    {
        
        [UnityTest]
        public IEnumerator SetNodeBlockTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("VariableButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("VariableButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            VariableButtonScript button = buttonObject.GetComponent<VariableButtonScript>();

            //when

            IVariable newVariable = manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });

            button.SetNodeBlock(newVariable);

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(button.text.GetComponent<TMP_Text>().text, "a");
            Assert.AreEqual(button.typeField.GetComponent<TMP_Text>().text, "int");
        }
        [UnityTest]
        public IEnumerator SpawnNodeBlockTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("VariableButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("VariableButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            VariableButtonScript button = buttonObject.GetComponent<VariableButtonScript>();
            IVariable newVariable = manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            //when

            button.SetNodeBlock(newVariable);
            button.SpawnNodeBlock();

            //then
            yield return new WaitForSeconds(1);
            GameObject[] nodeBlock = GameObject.FindGameObjectsWithTag("NodeBlock");
            List<GameObject> nodeBlocks = new List<GameObject>(nodeBlock);
            Assert.AreNotEqual(nodeBlocks.Count, 0);
            Assert.NotNull(nodeBlocks.Find(obj => obj.GetComponent<NodeBlockController>().nodeBlock.NodeName == "Get "+ newVariable.Name));
        }
        [UnityTest]
        public IEnumerator DeleteNodeBlockTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("VariableButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("VariableButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            VariableButtonScript button = buttonObject.GetComponent<VariableButtonScript>();
            IVariable newVariable = manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });

  
            //when

            button.SetNodeBlock(newVariable);
            button.variable.Delete();

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(manager.backendManager.GlobalVariables.Variables.Contains(newVariable), false);
        }
        [UnityTest]
        public IEnumerator EditNodeBlockTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("VariableButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("VariableButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            VariableButtonScript button = buttonObject.GetComponent<VariableButtonScript>();
            IVariable newVariable = manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            //when

            button.SetNodeBlock(newVariable);
            button.EditMyNodeBlock();

            //then
            yield return new WaitForSeconds(1);

            GameObject editor = GameObject.Find("VariableEditor");
            Assert.AreEqual(editor.GetComponent<VariableEditor>().variable, newVariable);
        }
        [UnityTest]
        public IEnumerator SpawnSetNodeBlockTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("VariableButtonScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject buttonObject = GameObject.Find("VariableButton");

            Assert.NotNull(managerObject);
            Assert.NotNull(buttonObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            VariableButtonScript button = buttonObject.GetComponent<VariableButtonScript>();
            IVariable newVariable = manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            //when

            button.SetNodeBlock(newVariable);
            button.SpawnSetNodeBlock();

            //then
            yield return new WaitForSeconds(1);
            GameObject[] nodeBlock = GameObject.FindGameObjectsWithTag("NodeBlock");
            List<GameObject> nodeBlocks = new List<GameObject>(nodeBlock);
            Assert.NotNull(nodeBlocks.Find(obj => obj.GetComponent<NodeBlockController>().nodeBlock.NodeName == "Set " + newVariable.Name));
        }
        
    }
}