using UnityEngine;

[RequireComponent(typeof(PhysicsComponent))]
public class Player : MonoBehaviour
{
    public PhysicsEngine physicsEngine;
    private PhysicsComponent _physicsComponent;
    private InputService inputService;
    void Start()
    {
        _physicsComponent = GetComponent<PhysicsComponent>();
        InitInput();
        SubscribeToEvents(true);
    }

    void InitInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        inputService = Instantiate(MPrefabs.Instance.InputServiceDesktopPrefab);
#elif UNITY_ANDROID || Unity_iOS
        inputService = Instantiate(MPrefabs.Instance.InputServiceMobilePrefab);
#endif
    }

    void SubscribeToEvents(bool state)
    {
        if (state) inputService.PlayerMakesScreenTap += OnPlayerClick;
        else inputService.PlayerMakesScreenTap -= OnPlayerClick;
    }

    void OnPlayerClick()
    {
        if(CanFlipPhysics())
            physicsEngine.gravityForce.Value = -physicsEngine.gravityForce.Value;
    }

    bool CanFlipPhysics()
    {
        return _physicsComponent.hasBottomSupport && _physicsComponent.hasUpperSupport;
    }

    private void OnDestroy() => SubscribeToEvents(false);
}
