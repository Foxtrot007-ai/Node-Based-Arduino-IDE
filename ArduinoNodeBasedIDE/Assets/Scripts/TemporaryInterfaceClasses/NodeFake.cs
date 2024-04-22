using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Backend;
using Backend.API;
using Backend.Node;

public class NodeFake : INode
{
    public List<IConnection> InputsList { get; set; }

    public List<IConnection> OutputsList { get; set; }

    public NodeType NodeType { get; set; }

    public string NodeName { get; set; }

    public bool IsDeleted { get; set; }

    public void Delete()
    {
        IsDeleted = false;
    }
}
