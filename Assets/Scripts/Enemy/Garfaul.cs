using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garfaul : Enemy
{
    void Start() {
        this.stats.damage = 20;
        this.stats.nombre = "Garfaul";
        this.stats.vida = 200;
        this.stats.defensa = 10;
        this.stats.speed = 20;
    }

    public override void HabilidadSpecial() { }
}
