using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPEndGame : MonoBehaviour
{
    public ManagerScenas managerScenas;
    private void OnTriggerEnter(Collider other){
        Invoke("EndGame", 6);
    }

    void EndGame(){
        managerScenas.EndGamePerras();
    }
}
