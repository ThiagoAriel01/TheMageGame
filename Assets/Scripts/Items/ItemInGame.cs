using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInGame : MonoBehaviour
{
    public int cantidad, id;
    public Inventory inv;
    private void Update()
    {
        transform.Rotate(0, 0, 0.28f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            inv.AddItem(id, cantidad);
            transform.SetParent(other.transform);
            inv.itemInGames.Add(this);
            gameObject.SetActive(false);
        }
    }
}
