using System;
using System.Collections;
using System.Collections.Generic;

public enum NodeBlockTypes
{
    Function, // 0 or 1 element in nextBlockList, 0 or more elements in inputList, 0 or 1 output;
    Variable, // nextBlockList Empty, 1 output, (its name and value needs to be declared by user nodeblock)
    Constants, // nextBlockList Empty, 1 output, (implemented with arduino reference
    Structure, // 0 or more element in nextBlockList (for example for loop has loop body and next function after loop end)
}

public class NodeBlock {

    private NodeBlockTypes type;
    private string name;
    public NodeBlock[] nextBlockList; //next nodeblock in control flow
    public NodeBlock[] inputList; //points to nodeblock that give its output or null
    public NodeBlock output; //points to nodeblock that take its input or null

    public NodeBlock(string name, NodeBlockTypes type)
    {
        this.name = name;
        this.type = type;
    }

    public string getName()
    {
        return name;
    }

}
