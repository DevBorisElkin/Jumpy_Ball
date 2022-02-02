using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BorderPoints
{
    public Vector3 _1;
    public Vector3 _2;
    public Vector3 _3;
    public Vector3 _4;
    public Vector3 _5;
    public Vector3 _6;
    public Vector3 _7;
    public Vector3 _8;

    public BorderPoints(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Vector3 v5, Vector3 v6, Vector3 v7, Vector3 v8)
    {
        _1 = v1;
        _2 = v2;
        _3 = v3;
        _4 = v4;
        _5 = v5;
        _6 = v6;
        _7 = v7;
        _8 = v8;
    }
}
