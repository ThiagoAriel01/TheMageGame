using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public abstract class BaseItem : MonoBehaviour
{
    protected StatsI statsI;
    protected struct StatsI
    {
        //public string description;
        public int points = 0;
        public Sprite icon;
        public string name;
    }

    void Awake()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerActor>().playableCharacter;
    }

    public virtual void Usar(){}

    /*public virtual void AddIcon(Sprite _icon)
    {
        statsI.icon = _icon;
    }*/

    public virtual void OnTriggerEnter() { }

    public virtual void DropItem() { }

}
