using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScenas : MonoBehaviour
{
    public void DieManager()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void EndGamePerras()
    {
        SceneManager.LoadScene("EndGame");
    }
}
