using System;
using Backend.API;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Backend;
using Backend.Type;

namespace ut.UIClasses.Buttons
{
    public class FunctionButtonTests
    {
        public IUserFunction MakeFunction(string name)
        {
            IBackendManager backendManager = new BackendManager();
            IUserFunction function = backendManager.Functions.AddFunction(new Backend.API.DTO.FunctionManageDto
            {
                FunctionName = name,
                OutputType = new VoidType()
            });
            return function;
        }

        public void SetButtons(FunctionButtonScript script)
        {
            script.changeButton = new GameObject();
            script.changeButton.AddComponent<Button>();

            script.editButton = new GameObject();
            script.editButton.AddComponent<Button>();

            script.deleteButton = new GameObject();
            script.deleteButton.AddComponent<Button>();

            script.spawnButton = new GameObject();
            script.spawnButton.AddComponent<Button>();
        }

        [Test]
        public void FunctionButtonScriptSetFunctionTest()
        {
            //given
            //Creating function
            IUserFunction node = MakeFunction("Test Function");

            //creating fake button object with his fields
            GameObject button = new GameObject();
            FunctionButtonScript script = button.AddComponent<FunctionButtonScript>();


            //text field
            GameObject nameField = new GameObject();
            nameField.AddComponent<TextMeshProUGUI>();

            script.text = nameField;

            //buttons

            SetButtons(script);

            //when

            script.SetNodeBlock(node);

            //then
            Assert.AreEqual(nameField.GetComponent<TMP_Text>().text, node.Name);
            Assert.AreEqual(script.spawnButton.GetComponent<Button>().interactable, true);
            Assert.AreEqual(script.deleteButton.GetComponent<Button>().interactable, true);
            Assert.AreEqual(script.editButton.GetComponent<Button>().interactable, true);
            Assert.AreEqual(script.changeButton.GetComponent<Button>().interactable, true);
        }
    }
}

