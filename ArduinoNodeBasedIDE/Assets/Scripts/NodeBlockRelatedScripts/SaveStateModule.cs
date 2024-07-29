using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Backend.API;
using Backend;
using Backend.Type;
using System;
using System.IO;
using UnityEngine.UIElements;

public class SaveStateModule
{
    private NodeBlockManager nodeBlockManager;
    private IBackendManager backendManager;
    private ViewsManager viewManager;
    private string currentPath = "Assets/Resources/save.json";

    [Serializable]
    public class sSaveFile
    {
        public List<sView> sViews { get; set; }
        public List<sConnection> sConnections { get; set; }
    }

    [Serializable]
    public class sView
    {
        public string viewName { get; set; }
        public List<sController> sControllers { get; set; }
}
    [Serializable]
    public class sController
    { 
        public string controllerId { get; set; }

        public string creatorId { get; set; }
        public Vector3 positionOnScene { get; set; }
    }
    [Serializable]
    public class sConnection
    {
        public string controllerIdFirst { get; set; }
        public int outputIndex { get; set; }
        public string controllerIdSecond { get; set; }
        public int inputIndex { get; set; }
    }

    public void Instantiate(NodeBlockManager manager, IBackendManager bManager, ViewsManager vManager)
    {
        this.nodeBlockManager = manager;
        this.backendManager = bManager;
        this.viewManager = vManager;
    }
    private void WriteToFile(string json)
    {
        StreamWriter writer = new StreamWriter(currentPath, false);
        writer.Write(json);
        writer.Close();
    }

    private string ReadFromFile()
    {
        StreamReader reader = new StreamReader(currentPath);
        return reader.ReadLine();
    }

    public void Save()
    {
        int currentControllerIndex = 0;
        sSaveFile save = new sSaveFile();
        save.sViews = new List<sView>();
        save.sConnections = new List<sConnection>();
        Dictionary<INode, string> controllerIDs = new Dictionary<INode, string>();
        
        //iterate and save controllers positions
        foreach(KeyValuePair<IFunction, List<GameObject>> entry in viewManager.views)
        {
            sView view = new sView();
            view.viewName = entry.Key.Name;
            view.sControllers = new List<sController>();
            foreach (GameObject obj in entry.Value)
            {
                sController controller = new sController();
                string newId = "#" + currentControllerIndex;
                controller.controllerId = newId;
                controllerIDs.Add(obj.GetComponent<NodeBlockController>().nodeBlock, newId);
                currentControllerIndex++;
                controller.positionOnScene = obj.GetComponent<NodeBlockController>().transform.position;
                controller.creatorId = obj.GetComponent<NodeBlockController>().nodeBlock.CreatorId;
                view.sControllers.Add(controller);
            }
            save.sViews.Add(view);
        }

        //iterate and save connections
        foreach (KeyValuePair<INode, string> entry in controllerIDs)
        {
            int outputIndex = 0;
            foreach(IConnection con in entry.Key.OutputsList)
            {
                if(con.Connected != null)
                {
                    sConnection connection = new sConnection();
                    connection.controllerIdFirst = entry.Value;
                    connection.outputIndex = outputIndex;

                    INode controllerSecond = con.Connected.ParentNode;
                    connection.controllerIdSecond = controllerIDs[controllerSecond];
                    int inputIndex = 0;
                    foreach (IConnection con2 in controllerSecond.InputsList)
                    {
                        if(con.Connected == con2)
                        {
                            connection.inputIndex = inputIndex;
                            break;
                        }
                        inputIndex++;
                    }

                    save.sConnections.Add(connection);
                }
                outputIndex++;
            }
        }

        //parse to json
        string json = JsonUtility.ToJson(save);
        WriteToFile(json);
    }

    public void Load()
    {
        string json = ReadFromFile();
        sSaveFile save = JsonUtility.FromJson<sSaveFile>(json);

        //create gameobjects with controllers

        //connect them;

        //set view as start
    }

    
}
