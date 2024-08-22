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
using Backend.API.DTO;


namespace ut.UIClasses.Buttons
{
    public class InputButtonScriptPlayModeTests
    {
        
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

            IUserFunction function = manager.backendManager.Functions.AddFunction(new Backend.API.DTO.FunctionManageDto
            {
                FunctionName = "test1",
                OutputType = new VoidType()
            });
            //Creating Variable
            IVariable node = function.InputList.AddVariable(new VariableManageDto
            {
                VariableName = "a",
                Type = new Backend.Type.PrimitiveType(Backend.Type.EType.Int)
            });


            //when

            button.SetNodeBlock(node);


            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(button.text.GetComponent<TMP_Text>().text, "a");
            Assert.AreEqual(button.typeField.GetComponent<TMP_Text>().text, "int");
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

            manager.AddNodeBlock("test1", 0, 0);

            IUserFunction function = manager.backendManager.Functions.Functions[0];
            //Creating Variable
            IVariable node = function.InputList.AddVariable(new VariableManageDto
            {
                VariableName = "a",
                Type = new Backend.Type.PrimitiveType(Backend.Type.EType.Int)
            });

            //when
            yield return new WaitForSeconds(1);
            button.SetNodeBlock(node);
            button.SpawnNodeBlock();

            //then
            yield return new WaitForSeconds(1);
            GameObject[] nodeBlock = GameObject.FindGameObjectsWithTag("NodeBlock");
            List<GameObject> nodeBlocks = new List<GameObject>(nodeBlock);
            Assert.Null(nodeBlocks.Find(obj => obj.GetComponent<NodeBlockController>().nodeBlock.NodeName == node.Name));
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
            manager.AddNodeBlock("test1", 0, 0);

            IUserFunction function = manager.backendManager.Functions.Functions[0];
            //Creating Variable
            IVariable variable = function.InputList.AddVariable(new VariableManageDto
            {
                VariableName = "a",
                Type = new Backend.Type.PrimitiveType(Backend.Type.EType.Int)
            });

            //when

            button.SetNodeBlock(variable);
            button.DeleteNodeBlock();

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(manager.backendManager.Functions.Functions[0].InputList.Variables.Contains(variable), true);
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
            manager.AddNodeBlock("test1", 0, 0);

            IUserFunction function = manager.backendManager.Functions.Functions[0];
            //Creating Variable
            IVariable variable = function.InputList.AddVariable(new VariableManageDto
            {
                VariableName = "a",
                Type = new Backend.Type.PrimitiveType(Backend.Type.EType.Int)
            });

            //when

            button.SetNodeBlock(variable);
            button.EditMyNodeBlock();

            //then
            yield return new WaitForSeconds(1);

            GameObject editor = GameObject.Find("VariableEditor");
            Assert.AreEqual(editor.GetComponent<VariableEditor>().variable, variable);
        }
    }
}