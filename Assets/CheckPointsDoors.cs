using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckPointsDoors : MonoBehaviour
{
    public GameObject LeftDoor, RigthDoor; // rigth 100 Y, left -100 Y
    public TextMeshProUGUI textoActivar;
    public static bool openDoor;

    Vector3 posisionFinalIzq, posisionFinalDerecha;
    void Start()
    {
        textoActivar.gameObject.SetActive(false);
        posisionFinalIzq = new Vector3(transform.rotation.x, transform.rotation.y - 100f, transform.rotation.z);
        posisionFinalDerecha = new Vector3(transform.rotation.x, transform.rotation.y + 100f, transform.rotation.z);       
    }

    void Update()
    {
        if (openDoor)
            OpenDoor();
        else
            CloseDoor();
    }

    private void OpenDoor()
    {
        LeftDoor.gameObject.transform.localRotation = Quaternion.Euler(posisionFinalIzq);
        RigthDoor.gameObject.transform.localRotation = Quaternion.Euler(posisionFinalDerecha);
    }
    private void CloseDoor()
    {
        LeftDoor.gameObject.transform.localRotation = Quaternion.identity;
        RigthDoor.gameObject.transform.localRotation = Quaternion.identity;
    }

    private void OnTriggerEnter(Collider other)
    {
        textoActivar.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        textoActivar.gameObject.SetActive(false);
    }
}
