using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartelEliminacion : MonoBehaviour
{
    [SerializeField]private Inventory inv;
    public Slider slider;
    public Text cantText;

    void Update()
    {
        if (this.gameObject.activeInHierarchy){ 
            slider.maxValue = inv.OSC;
            cantText.text = slider.value.ToString();
        }
    }
    public void Aceptar()
    {
        inv.EliminarItem(inv.OSID, Mathf.RoundToInt(slider.value));
        Debug.Log("Se acepto eliminar: " + Mathf.RoundToInt(slider.value) + " items con ID: " + inv.OSID);
        slider.value = 1;
        this.gameObject.SetActive(false);
    }

    public void Cancelar(){
        slider.value = 1;
        this.gameObject.SetActive(false);
    }
}
