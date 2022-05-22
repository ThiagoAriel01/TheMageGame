using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrok : Enemy
{
    void Start() {
        this.stats.damage = 7;
        this.stats.nombre = "Warrok";
        this.stats.vida = 50;
        this.stats.defensa = 20;
        this.stats.speed = 5;
    }

    public override void HabilidadSpecial() { }
}
