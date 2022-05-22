using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumible : BaseItem
{
    public Consumible()
    {
        this.description = "Incrementa tu vida max, no cura";
        this.icon=null;
        this.name="Potion Increase MAX HP";
        this.type = "";
        stat.damage=0;
        stat.nombre="";
        stat.vida=0;
        stat.CapLife+=5;
        stat.SafeLife =0;
        stat.defensa =0;
        stat.speed =0;
    }

    /*public override void Usar(){
        pc.IncreaseCapHP(stat.CapLife);}*/
}
