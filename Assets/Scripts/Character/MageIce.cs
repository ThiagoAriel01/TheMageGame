using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MageIce : PC
{
    public MageIce()
    {
        this.stats.damage = 16;
        this.stats.nombre = "MageIce";
        this.stats.vida = 55;
        this.stats.CapLife = 55;
        this.stats.SafeLife = 0;
        this.stats.defensa = 7;
        this.stats.speed = 5;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (this.stats.vida > 0)
        {
            int auxDamage = damage - this.stats.defensa;
            this.stats.vida -= auxDamage;
            //GameObject.Find("Character").GetComponent<PlayerActor>().healthBar.SetHealth(this.stats.vida);
        }
    }

}
