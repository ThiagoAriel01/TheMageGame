using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMagos : MonoBehaviour
{
    public delegate void MagosDelegate(PlayerActor actor);
    public MagosDelegate onMagoChanged;
    public PlayerActor[] arrayMagos;
    public int magoActual = 0, magoAnterior;
    public bool cambiando;
    public float delay;
    static private ManagerMagos instance;
    static public PlayerActor mago { get { return instance.arrayMagos[instance.magoActual]; } }

    void Awake() {
        instance = this;
    }
    static public ManagerMagos Intance
    {
        get { return instance; }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3)&&!cambiando)
        {
            cambiando = true;
            arrayMagos[magoActual].habilitys.CambioEstancia();
            Invoke("CambiarAhora", delay);
            magoActual++;
            if (magoActual>=arrayMagos.Length)
                magoActual = 0;
        }       
    }

    void CambiarAhora()
    {
        cambiando = false;   
        arrayMagos[magoAnterior].gameObject.SetActive(false);
        arrayMagos[magoActual].gameObject.SetActive(true);
        arrayMagos[magoActual].GetComponent<CharacterController>().enabled=false;
        arrayMagos[magoActual].transform.position = arrayMagos[magoAnterior].transform.position;
        arrayMagos[magoActual].camara.transform.localRotation = arrayMagos[magoAnterior].camara.transform.localRotation;
        arrayMagos[magoActual].GetComponent<CharacterController>().enabled = true;
        arrayMagos[magoActual].transform.rotation = arrayMagos[magoAnterior].transform.rotation;
        arrayMagos[magoAnterior].habilitys.CambiarParticulas();
        magoAnterior = magoActual;

        onMagoChanged?.Invoke(arrayMagos[magoActual]);
    }
}
