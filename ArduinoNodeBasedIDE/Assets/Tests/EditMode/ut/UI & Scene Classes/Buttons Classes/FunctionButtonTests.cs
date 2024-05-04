using System;
using System.Collections;
using System.Collections.Generic;
using Backend.API;
using Backend.API.DTO;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace ut.UIClasses
{
    public class FunctionButtonTests
    {
        public IFunctionManage MakeFunction(string name, string outputType)
        {
            IFunctionManage function = new FunctionFake();
            function.Change(new FunctionManageDto
            {
                FunctionName = name,
                OutputType = new MyTypeFake
                {
                    EType = (Backend.Type.EType) Enum.Parse(typeof(Backend.Type.EType), outputType),
                    TypeName = outputType
                }
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
            IFunctionManage node = MakeFunction("Test Function", "Void");

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

        [Test]
        public void FunctionButtonScriptSetSetupTest()
        {
            //given
            //Creating function
            IFunctionManage node = MakeFunction("setup", "Void");

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
            Assert.AreEqual(script.spawnButton.GetComponent<Button>().interactable, false);
            Assert.AreEqual(script.deleteButton.GetComponent<Button>().interactable, false);
            Assert.AreEqual(script.editButton.GetComponent<Button>().interactable, false);
            Assert.AreEqual(script.changeButton.GetComponent<Button>().interactable, true);
        }

        [Test]
        public void FunctionButtonScriptSetLoopTest()
        {
            //given
            //Creating function
            IFunctionManage node = MakeFunction("loop", "Void");

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
            Assert.AreEqual(script.spawnButton.GetComponent<Button>().interactable, false);
            Assert.AreEqual(script.deleteButton.GetComponent<Button>().interactable, false);
            Assert.AreEqual(script.editButton.GetComponent<Button>().interactable, false);
            Assert.AreEqual(script.changeButton.GetComponent<Button>().interactable, true);
        }
    }
}

