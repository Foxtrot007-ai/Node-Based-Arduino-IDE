using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memento
{
    public IAction State { get; set; }
    public Memento()
    {
    }
    public IAction GetState()
    {
        return this.State;
    }
    public void SetState(IAction State)
    {
        this.State = State;
    }
}

