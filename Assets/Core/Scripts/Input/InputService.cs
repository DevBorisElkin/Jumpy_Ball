using System;
using UnityEngine;

public class InputService : MonoBehaviour, IInput
{
    public Action PlayerMakesScreenTap;
    
    public virtual void Update() => ManageInput();

    public virtual void ManageInput()
    {
        throw new NotImplementedException();
    }
}
