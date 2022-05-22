using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Habilitys : MonoBehaviour
{
    [Header("Magia")]
    public GameObject xSpellPreview;
    public GameObject xSpell;
    public GameObject cSpellPreview;
    public GameObject cSpell;

    [Header("Hability X")]
    public Image habilityImageX;
    public float cooldownX = 5;
    bool isColdownX = false;

    [Header("Hability C")]
    public Image habilityImageC;
    public float cooldownC = 6;
    bool isColdownC = false;

    public PlayerActor player;

    [SerializeField] GameObject particleActivar, particleDesactivarIzq, particleDesactivarDerecha;
    [SerializeField] GameObject habilityActivarX, habilityActivarC, habilityDesactivarX, habilityDesactivarC, ultActivar, ultDesactivar;

    void Start()
    {
        habilityImageX.fillAmount = 0;
        habilityImageC.fillAmount = 0;
    }

    void Update()
    {
        HandleXAttack();
        HandleCAttack();
    }

    public void HandleXAttack()
    {

        if (Input.GetKey(KeyCode.Alpha1) && isColdownX == false && HealthLife.mana > 0)
        {
            xSpellPreview.SetActive(true);
            if (Input.GetMouseButtonDown(1))
            {
                player.puedeMoverse = false;
                player.anim.Play("HabilityX");
                isColdownX = true;
                habilityImageX.fillAmount = 1;
            }
        }           
        else
        {

            xSpellPreview.SetActive(false);
        }
        
        if (isColdownX)
        {
            habilityImageX.fillAmount -= 1 / cooldownX * Time.deltaTime;
            if (habilityImageX.fillAmount <= 0)
            {
                habilityImageX.fillAmount = 0;
                isColdownX = false;
            }
        }
    }
    public void HandleCAttack()
    {
        if (Input.GetKey(KeyCode.Alpha2) && isColdownC == false && HealthLife.mana > 0)
        {
            cSpellPreview.SetActive(true);
            if (Input.GetMouseButtonDown(1))
            {
                player.puedeMoverse = false;
                player.anim.Play("HabilityC");
                isColdownC = true;
                habilityImageC.fillAmount = 1;
            }
        }            
        else
        {
            cSpellPreview.SetActive(false);
        }
        
        if (isColdownC)
        {
            habilityImageC.fillAmount -= 1 / cooldownC * Time.deltaTime;
            if (habilityImageC.fillAmount <= 0)
            {
                habilityImageC.fillAmount = 0;
                isColdownC = false;
            }
        }

    }

    void InstanciarX(){
        GameObject.Instantiate(xSpell, this.transform.position, this.transform.rotation);
    }

    void InstanciarC(){
        GameObject.Instantiate(cSpell, cSpellPreview.gameObject.transform.position, this.transform.rotation);
    }

    public void CambioEstancia()
    {
        player.puedeMoverse = false;
        particleActivar.SetActive(true);
        particleDesactivarDerecha.SetActive(false);
        particleDesactivarIzq.SetActive(false);
        player.anim.Play("Ult");
    }
    public void CambiarParticulas()
    {
        particleActivar.SetActive(false);
        particleDesactivarDerecha.SetActive(true);
        particleDesactivarIzq.SetActive(true);
        habilityActivarC.SetActive(true);
        habilityActivarX.SetActive(true);
        ultActivar.SetActive(true);
        habilityDesactivarC.SetActive(false);
        habilityDesactivarX.SetActive(false);
        ultDesactivar.SetActive(false);        
    }
}
