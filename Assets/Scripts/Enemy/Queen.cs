using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Enemy
{
    void Start() {
        this.stats.damage = 17;
        this.stats.nombre = "Queen";
        this.stats.vida = 100;
        this.stats.defensa = 5;
        this.stats.speed = 22;
    }

    public override void HabilidadSpecial() { }
}
