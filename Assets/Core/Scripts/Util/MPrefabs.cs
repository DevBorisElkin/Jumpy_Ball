using UnityEngine;

public class MPrefabs : MonoBehaviour
{
    public static MPrefabs Instance;

    private void Awake() => InitSingleton();

    void InitSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public InputService_Mobile InputServiceMobilePrefab;
    public InputService_Desktop InputServiceDesktopPrefab;
}
