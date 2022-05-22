using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "QuestActor", order =1)]
public class QuestActor : ScriptableObject
{
    [System.Serializable]
    public struct Mision
    {
        public int ID;
        public string name;
        public string description;
        public QuestType questType;

        [System.Serializable]
        public enum QuestType
        {
            Recoleccion,
            Matar,
            Entrega
        }

        //Tambien puede usarse este tipo de mision para hacer que el jugador deba ir hasta cierto lugar o persona  a la que debe llevar X item
        //(con esto tambien logramos crear una mision de "Entrega" + "Recoleccion de Items")
        [Header("Misiones de Recoleccion")]
        public bool diferentesItems;
        public bool seQuedaConItems;
        public List<ItemsRecoger> datos;

        [System.Serializable]
        public struct ItemsRecoger
        {
            public string nombre;
            public int cantidad;
            public int itemId;
        }

        [Header("Misiones Matar")]
        public int cantidad;
        public int enemyID;

        [Header("Recompensas")]
        public int gold;
        public int xp;
        public bool hasSpecialR;
        public SpecialRewards[] specialR;

        [System.Serializable]
        public struct SpecialRewards
        {
            public string nombre;
            public GameObject reward;
        }
    }

    public Mision[] misions;
    public float precisionDestino = 1.5f;
}
