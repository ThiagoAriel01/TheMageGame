using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : BaseItem
{
    public Equipable()
    {
        this.description = "Item Equipable";
        this.icon=null;
        this.name ="Armadura ";
        this.type = "";
        stat.damage = 0;
        stat.nombre = "";
        stat.vida = 0;
        stat.CapLife = 0;
        stat.SafeLife = 0;
        stat.defensa += 10;
        stat.speed = 0;
    }

    /*public override void Usar() { EquipItem(); }

    private void EquipItem() { 
        pc.IncreaseDefensa(stat.defensa);}*/

}
