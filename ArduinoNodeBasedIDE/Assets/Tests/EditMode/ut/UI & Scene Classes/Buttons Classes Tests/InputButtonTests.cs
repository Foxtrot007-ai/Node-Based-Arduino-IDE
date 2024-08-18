using Backend.API;
using Backend.API.DTO;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using Backend;
using Backend.Type;

namespace ut.UIClasses.Buttons
{
    public class InputButtonTests
    {

        [Test]
        public void InputButtonScriptSetVariableTest()
        {
            //given
            //Creating function
            IBackendManager backendManager = new BackendManager();
            IUserFunction function = backendManager.Functions.AddFunction(new Backend.API.DTO.FunctionManageDto
            {
                FunctionName = "test1",
                OutputType = new VoidType()
            });
            //Creating Variable
            IVariable node = function.InputList.AddVariable(new VariableManageDto
            {
                VariableName = "testVariable",
                Type = new Backend.Type.PrimitiveType(Backend.Type.EType.Int)
            });


            //creating fake button object with his fields
            GameObject button = new GameObject();
            InputButtonScript script = button.AddComponent<InputButtonScript>();

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

