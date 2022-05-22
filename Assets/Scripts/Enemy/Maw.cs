using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maw : Enemy
{
    void Start() {
        this.stats.damage = 7;
        this.stats.nombre = "Maw";
        this.stats.vida = 70;
        this.stats.defensa = 7;
        this.stats.speed = 5;
    }

    public override void HabilidadSpecial() { }
}
