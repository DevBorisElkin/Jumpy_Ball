using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WorldComponent))]
public class Wall : MonoBehaviour
{
    public bool upper;
    
    [HideInInspector] public WorldComponent worldComponent;

    private void Awake()
    {
        InitDependencies();
    }

    void InitDependencies()
    {
        worldComponent = GetComponent<WorldComponent>();
        worldComponent.scale = transform.localScale;
    }

    public void ConfigureWall(Vector2 scale)
    {
        transform.localScale = scale;
        worldComponent.scale = scale;
    }
}
