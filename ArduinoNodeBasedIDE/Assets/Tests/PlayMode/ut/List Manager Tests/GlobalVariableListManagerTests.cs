using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Backend.Type;

namespace ut.UIClasses.Lists
{
    public class GlobalVariableListManagerTests
    {

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
            manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a0",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a1",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a2",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a3",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });

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
            manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a0",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a1",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "a2",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            manager.backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "d0",
                Type = new Backend.Type.PrimitiveType(EType.Int)
            });
            //when
            //should search for only variable with "a" in name
            list.nameField.GetComponent<TMP_InputField>().text = "a";

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(list.contentObjects.Count, 3);
        }
    }
}


