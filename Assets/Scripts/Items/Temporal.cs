using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temporal : BaseItem
{
    public Temporal()
    {
        this.description = "Item de uso Temporal";
        this.icon=null;
        this.name ="Pocion Regeneracion";
        this.type = "";
        stat.damage = 0;
        stat.nombre = "";
        stat.vida = 0;
        stat.CapLife=0;
        stat.SafeLife = 0;
        stat.defensa = 0;
        stat.speed = 0;
    }

    bool inUse = false;
    int timer;

   /*public override void Usar()
    {
        if (!inUse)
        {
            //aumenta la vida durante segundos
        }
    }*/
}
