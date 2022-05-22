using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasite : Enemy
{
    void Start() {
        this.stats.damage = 10;
        this.stats.nombre = "Parasite";
        this.stats.vida = 46;
        this.stats.defensa = 5;
        this.stats.speed = 10;
    }

    public override void HabilidadSpecial() { }
}
