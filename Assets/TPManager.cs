using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPManager : MonoBehaviour
{
    public GameObject tpActivar;
    public GameObject sword,daga;
    void Start(){
        tpActivar.SetActive(false);
    }

    public void RecibirString(string tipoPersonaje)
    {
        if (tipoPersonaje == "Rey")
            tpActivar.SetActive(true);

        if (tipoPersonaje == "Queen")
            sword.SetActive(true);

        if (tipoPersonaje == "Maw")
            daga.SetActive(true);
    }
}
