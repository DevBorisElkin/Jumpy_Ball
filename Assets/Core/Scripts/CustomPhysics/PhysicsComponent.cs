using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UniRx;
using UnityEngine;

public class PhysicsComponent : WorldComponent
{
    public float mass = 1f;
    public bool clampMaxFallingSpeed;

    public bool hasBottomSupport;
    public bool hasUpperSupport;

    [ReadOnly] public Vector2 vecocity;

    public void Normalize(WorldComponent second)
    {
        hasBottomSupport = true;
        hasUpperSupport = true;
        vecocity = new Vector3(0, 0, 0);
        FixYPosition(this, second);
    }

    public void ResetSupportsData()
    {
        Debug.Log($"<b><color=#6556f9>[Supports data was cleared]</color></b>"); 
        hasBottomSupport = false;
        hasUpperSupport = false;
    }
}
