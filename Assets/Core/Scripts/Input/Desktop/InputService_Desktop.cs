using UnityEngine;

public class InputService_Desktop : InputService
{
    public override void ManageInput() => HandlePlayerClicks();

    void HandlePlayerClicks()
    {
        if (Input.GetMouseButtonDown(0)) PlayerMakesScreenTap?.Invoke();
    }
}
