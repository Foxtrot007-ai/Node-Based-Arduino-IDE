using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NodeBlockManager : MonoBehaviour
{
    public List<NodeBlock> languageReferenceList;

    public string languageReferenceFile = "Assets/Resources/languageReference.txt";

    public NodeBlock lineReader(string line)
    {
        var parts = line.Split(';');
        string name = parts[0];
        NodeBlockTypes type = (NodeBlockTypes)Enum.Parse(typeof(NodeBlockTypes), parts[1], true);
        int inputListSize = int.Parse(parts[2]);
        int outputListSize = int.Parse(parts[3]);
        NodeBlock block = new NodeBlock(name, type, inputListSize, outputListSize);
        return block;
    }

    // Start is called before the first frame update
    void Start()
    {
        languageReferenceFile = "Assets/Resources/languageReference.txt";
        languageReferenceList = new List<NodeBlock>();
        var stream = new StreamReader(languageReferenceFile);
        while (!stream.EndOfStream)
            languageReferenceList.Add(lineReader(stream.ReadLine()));

        foreach(NodeBlock function in languageReferenceList)
            Debug.Log(function.GetName());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
