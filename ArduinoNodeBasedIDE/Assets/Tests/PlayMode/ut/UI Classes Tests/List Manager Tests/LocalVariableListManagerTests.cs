using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace ut.UIClasses.Lists
{
    public class LocalVariableListManagerTests
    {
        NodeBlockManager manager;
        LocalVariableListManager list;
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

            manager = managerObject.GetComponent<NodeBlockManager>();
            list = listObject.GetComponent<LocalVariableListManager>();

            //when
            list.nameField.GetComponent<TMP_InputField>().text = "testVariable";
            list.CreateNewVariable();

            //then
            Assert.NotNull(manager.viewsManager.GetLocalVariables().Find(x => x.Name == "testVariable"));
        }
    }
}

