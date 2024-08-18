using Backend.API;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using Backend;

namespace ut.UIClasses.Buttons
{
    public class ReferenceButtonTests
    {
        [Test]
        public void ReferenceButtonScriptSetFunctionTest()
        {
            //given
            IBackendManager backendManager = new BackendManager();
            ITemplate function = backendManager.Templates.Templates[0];


            GameObject button = new GameObject();
            ReferenceButtonScript script = button.AddComponent<ReferenceButtonScript>();

            GameObject text = new GameObject();
            text.AddComponent<TextMeshProUGUI>();

            script.text = text;
            //when

            script.SetNodeBlock(function);

            //then
            Assert.AreEqual(text.GetComponent<TMP_Text>().text, function.Name);
        }
    }
}

