using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparador : MonoBehaviour
{
    [SerializeField] private GameObject bala;

    public void Disparar()  {
        GameObject.Instantiate(bala, this.transform.position, this.transform.rotation);
    }
}
