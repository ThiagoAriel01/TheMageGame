using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BaseDatos", menuName = "Intentario/Lista", order =1)]
public class InventoryData : ScriptableObject
{
    [System.Serializable]
    public struct ObjetoInventario
    {
        public string nombre;
        public int ID;
        public Sprite icon;
        public bool acumulable;
        public string descripcion, Void;
        public Tipo tipo;

    }
    public enum Tipo
    {
        consumible,
        equipable,
        temporal
    }

    public ObjetoInventario[] baseDatos;
}
