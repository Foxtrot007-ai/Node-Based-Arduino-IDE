using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Originator
{
    public Originator()
    {
    }
    private IAction _state;
    public IAction State
    {
        get { return _state; }
        set
        {
            _state = value;
            undoStates.Push(this.CreateMemento());
            redoStates.Clear();
        }
    }
    public Memento CreateMemento()
    {
        Memento memento = new Memento();
        memento.SetState(this._state);
        return memento;
    }
    public void RestoreMemento(Memento memento)
    {
        this._state = memento.GetState();
    }
  
    Stack<Memento> undoStates = new Stack<Memento>();
    Stack<Memento> redoStates = new Stack<Memento>();
    public void Undo()
    {
        if (undoStates.Count > 0)
        {
            Memento currentState = undoStates.Pop();
            redoStates.Push(currentState);
            currentState.State.UndoAction();
            Memento previousState = undoStates.Peek();
            this.RestoreMemento(previousState);
        }
    }
    public void Redo()
    {
        if (redoStates.Count > 0)
        {
            Memento futureState = redoStates.Pop();
            undoStates.Push(futureState);
            this.RestoreMemento(futureState);
            this._state.RedoAction();
        }
    }
}
