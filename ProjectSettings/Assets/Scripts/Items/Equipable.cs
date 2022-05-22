using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : BaseItem
{
    public Equipable()
    {
        this.statsI.description="Item Equipable";
        this.statsI.points = 20;
        this.statsI.icon;
        this.statsI.name="Armadura ";
    }

    public override void Usar() { EquipItem(points); }

    private void EquipItem(int points) { playableCharacter.SetTotalHP(points); }

    public override void OnTriggerEnter() { Usar(); }
}
