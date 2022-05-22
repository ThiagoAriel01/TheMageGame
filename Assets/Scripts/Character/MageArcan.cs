using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageArcan : PC
{
    public MageArcan()
    {
        this.stats.damage = 15;
        this.stats.nombre = "MageArcan";
        this.stats.vida = 50;
        this.stats.CapLife = 50;
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
