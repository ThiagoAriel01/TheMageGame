using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public enum QuestType
    {
        Recoleccion,
        Matar,
        Entrega
    }

    public int ID;
    public bool complete=false;
    public string name;
    public string description;
    public QuestType questType;

    [Header("Para Destino")]
    public GameObject destino;

    [Header("Para Enemigos")]
    public int idEnemigo;
    public int totalAmount;
    public int currentAmount;

    [Header("Para Items")]
    public bool retieneItems;
    public List<QuestActor.Mision.ItemsRecoger> itemsRecogers = new List<QuestActor.Mision.ItemsRecoger>();
}

public class QuestData
{
    public List<Quest> questList;
}