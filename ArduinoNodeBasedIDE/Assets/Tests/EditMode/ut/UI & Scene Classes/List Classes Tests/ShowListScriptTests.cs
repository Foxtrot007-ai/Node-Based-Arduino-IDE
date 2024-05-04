using NUnit.Framework;
using UnityEngine;


namespace ut.UIClasses.Lists
{
    public class ShowListScriptTests
    {

        public void SetObjects(ShowListScript script)
        {
            script.referenceListPrefab = new GameObject();

            script.globalVariableListPrefab = new GameObject();

            script.functionListPrefab = new GameObject();

            script.localVariableListPrefab = new GameObject();

            GameObject managerObject = new GameObject();
            NodeBlockManager manager = managerObject.AddComponent<NodeBlockManager>();
            script.nodeBlockManager = manager;
        }

        [Test]
        public void ShowListScriptFunctionListShowTest()
        {
            //given

            GameObject gameObject = new GameObject();
            ShowListScript script = gameObject.AddComponent<ShowListScript>();

            SetObjects(script);

            //when

            script.setListActive(script.functionListPrefab);

            //then
            Assert.AreEqual(script.functionListPrefab.activeSelf, true);
        }

        [Test]
        public void ShowListScriptReferenceListShowTest()
        {
            //given

            GameObject gameObject = new GameObject();
            ShowListScript script = gameObject.AddComponent<ShowListScript>();

            SetObjects(script);

            //when

            script.setListActive(script.referenceListPrefab);

            //then
            Assert.AreEqual(script.referenceListPrefab.activeSelf, true);
        }
        [Test]
        public void ShowListScriptLocalVariableListShowTest()
        {
            //given

            GameObject gameObject = new GameObject();
            ShowListScript script = gameObject.AddComponent<ShowListScript>();

            SetObjects(script);

            //when

            script.setListActive(script.localVariableListPrefab);

            //then
            Assert.AreEqual(script.localVariableListPrefab.activeSelf, true);
        }
        [Test]
        public void ShowListScriptGlobalVariableListPrefabShowTest()
        {
            //given
            GameObject gameObject = new GameObject();
            ShowListScript script = gameObject.AddComponent<ShowListScript>();

            SetObjects(script);

            //when

            script.setListActive(script.globalVariableListPrefab);

            //then
            Assert.AreEqual(script.globalVariableListPrefab.activeSelf, true);
        }
    }
}

