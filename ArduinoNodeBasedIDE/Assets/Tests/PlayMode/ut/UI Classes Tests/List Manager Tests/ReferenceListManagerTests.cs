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
    public class ReferenceListManagerTests
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
        public IEnumerator LoadTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("ReferenceListManagerTestScene", LoadSceneMode.Single);
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
            ReferenceListManager list = listObject.GetComponent<ReferenceListManager>();
            manager.languageReferenceParser.functions.Add(MakeFunction("Add"));
            //when

            list.inputField.GetComponent<TMP_InputField>().text = "Add";

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(list.contentObjects.Count, 1);
            Assert.AreEqual(list.contentObjects[0].GetComponent<ButtonScript>().function.Name, "Add");
        }
    }
}



