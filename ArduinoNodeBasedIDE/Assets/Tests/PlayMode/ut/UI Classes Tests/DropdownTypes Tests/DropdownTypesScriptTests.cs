using System.Collections;
using System.Collections.Generic;
using Backend.Validator;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace ut.UIClasses.Dropdown
{
    public class DropdownTypesScriptTests
    {
        [UnityTest, Order(1)]
        public IEnumerator LoadTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("DropdownTypesScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject dropmenu = GameObject.Find("DropdownTypes");

            Assert.NotNull(managerObject);
            Assert.NotNull(dropmenu);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            DropdownTypesScript dropdownTypes = dropmenu.GetComponent<DropdownTypesScript>();

            //when

            dropdownTypes.UpdateDropdownWithData();
            GameObject dropdown = GameObject.Find("Dropdown");
            Assert.NotNull(dropdown);
            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(dropdown.GetComponent<TMP_Dropdown>().options.Count, 10);
        }
        [UnityTest, Order(2)]
        public IEnumerator AddTypeTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("DropdownTypesScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject dropmenu = GameObject.Find("DropdownTypes");

            Assert.NotNull(managerObject);
            Assert.NotNull(dropmenu);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            DropdownTypesScript dropdownTypes = dropmenu.GetComponent<DropdownTypesScript>();

            //when
            ClassTypeValidator.AddClassType("TestClassType");

            dropdownTypes.UpdateDropdownWithData();
            GameObject dropdown = GameObject.Find("Dropdown");
            Assert.NotNull(dropdown);

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(dropdown.GetComponent<TMP_Dropdown>().options.Count, 11);
        }
        [UnityTest, Order(3)]
        public IEnumerator SelectTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("DropdownTypesScriptTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.FindGameObjectWithTag("NodeBlocksManager");
            GameObject dropmenu = GameObject.Find("DropdownTypes");

            Assert.NotNull(managerObject);
            Assert.NotNull(dropmenu);

            NodeBlockManager manager = managerObject.GetComponent<NodeBlockManager>();
            DropdownTypesScript dropdownTypes = dropmenu.GetComponent<DropdownTypesScript>();

            //when
            dropdownTypes.UpdateDropdownWithData();
            dropdownTypes.option = "Bool";
            GameObject dropdown = GameObject.Find("Dropdown");
            Assert.NotNull(dropdown);

            //then
            yield return new WaitForSeconds(1);
            Assert.AreEqual(dropdown.GetComponent<TMP_Dropdown>().captionText.text, "Bool");
        }
    }
}
