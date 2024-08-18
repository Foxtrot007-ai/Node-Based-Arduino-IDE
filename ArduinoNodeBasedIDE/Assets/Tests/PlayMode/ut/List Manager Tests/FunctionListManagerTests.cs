using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Backend.Type;

namespace ut.UIClasses.Lists
{
    public class FunctionListManagerTests
    {
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
            Assert.AreEqual(list.contentObjects[0].GetComponent<ButtonScript>().function.Name, "add");
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

            manager.backendManager.Functions.AddFunction(new Backend.API.DTO.FunctionManageDto
            {
                FunctionName = "Name1",
                OutputType = new VoidType()
            });
            manager.backendManager.Functions.AddFunction(new Backend.API.DTO.FunctionManageDto
            {
                FunctionName = "Name2",
                OutputType = new VoidType()
            });
            manager.backendManager.Functions.AddFunction(new Backend.API.DTO.FunctionManageDto
            {
                FunctionName = "Name3",
                OutputType = new VoidType()
            });
            manager.backendManager.Functions.AddFunction(new Backend.API.DTO.FunctionManageDto
            {
                FunctionName = "Name4",
                OutputType = new VoidType()
            });
            //when

            list.inputField.GetComponent<TMP_InputField>().text = "a";
            

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(list.contentObjects.Count, 4);
        }
    }
}





