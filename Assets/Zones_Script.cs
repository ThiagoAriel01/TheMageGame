using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Zones_Script : MonoBehaviour
{
    public TextMeshProUGUI textActrivar;
    void Start()
    {
        textActrivar.gameObject.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            textActrivar.gameObject.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            textActrivar.gameObject.SetActive(false);
    }
}
