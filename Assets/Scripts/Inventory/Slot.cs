using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public GameObject item;
    public int ID;
    public string type;
    //public string name;
    public string description;
    public Sprite icon;

    public bool empty;

    public Transform slotImageGameObject;

    public void Start(){
        slotImageGameObject = transform.GetChild(0);
    }

    public void UpdateSlot(){
        slotImageGameObject.GetComponent<Image>().sprite = icon;
    }

    public void UsarItem(){
        item.GetComponent<BaseItem>().ItemUsage();
    }

    public void OnPointerClick(PointerEventData pointerEventData){
        UsarItem();
    }
}
