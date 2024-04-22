using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Backend.API;
using Backend.InOut;

public class ConnectionFake : IConnection
{
    public IConnection Connected { get; set; }

    public InOutType InOutType { get; set; }

    public string InOutName { get; set; }

    public ConnectionPoint UIPoint { get; set; }

    public void Connect(IConnection iConnection)
    {
        Connected = iConnection;
    }

    public void Disconnect()
    {
        Connected = null;
    }
}
