using System;
using System.Collections;
using System.Collections.Generic;
using Backend.API;
using Backend.API.DTO;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

namespace ut.UIClasses.Buttons
{
    public class VariableButtonTests
    {
    
        [Test]
        public void VariableButtonScriptSetVariableTest()
        {
            //given
            //Creating variable
            IVariableManage node = new VariableFake();
            node.Change(new VariableManageDto
            {
                VariableName = "test Function",
                Type = new MyTypeFake
                {
                    EType = Backend.Type.EType.Int,
                    TypeName = "Int"
                }
            });

            //creating fake button object with his fields
            GameObject button = new GameObject();
            VariableButtonScript script = button.AddComponent<VariableButtonScript>();

            GameObject nameField = new GameObject();
            nameField.AddComponent<TextMeshProUGUI>();

            GameObject typeField = new GameObject();
            typeField.AddComponent<TextMeshProUGUI>();

            script.text = nameField;
            script.typeField = typeField;
            //when

            script.SetNodeBlock(node);

            //then
            Assert.AreEqual(nameField.GetComponent<TMP_Text>().text, node.Name);
            Assert.AreEqual(typeField.GetComponent<TMP_Text>().text, node.Type.TypeName);
        }
    }
}

