using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    public int xp;
    public Slider XP_UI;
    HealthLife life;

    void Start()
    {
        life = GetComponent<HealthLife>();
        XP_UI.maxValue = xpNecesaria(life.nivel);
    }

    public void RecibirXP(int valor)
    {
        XP_UI.maxValue = xpNecesaria(life.nivel);
        xp += valor;
        Debug.Log(valor);
    }

    void Update()
    {
        if (xp >= xpNecesaria(life.nivel))
        {
            life.SubirNivel();
            xp = xp - xpNecesaria(life.nivel - 1);
        }
        XP_UI.value = xp;
    }

    int xpNecesaria(int lvl)
    {
        return 50 + (lvl * lvl);
    }
}
