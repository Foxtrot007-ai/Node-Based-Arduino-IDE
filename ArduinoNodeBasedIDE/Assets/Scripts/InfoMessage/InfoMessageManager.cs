using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for showing information from system to user
public class InfoMessageManager : MonoBehaviour
{
    //queue with message content
    private Queue<Tuple<String, float>> messages = new Queue<Tuple<String, float>>();
    public GameObject messagePrefab;
    public GameObject currentMessage = null;

    void Start()
    {
        //addMessage("First test message", 0.2f);
        //addMessage("Second test message", 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        //checking if current message showing ended then show next one to user
        if(currentMessage == null && messages.Count != 0)
        {
            currentMessage = Instantiate(messagePrefab);
            currentMessage.SetActive(true);
            currentMessage.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
            currentMessage.transform.localScale = Vector3.one;
            currentMessage.transform.position = GameObject.FindGameObjectWithTag("Canvas").transform.position;
            Tuple<String, float> message = messages.Dequeue();
            currentMessage.GetComponent<InfoMessageScript>().SetMessage(message.Item1, message.Item2);
        }
    }

    //function for other part of frontend system to add messages
    public void addMessage(String message, float fateRate)
    {
        messages.Enqueue(new Tuple<String, float>(message, fateRate));
    }
}
