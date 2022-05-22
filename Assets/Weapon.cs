using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool entra = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            entra = true;
    }

    void OnTriggerExit(){
        entra = true;
    }
}
