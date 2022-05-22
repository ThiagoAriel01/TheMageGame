using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : Enemy
{
    void Start() {
        this.stats.damage = 15;
        this.stats.nombre = "Vampire";
        this.stats.vida = 50;
        this.stats.defensa = 5;
        this.stats.speed = 22;
    }

    public override void HabilidadSpecial() { }
}
