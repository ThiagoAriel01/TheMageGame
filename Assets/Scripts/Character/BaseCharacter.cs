using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class BaseCharacter
{
    public Stats stats;
    public struct Stats{
        public int damage;
        public string nombre;
        public int vida;
        public int CapLife;
        public int SafeLife;
        public int defensa;
        public int speed;
    }

    public virtual void Move(){}
    public virtual void Atacar(){}
    public virtual void TakeDamage(int damage) { }
    virtual public void IncreaseHP(int points) { }
    virtual public void IncreaseDefensa(int point) { }
    virtual public void IncreaseCapHP(int points) { }
    public virtual void Morir(){}
    public virtual void Bloquear(){}
    virtual public int GetVida() { return stats.vida; }
    virtual public int GetDamage() { return stats.damage; }
    virtual public int GetCapLife() { return stats.CapLife; }
    virtual public int GetSafeLife() { return stats.SafeLife; }
    virtual public int GetDefensa() { return stats.defensa; }
    virtual public int GetSpeed() { return stats.speed; }
    public virtual void OnTriggerEnter(Collider collision){}
    //virtual public void AddItem(BaseItem item) { }
}
