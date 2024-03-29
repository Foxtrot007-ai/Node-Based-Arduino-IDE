using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceListManager : ListManager
{
    protected override List<NodeBlock> GetNodeBlocks()
    {
        return nodeBlockManager.SearchNodeBlocks(this, lastInput);
    }
}
