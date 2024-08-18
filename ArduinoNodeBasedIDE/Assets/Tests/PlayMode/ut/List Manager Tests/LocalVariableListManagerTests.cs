using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Backend.Type;

namespace ut.UIClasses.Lists
{
    public class LocalVariableListManagerTests
    {
        [UnityTest]
        public IEnumerator LoadTestScene()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("LocalVariableListManagerTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject listObject = GameObject.FindGameObjectWithTag("SearchBar");

            Assert.NotNull(managerObject);
            Assert.NotNull(listObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            LocalVariableListManager list = listObject.GetComponent<LocalVariableListManager>();

            //when
            list.nameField.GetComponent<TMP_InputField>().text = "testVariable";
            list.CreateNewVariable();

            //then
            Assert.NotNull(manager.viewsManager.GetLocalVariables().Find(x => x.Name == "testVariable"));
        }

        [UnityTest]
        public IEnumerator LoadVariableListTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("LocalVariableListManagerTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject listObject = GameObject.FindGameObjectWithTag("SearchBar");

            Assert.NotNull(managerObject);
            Assert.NotNull(listObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            LocalVariableListManager list = listObject.GetComponent<LocalVariableListManager>();
            manager.backendManager.Loop.Variables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a1",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            manager.backendManager.Loop.Variables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a2",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            manager.backendManager.Loop.Variables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a3",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });


            //when
            list.nameField.GetComponent<TMP_InputField>().text = "";
            list.ReloadVariables();

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(list.contentObjects.Count, 3);
        }

        [UnityTest]
        public IEnumerator SearchBarTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("LocalVariableListManagerTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject listObject = GameObject.FindGameObjectWithTag("SearchBar");

            Assert.NotNull(managerObject);
            Assert.NotNull(listObject);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            LocalVariableListManager list = listObject.GetComponent<LocalVariableListManager>();
            manager.backendManager.Loop.Variables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a1",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            manager.backendManager.Loop.Variables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "b2",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            manager.backendManager.Loop.Variables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "c3",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });


            //when
            list.nameField.GetComponent<TMP_InputField>().text = "a";
            list.CreateNewVariable();

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(list.contentObjects.Count, 2);
        }
    }
}

