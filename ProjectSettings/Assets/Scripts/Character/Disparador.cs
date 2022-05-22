using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparador : MonoBehaviour
{
    [SerializeField] private Bala bala;
    //Vector3 newRotation;

    public void Disparar()
    {
        /*newRotation = new Vector3(-90, this.transform.rotation.y, this.transform.rotation.z); 
        GameObject.Instantiate(bala, this.transform.position, newRotation);*/
        GameObject.Instantiate(bala, this.transform.position, this.transform.rotation);
    }
}
