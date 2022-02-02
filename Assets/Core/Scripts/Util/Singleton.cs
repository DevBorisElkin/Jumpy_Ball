using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    #region Singleton
        
    public static T Instance;
    public virtual void Awake() => InitSingleton();

    void InitSingleton()
    {
        if (Instance == null)
        {
            Instance = (T) this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    #endregion
}
