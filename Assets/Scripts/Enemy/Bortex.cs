using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bortex : Enemy
{
    void Start() {
        this.stats.damage = 8;
        this.stats.nombre = "Bortex";
        this.stats.vida = 50;
        this.stats.defensa = 7;
        this.stats.speed = 7;
    }

    public override void HabilidadSpecial() { }
}
