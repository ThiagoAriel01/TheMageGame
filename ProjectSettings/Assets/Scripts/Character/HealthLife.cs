using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthLife : MonoBehaviour
{
    [SerializeField] float health = 100f;//PROBAR
    public Image barravida;

    void Update()
    {
        health = Mathf.Clamp(health, 0, 100);
        barravida.fillAmount = health / 100;
    }
}
