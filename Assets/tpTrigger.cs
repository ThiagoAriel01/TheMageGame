using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpTrigger : MonoBehaviour
{
    public tp tp;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tp.cc.enabled = false;
            other.gameObject.transform.position = tp.posisionATepear.position;
            tp.cc.enabled = true;
        }
    }
}
