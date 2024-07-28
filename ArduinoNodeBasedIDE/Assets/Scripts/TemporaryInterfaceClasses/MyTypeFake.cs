using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Backend.API;
using Backend.Type;

public class MyTypeFake : IMyType
{
    public string TypeName {get;set;}

    public EType EType { get; set; }
}
