using System.Collections;
using System.Collections.Generic;
using System;
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
    private string currentPath = "Assets/Resources/nodesOnScene.json";

    [Serializable]
    public class sSaveFile
    {
        public List<sView> sViews;
        public List<sConnection> sConnections;
    }

    [Serializable]
    public class sView
    {
        public string viewName;
        public List<sController> sControllers;
}
    [Serializable]
    public class sController
    {
        public string controllerId;
        public bool isStartNode;
        public string creatorId;
        public Vector3 positionOnScene;
    }
    [Serializable]
    public class sConnection
    {
        public string controllerIdFirst;
        public int outputIndex;
        public string controllerIdSecond;
        public int inputIndex;
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
                controller.isStartNode = obj.GetComponent<NodeBlockController>().isStartNodeBlock;
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
        //Debug.Log(json);
        WriteToFile(json);
    }

    public void Load()
    {
        string json = ReadFromFile();
        Debug.Log(json);
        sSaveFile save = JsonUtility.FromJson<sSaveFile>(json);

        if(save == null)
        {
            throw new Exception("Load file fail.");
        }

        Dictionary<string, INode> controllerIDs = new Dictionary<string, INode>();

        //create gameobjects with controllers
        foreach(sView view in save.sViews)
        {
            Debug.Log("viewName: " + view.viewName);
            IFunction function = backendManager.Functions.Functions.Find(v => v.Name == view.viewName);

            if (view.viewName == "setup")
            {
                function = backendManager.Setup;
            }
            else if (view.viewName == "loop")
            {
                function = backendManager.Loop;
            }
            else if(function == null)
            {
                throw new Exception("view Name fail");
            }

            Debug.Log("function name: " + function.Name);
            viewManager.AddNewView(function);
            viewManager.ChangeView(function);

            int i = 0;
            foreach (sController controller in view.sControllers)
            {
                Debug.Log("controllerId: " + controller.controllerId);

                Debug.Log("creatorId: " + controller.creatorId);

                Debug.Log(controller.isStartNode);

                GameObject nodeBlockObject = nodeBlockManager.SpawnNodeBlockWithoutValidation(nodeBlockManager.nodeBlockPrefab);
                nodeBlockObject.transform.position = controller.positionOnScene;
                INode node;
                if (view.viewName == "setup" && controller.isStartNode)
                {
                    node = backendManager.Setup.StartNode;
                }
                else if (view.viewName == "loop" && controller.isStartNode)
                {
                    node = backendManager.Loop.StartNode;
                }
                else if(controller.isStartNode)
                {
                    node = function.StartNode;
                }
                else
                {
                    node = backendManager.InstanceCreator.CreateNodeInstance(controller.creatorId);
                }

                if (controller.isStartNode) { //startnode 
                    nodeBlockObject.GetComponent<NodeBlockController>().isStartNodeBlock = true; 
                }

                nodeBlockObject.GetComponent<NodeBlockController>().InstantiateNodeBlockController(node);
                controllerIDs.Add(controller.controllerId, node);
                i++;
            }
        }

        //connect them;
        foreach(sConnection con in save.sConnections)
        {
            INode controllerFirst = controllerIDs[con.controllerIdFirst];
            INode controllerSecond = controllerIDs[con.controllerIdSecond];
            controllerFirst.OutputsList[con.outputIndex].Connect(controllerSecond.InputsList[con.inputIndex]);
        }
    }

    
}
