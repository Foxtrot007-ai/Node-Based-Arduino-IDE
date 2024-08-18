using System.Collections;
using System.Collections.Generic;
using Backend.API;
using Backend.API.DTO;
using Backend.Type;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace ut.UIClasses.InfoMessage
{
    public class InfoMessageManagerTests
    {
        [UnityTest]
        public IEnumerator ShowMessageTest()
        {
            //given
            var asyncLoadLevel = SceneManager.LoadSceneAsync("InfoMessageManagerTestScene", LoadSceneMode.Single);
            while (!asyncLoadLevel.isDone)
            {
                Debug.Log("Loading the Scene");
                yield return null;
            }

            GameObject managerObject = GameObject.Find("InfoMessageManager");

            Assert.NotNull(managerObject);

            InfoMessageManager script = managerObject.GetComponent<InfoMessageManager>();

            //when

            script.addMessage("Test Message", 0.2f);
            yield return new WaitForSeconds(1);

            GameObject infoObject = GameObject.FindGameObjectWithTag("InfoMessage");

            Assert.NotNull(infoObject);

            InfoMessageScript info = infoObject.GetComponent<InfoMessageScript>();

            Assert.AreEqual(info.text.GetComponent<TMP_Text>().text, "Test Message");

            //then

            Debug.Log("Waiting for message to fade out");
            while (script.currentMessage != null)
            {             
                yield return null;
            }

            infoObject = GameObject.FindGameObjectWithTag("InfoMessage");

            Assert.IsNull(infoObject);
        }
    }
}
