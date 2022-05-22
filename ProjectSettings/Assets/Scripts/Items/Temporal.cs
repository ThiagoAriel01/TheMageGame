using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temporal : BaseItem
{
    public Temporal()
    {
        this.statsI.description="Item de uso Temporal";
        this.statsI.points = 5;
        this.statsI.icon;
        this.statsI.name="Pocion Regeneracion";
    }

    bool inUse = false;      

    public override void Usar()
    {
        if (!inUse)
        {

        }
    }

    public override void OnTriggerEnter() { Usar(); }
}
