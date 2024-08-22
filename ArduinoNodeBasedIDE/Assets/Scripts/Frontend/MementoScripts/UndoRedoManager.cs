using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoRedoManager : MonoBehaviour
{
    //Memento 
    public Originator originator = new Originator();
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            originator.Undo();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            originator.Redo();
        }
    }
}
