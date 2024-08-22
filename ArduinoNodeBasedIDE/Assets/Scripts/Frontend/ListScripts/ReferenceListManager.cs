using Backend.API;
using System.Collections.Generic;

//Script for Global Variable List UI Control
public class ReferenceListManager : ListManager
{
    //overrided class methods
    protected override List<ITemplate> GetTemplates()
    {
        return nodeBlockManager.SearchNodeBlocks(this, lastInput);
    }

    public override void UpdateContent()
    {
        DestroyContent();
        AddContentTemplates();
    }
}
