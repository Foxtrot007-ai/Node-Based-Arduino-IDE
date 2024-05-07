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
    public class FunctionListManagerTests
    {
        public IFunctionManage MakeFunction(string name)
        {
            IFunctionManage node = new FunctionFake();
            node.Change(new FunctionManageDto
            {
                FunctionName = name,
                OutputType = new MyTypeFake
                {
                    EType = Backend.Type.EType.Int,
                    TypeName = "Int"
                }
            });
            return node;
        }
        [UnityTest]
        public IEnumerator CreateFunctionTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("FunctionListManagerTestScene", LoadSceneMode.Single);
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
            FunctionListManager list = listObject.GetComponent<FunctionListManager>();
            //when

            list.nameField.GetComponent<TMP_InputField>().text = "add";
            list.CreateNewFunction();

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(list.contentObjects.Count, 3);
            Assert.AreEqual(list.contentObjects[0].GetComponent<ButtonScript>().function.Name, "setup");
            Assert.AreEqual(list.contentObjects[1].GetComponent<ButtonScript>().function.Name, "loop");
            Assert.AreEqual(list.contentObjects[2].GetComponent<ButtonScript>().function.Name, "add");
        }

        [UnityTest]
        public IEnumerator SearchBarTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("FunctionListManagerTestScene", LoadSceneMode.Single);
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
            FunctionListManager list = listObject.GetComponent<FunctionListManager>();
            manager.myFunctionList.Add(MakeFunction("a0"));
            manager.myFunctionList.Add(MakeFunction("a1"));
            manager.myFunctionList.Add(MakeFunction("a2"));
            manager.myFunctionList.Add(MakeFunction("a3"));
            //when

            list.inputField.GetComponent<TMP_InputField>().text = "a";
            

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(list.contentObjects.Count, 4);
        }
    }
}





