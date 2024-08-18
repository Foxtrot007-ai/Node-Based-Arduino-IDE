using NUnit.Framework;
using TMPro;
using UnityEngine;
using Backend;
using Backend.Type;
using Backend.API;

namespace ut.UIClasses.Buttons
{
    public class VariableButtonTests
    {
    
        [Test]
        public void VariableButtonScriptSetVariableTest()
        {
            //given
            //Creating variable
            IBackendManager backendManager = new BackendManager();

            //Creating Variable
            IVariable newVariable = backendManager.GlobalVariables.AddVariable(new Backend.API.DTO.VariableManageDto
            {
                VariableName = "test1",
                Type = new Backend.Type.PrimitiveType(EType.Int)
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

            script.SetNodeBlock(newVariable);

            //then
            Assert.AreEqual(nameField.GetComponent<TMP_Text>().text, newVariable.Name);
            Assert.AreEqual(typeField.GetComponent<TMP_Text>().text, newVariable.Type.TypeName);
        }
    }
}

