using Backend.API;
using Backend.API.DTO;
using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace ut.UIClasses.Lists
{
    public class GlobalVariableListManagerTests
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
        public IEnumerator CreateGlobalVariableTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("GlobalVariableListManagerTestScene", LoadSceneMode.Single);
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
            GlobalVariableListManager list = listObject.GetComponent<GlobalVariableListManager>();

            //when
            list.nameField.GetComponent<TMP_InputField>().text = "NewGlobalVariable";
            list.CreateNewVariable();

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(list.contentObjects[0].GetComponent<ButtonScript>().variable.Name, "NewGlobalVariable");
        }
        [UnityTest]
        public IEnumerator LoadListTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("GlobalVariableListManagerTestScene", LoadSceneMode.Single);
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
            GlobalVariableListManager list = listObject.GetComponent<GlobalVariableListManager>();
            manager.variableList.Add(MakeVariable("a"));
            manager.variableList.Add(MakeVariable("b"));
            manager.variableList.Add(MakeVariable("c"));
            manager.variableList.Add(MakeVariable("d"));

            //when
            list.nameField.GetComponent<TMP_InputField>().text = "test";
            yield return new WaitForSeconds(1);

            list.nameField.GetComponent<TMP_InputField>().text = "";
            yield return new WaitForSeconds(1);

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(list.contentObjects.Count, 4);
        }
        [UnityTest]
        public IEnumerator SearchBarTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("GlobalVariableListManagerTestScene", LoadSceneMode.Single);
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
            GlobalVariableListManager list = listObject.GetComponent<GlobalVariableListManager>();
            manager.variableList.Add(MakeVariable("a0"));
            manager.variableList.Add(MakeVariable("a1"));
            manager.variableList.Add(MakeVariable("a2"));
            manager.variableList.Add(MakeVariable("d0"));

            //when
            //should search for only variable with "a" in name
            list.nameField.GetComponent<TMP_InputField>().text = "a";

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(list.contentObjects.Count, 3);
        }
    }
}


