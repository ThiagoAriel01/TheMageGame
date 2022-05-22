using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    public override void TakeDamage(int damage)
    {
        if(this.stats.vida > 0)
        {
            int auxDemage = damage - this.stats.defensa;
            this.stats.vida -= auxDemage;
        }
    }
}
