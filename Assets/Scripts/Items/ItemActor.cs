using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class ItemActor : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public Text cantidadText;
    public int ID,cantidad = 1;
    public bool acumulable;
    public GameObject description_;
    public Button button;
    public Text nombre_, dato_;
    public Vector3 offset;
    public InventoryData data;

    void Start()
    {
        acumulable = data.baseDatos[ID].acumulable;//null reference
        button = GetComponent<Button>();
        description_ = Inventory.description;
        nombre_ = description_.transform.GetChild(0).GetComponent<Text>();
        dato_ = description_.transform.GetChild(1).GetComponent<Text>();
        description_.SetActive(false);
        if (!description_.GetComponent<Image>().enabled){
            description_.GetComponent<Image>().enabled = true;
            nombre_.enabled = true;
            dato_.enabled = true;
        }
    }
    void Update()
    {
        if (transform.parent.GetComponent<Image>() != null)//null reference
            transform.parent.GetComponent<Image>().fillCenter = true;
        
        cantidadText.text = cantidad.ToString();
        if (transform.parent == Inventory.canvas)
            description_.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        description_.SetActive(true);
        nombre_.text = data.baseDatos[ID].nombre;
        dato_.text = data.baseDatos[ID].descripcion;
        description_.transform.position = transform.position + offset;
    }

    public void OnPointerExit(PointerEventData eventData){
        description_.SetActive(false);
    }
}
