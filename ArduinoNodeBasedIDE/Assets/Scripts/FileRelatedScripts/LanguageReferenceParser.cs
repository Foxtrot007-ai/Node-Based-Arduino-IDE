using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LanguageReferenceParser
{
    public string languageReferenceFile = "Assets/Resources/languageReference.txt";
    public NodeBlock lineReader(string line)
    {
        var parts = line.Split(';');
        string name = parts[0];
        NodeBlockTypes type = (NodeBlockTypes)Enum.Parse(typeof(NodeBlockTypes), parts[1], true);
        //input parse
        var inputparts = parts[2].Split('/');
        int inputListSize = int.Parse(inputparts[0]);

        //output parse
        var outputparts = parts[3].Split('/');
        int outputListSize = int.Parse(outputparts[0]);

        NodeBlock block = new NodeBlock(name, type, inputListSize, outputListSize);

        Debug.Log(outputListSize + "," + outputparts[0]);
        for (int i = 0; i < inputListSize; i++)
        {
            block.SetInputType(inputparts[i + 1], i);
        }

        if (outputListSize > 0)
        {
            block.SetOutputType(outputparts[1]);
        }


        return block;
    }
    public List<NodeBlock> loadReferenceList()
    {
        var stream = new StreamReader(languageReferenceFile);
        List<NodeBlock> list = new List<NodeBlock>();
        while (!stream.EndOfStream)
            list.Add(lineReader(stream.ReadLine()));

        return list;
    }
}
