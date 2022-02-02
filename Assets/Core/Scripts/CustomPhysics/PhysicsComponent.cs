using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PhysicsComponent : WorldComponent
{
    public float mass = 1f;
    public bool clampMaxFallingSpeed;

    public bool hasBottomSupport;

    [ReadOnly] public Vector2 vecocity;

    public void Normalize(WorldComponent second)
    {
        hasBottomSupport = true;
        vecocity = new Vector3(0, 0, 0);
        FixYPosition(this, second);
    }
}
