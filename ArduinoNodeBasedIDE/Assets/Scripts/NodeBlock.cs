using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeBlockTypes
{
    Function, //0 or 1 element in nextBlockList, 0 or more elements in inputList, 0 or 1 output;
    Variable, //previousBlockList and nextBlockList Empty, 1 output, (its name and value needs to be declared by user nodeblock)
    Constant, //previousBlockList  and nextBlockList Empty, 1 output, (implemented with arduino reference
    Structure, // 0 or more element in nextBlockList (for example for loop has loop body and next function after loop end)
}

public class NodeBlock : ICloneable{

    private string name;
    private NodeBlockTypes type;

    public NodeBlock previousBlock; //previous nodeblock in control flow
    public bool hasPreviousBlock;

    public NodeBlock[] nextBlockList; //next nodeblocks in control flow
    public int nextBlockListSize;

    public NodeBlock[] inputBlockList; //points to nodeblock that give its output or null
    public int inputBlockListSize;

    public NodeBlock outputBlock; //points to nodeblock that take its input or null
    public bool returnOutputBlock;

    public NodeBlock(string name, NodeBlockTypes type, int numberOfInput, int output)
    {
        this.name = name;
        this.type = type;

        inputBlockListSize = numberOfInput;
        inputBlockList = new NodeBlock[numberOfInput];

        returnOutputBlock = (output == 1);

        if (type.Equals(NodeBlockTypes.Function))
        {
            hasPreviousBlock = true;
            nextBlockListSize = 1;
            nextBlockList = new NodeBlock[1];
        }

        if (type.Equals(NodeBlockTypes.Structure))
        {
            hasPreviousBlock = true;
            if (name == "if" || name == "for")
            {
                nextBlockListSize = 2;
                nextBlockList = new NodeBlock[2];
            }
        }
        
    }

    public object Clone()
    {
        NodeBlock temp = new NodeBlock(name, type, inputBlockListSize, returnOutputBlock ? 1 : 0);
        return temp;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public string GetName()
    {
        return name;
    }

    public void SetPreviousBlock(NodeBlock prev)
    {
        this.previousBlock = prev;
    }
    
    public NodeBlock GetPreviousBlock()
    {
        return previousBlock;
    }

    public void SetNextBlock(NodeBlock next, int nodeIndex)
    {
        if(nodeIndex >= 0 && nodeIndex < nextBlockListSize)
        {
            nextBlockList[nodeIndex] = next;
        }
    }

    public NodeBlock GetNextBlock(int nodeIndex)
    {
        if (nodeIndex >= 0 && nodeIndex < nextBlockListSize)
        {
            return nextBlockList[nodeIndex];
        }
        else
        {
            return null;
        }
    }

    public void SetInputBlock(NodeBlock input, int inputIndex)
    {
        if (inputIndex >= 0 && inputIndex < inputBlockListSize)
        {
            inputBlockList[inputIndex] = input;
        }
    }

    public NodeBlock GetInputBlock(int inputIndex)
    {
        if (inputIndex >= 0 && inputIndex < inputBlockListSize)
        {
            return inputBlockList[inputIndex];
        }
        else
        {
            return null;
        }
    }

    public void SetOutputBlock(NodeBlock output)
    {
        this.outputBlock = output;
    }

    public NodeBlock GetOutputBlock()
    {
        return outputBlock;
    }

    public NodeBlockTypes GetNodeBlockType()
    {
        return type;
    }
}
