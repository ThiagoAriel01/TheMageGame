using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumible : BaseItem
{
    public Consumible()
    {
        this.statsI.description="Incrementa tu vida max, no cura";
        this.statsI.points = 5;
        this.statsI.icon;
        this.statsI.name="Potion Increase MAX HP";
    }
    public override void Usar()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerActor>().playableCharacter;
        pc.IncreaseCapHP(this.points);
    }

    public override void OnTriggerEnter() { Usar(); }
}
