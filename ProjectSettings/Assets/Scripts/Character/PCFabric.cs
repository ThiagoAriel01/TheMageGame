using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCFabric : MonoBehaviour
{
    private static PCFabric _instance = null;

    private PC playableCharacter = null;

    private void InitializeSingleton()
    {
        if (_instance == null)
        {
            _instance = this;

            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(_instance);
        }
    }

    public static PCFabric GetInstance()
    {
        return _instance;
    }

    void Awake()
    {
        InitializeSingleton();
    }

    public void SetClass(PC playerClass)
    {
        playableCharacter = playerClass;
    }

    public PC GetClass()
    {
        return playableCharacter;
    }

}
