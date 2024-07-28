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
    public class ReferenceButtonTests
    {
        [Test]
        public void ReferenceButtonScriptSetFunctionTest()
        {
            //given
            IFunctionManage node = new FunctionFake();
            node.Change(new FunctionManageDto
            {
                FunctionName = "test Function",
                OutputType = new MyTypeFake 
                { 
                    EType = Backend.Type.EType.Void, 
                    TypeName = "Void"
                }
            });

            GameObject button = new GameObject();
            ReferenceButtonScript script = button.AddComponent<ReferenceButtonScript>();

            GameObject text = new GameObject();
            text.AddComponent<TextMeshProUGUI>();

            script.text = text;
            //when

            script.SetNodeBlock(node);

            //then
            Assert.AreEqual(text.GetComponent<TMP_Text>().text, node.Name);
        }
        [Test]
        public void ReferenceButtonScriptSetVariableTest()
        {
            //given
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

            GameObject button = new GameObject();
            ReferenceButtonScript script = button.AddComponent<ReferenceButtonScript>();

            GameObject text = new GameObject();
            text.AddComponent<TextMeshProUGUI>();

            script.text = text;
            //when

            script.SetNodeBlock(node);

            //then
            Assert.AreEqual(text.GetComponent<TMP_Text>().text, node.Name);
        }
    }
}

