using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PC : BaseCharacter
{
    //public void CreateInventoryData(){inventoryData = new InventoryData();}

    public void SetTotalHP(int hpPoints){stats.CapLife += hpPoints;}

    //public InventoryData GetInventoryData(){return inventoryData;}

    public override void IncreaseHP(int points)
    {
        if (stats.vida <= stats.CapLife)
            stats.vida += points;
        else 
            Debug.Log("HP is full");
    }

    public override void IncreaseCapHP(int points)
    {      
        stats.CapLife += points;
    }
    public override void IncreaseDefensa(int points)
    {
        stats.defensa += points;
    }
}
