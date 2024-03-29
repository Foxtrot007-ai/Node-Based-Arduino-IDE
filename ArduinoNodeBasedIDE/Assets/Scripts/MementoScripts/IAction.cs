using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    public void UndoAction();
    public void RedoAction();
}
