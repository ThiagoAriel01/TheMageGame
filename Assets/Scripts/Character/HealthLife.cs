using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthLife : MonoBehaviour
{
    public static float health = 100;//PROBAR
    public Image barravida;

    public static float mana = 100;
    public float SaludActual, manaActual;//PROBAR
    public Image barraMana;

    public int nivel = 1;
    public Text Nivel_UI;

    void Update()
    {
        Nivel_UI.text = "Nivel: " + nivel;

        SaludActual = health / 100;
        barravida.fillAmount = SaludActual;
        manaActual = mana / 100;
        barraMana.fillAmount = manaActual;

        //Breaks de Salud
        if (health <= 0)
            health = 0;
        if (SaludActual <= 0)
            SaludActual = 0;
        if (health >= 100)
            health = 100;
        if (SaludActual >= 100)
            SaludActual = 100;

        //Breaks Mana
        if (mana <= 0)
            mana = 0;
        if (manaActual <= 0)
            manaActual = 0;
        if (mana >= 100)
            mana = 100;
        if (manaActual >= 100)
            manaActual = 100;
    }

    public void SubirNivel() { nivel++; }
}
