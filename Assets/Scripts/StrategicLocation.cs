using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StrategicLocation : Location
{
    // Update is called once per frame
    void Update()
    {
        GenerateResource();
    }

    protected abstract void GenerateResource();
}
