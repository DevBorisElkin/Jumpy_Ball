using UnityEngine;

public class InputService_Mobile : InputService
{
    public override void ManageInput() => HandleTapInput();
    void HandleTapInput()
    {
        if(Input.touchCount == 0) return;
        if(Input.GetTouch(0).phase == TouchPhase.Began) PlayerMakesScreenTap?.Invoke();
    }
}
