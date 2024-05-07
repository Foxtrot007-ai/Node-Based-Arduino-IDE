using Castle.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoMessageManager : MonoBehaviour
{
    private Queue<Pair<String, float>> messages = new Queue<Pair<String, float>>();
    public GameObject messagePrefab;
    public GameObject currentMessage = null;
    // Start is called before the first frame update
    void Start()
    {
        addMessage("First test message", 0.2f);
        addMessage("Second test message", 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentMessage == null && messages.Count != 0)
        {
            currentMessage = Instantiate(messagePrefab);
            currentMessage.SetActive(true);
            currentMessage.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
            currentMessage.transform.localScale = Vector3.one;
            currentMessage.transform.position = GameObject.FindGameObjectWithTag("Canvas").transform.position;
            Pair<String, float> message = messages.Dequeue();
            currentMessage.GetComponent<InfoMessageScript>().SetMessage(message.First, message.Second);
        }
    }

    public void addMessage(String message, float fateRate)
    {
        messages.Enqueue(new Pair<String, float>(message, fateRate));
    }
}
