using Backend.API;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceListManager : ListManager
{
    protected override List<IFunctionManage> GetFunctions()
    {
        return nodeBlockManager.SearchNodeBlocks(this, lastInput);
    }

    protected override void UpdateContent()
    {
        DestroyContent();
        AddContentFunctions();
    }
}
