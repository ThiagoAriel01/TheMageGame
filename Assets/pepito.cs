using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pepito : MonoBehaviour
{
    void Start()
    {
        Invoke("pepito2",3f);
    }

    void pepito2()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
