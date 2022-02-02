using UnityEngine;

public class Player : MonoBehaviour
{
    private InputService inputService;
    void Start()
    {
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
        Debug.Log("Player Clicked!");
    }

    private void OnDestroy() => SubscribeToEvents(false);
}
