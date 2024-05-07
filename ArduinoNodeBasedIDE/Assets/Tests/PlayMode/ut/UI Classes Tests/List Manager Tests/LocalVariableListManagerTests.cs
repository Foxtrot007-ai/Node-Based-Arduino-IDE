using Backend.API;
using Backend.API.DTO;
using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace ut.UIClasses.Lists
{
    public class LocalVariableListManagerTests
    {
        public IVariableManage MakeVariable(string name)
        {
            IVariableManage node = new VariableFake();
            node.Change(new VariableManageDto
            {
                VariableName = name,
                Type = new MyTypeFake
                {
                    EType = Backend.Type.EType.Int,
                    TypeName = "Int"
                }
            });
            return node;
        }

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
            manager.viewsManager.AddVariableToView(MakeVariable("a"));
            manager.viewsManager.AddVariableToView(MakeVariable("b"));
            manager.viewsManager.AddVariableToView(MakeVariable("c"));

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
            manager.viewsManager.AddVariableToView(MakeVariable("a"));
            manager.viewsManager.AddVariableToView(MakeVariable("b"));
            manager.viewsManager.AddVariableToView(MakeVariable("c"));

            //when
            list.nameField.GetComponent<TMP_InputField>().text = "a1";
            list.CreateNewVariable();

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(list.contentObjects.Count, 1);
        }
    }
}

