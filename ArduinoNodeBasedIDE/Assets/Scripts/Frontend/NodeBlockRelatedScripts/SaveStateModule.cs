using System.Collections.Generic;
using System;
using UnityEngine;
using Backend.API;
using System.IO;
using Backend.API.DTO;
using TMPro;
using Backend.Json;

public class SaveStateModule
{
    private NodeBlockManager nodeBlockManager;
    private IBackendManager backendManager;
    private ViewsManager viewManager;
    public GameObject inputFieldNameFile;
    private string currentLoadFileName = "defaultSaveFile";
    private string defaultPath = "defaultSaveFile";

    //object which be write to/ read from json file
    [Serializable]
    public class sSaveFile
    {
        public List<sView> sViews;
        public List<sConnection> sConnections;
        public BackendManagerJson sBackend;
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
        public sInputValue inputValue;
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

    [Serializable]
    public class sInputValue
    {
        public string value;
        public string type;
    }
    public void Instantiate(NodeBlockManager manager, IBackendManager bManager, ViewsManager vManager, GameObject inputField)
    {
        this.nodeBlockManager = manager;
        this.backendManager = bManager;
        this.viewManager = vManager;
        this.inputFieldNameFile = inputField;
        inputFieldNameFile.GetComponent<TMP_InputField>().text = defaultPath;
    }

    public string GetCurrentLoadedFilePath()
    {
        return ("defaultSaveFile" != currentLoadFileName) ? Application.persistentDataPath + "/" + currentLoadFileName + ".json" : "Editor state not saved";
    }
    private void WriteToFile(string json)
    {
        string currentPath = inputFieldNameFile.GetComponent<TMP_InputField>().text;
        this.currentLoadFileName = currentPath;
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/" + currentPath + ".json", false);
        writer.Write(json);
        writer.Close();
    }

    private sSaveFile ReadFromJsonFromFile()
    {
        string currentPath = inputFieldNameFile.GetComponent<TMP_InputField>().text;
        var filePath = Application.persistentDataPath + "/" + currentPath + ".json";
        if (!File.Exists(filePath))
        {
            return null;
        }
        this.currentLoadFileName = currentPath;
        return JsonUtility.FromJson<sSaveFile>(File.ReadAllText(filePath));
    }

    public void Save()
    {
        int currentControllerIndex = 0;
        sSaveFile save = new sSaveFile();
        save.sBackend = backendManager.Save();
        save.sViews = new List<sView>();
        save.sConnections = new List<sConnection>();
        Dictionary<INode, string> controllerIDs = new Dictionary<INode, string>();

        //iterate and save controllers positions
        foreach (KeyValuePair<IFunction, List<GameObject>> entry in viewManager.views)
        {
            sView view = new sView();
            view.viewName = entry.Key.Name;
            view.sControllers = new List<sController>();
            foreach (GameObject obj in entry.Value)
            {
                if (obj == null)
                {
                    continue;
                }

                sController controller = new sController();
                string newId = "#" + currentControllerIndex;
                controller.controllerId = newId;
                var nodeController = obj.GetComponent<NodeBlockController>();
                controllerIDs.Add(nodeController.nodeBlock, newId);
                currentControllerIndex++;
                controller.positionOnScene = nodeController.transform.position;
                controller.creatorId = nodeController.nodeBlock.CreatorId;
                controller.isStartNode = nodeController.isStartNodeBlock;
                if (nodeController.nodeBlock is IInputNode inputNode)
                {
                    controller.inputValue = new sInputValue();
                    controller.inputValue.value = inputNode.Value;
                    controller.inputValue.type = inputNode.Type.TypeName;
                }
                else
                {
                    controller.inputValue = null;
                }
                view.sControllers.Add(controller);
            }
            save.sViews.Add(view);
        }

        //iterate and save connections
        foreach (KeyValuePair<INode, string> entry in controllerIDs)
        {
            int inputIndex = 0;
            foreach (IConnection con in entry.Key.InputsList)
            {
                if (con.Connected != null)
                {
                    sConnection connection = new sConnection();
                    connection.controllerIdFirst = entry.Value;
                    connection.inputIndex = inputIndex;

                    INode controllerSecond = con.Connected.ParentNode;
                    connection.controllerIdSecond = controllerIDs[controllerSecond];
                    connection.outputIndex = controllerSecond.OutputsList.IndexOf(con.Connected);

                    save.sConnections.Add(connection);
                }
                inputIndex++;
            }
        }

        //parse to json
        string json = JsonUtility.ToJson(save);
        //Debug.Log(json);
        WriteToFile(json);
    }

    public void Load()
    {
        sSaveFile save = ReadFromJsonFromFile();
        // Debug.Log(save);
        if (save == null)
        {
            throw new Exception("Load file fail.");
        }
        viewManager.DeleteAllView();
        viewManager.actualView = null;
        viewManager.views.Clear();
        backendManager.Load(save.sBackend);

        Dictionary<string, INode> controllerIDs = new Dictionary<string, INode>();

        //create gameobjects with controllers
        foreach (sView view in save.sViews)
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
            else if (function == null)
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

                INode node;
                if (view.viewName == "setup" && controller.isStartNode)
                {
                    node = backendManager.Setup.StartNode;
                }
                else if (view.viewName == "loop" && controller.isStartNode)
                {
                    node = backendManager.Loop.StartNode;
                }
                else if (controller.isStartNode)
                {
                    node = function.StartNode;
                }
                else
                {
                    node = backendManager.InstanceCreator.CreateNodeInstance(controller.creatorId, function);
                }

                GameObject nodeBlockObject;
                if (node is IInputNode inputNode)
                {
                    inputNode.SetValue(new InputNodeValueDto
                    {
                        Type = Backend.Type.TypeConverter.ToIType(controller.inputValue.type),
                        Value = controller.inputValue.value,
                    });
                    nodeBlockObject = nodeBlockManager.SpawnNodeBlockWithoutValidation(nodeBlockManager.nodeBlockInputPrefab);
                }
                else
                {
                    nodeBlockObject = nodeBlockManager.SpawnNodeBlockWithoutValidation(nodeBlockManager.nodeBlockPrefab);
                }

                nodeBlockObject.transform.position = controller.positionOnScene;

                if (controller.isStartNode)
                {
                    //startnode 
                    nodeBlockObject.GetComponent<NodeBlockController>().isStartNodeBlock = true;
                }

                nodeBlockObject.GetComponent<NodeBlockController>().InstantiateNodeBlockController(node);
                controllerIDs.Add(controller.controllerId, node);
                i++;
            }
        }

        //connect them;
        foreach (sConnection con in save.sConnections)
        {
            INode controllerFirst = controllerIDs[con.controllerIdFirst];
            INode controllerSecond = controllerIDs[con.controllerIdSecond];
            Debug.Log(controllerFirst.NodeName + ", " + controllerSecond.NodeName);
            Debug.Log(con.outputIndex + "," + con.inputIndex);
            controllerFirst.InputsList[con.inputIndex].Connect(controllerSecond.OutputsList[con.outputIndex]);
        }

        viewManager.ChangeView(backendManager.Setup);
    }


}
