using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : BaseCharacter
{
    //public override void TakeDamage() { }
    public override void Morir() { }
    public override void Bloquear() { }    
    public virtual void HabilidadSpecial() { }
}
