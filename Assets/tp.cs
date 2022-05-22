using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tp : MonoBehaviour
{
    public Transform posisionATepear;
    public GameObject activarParticula;
    public GameObject activarParticula2;
    public GameObject activarTp;
    [HideInInspector]
    public CharacterController cc;

    void Start()
    {
        cc = ManagerMagos.mago.GetComponent<CharacterController>();
        activarTp.SetActive(false);
        activarParticula.SetActive(false);
        activarParticula2.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activarParticula.SetActive(true);
            activarParticula2.SetActive(true);
            Invoke("teleport",6f);
        }
    }

    void teleport()
    {
        activarTp.SetActive(true);
    }
}
