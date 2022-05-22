using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : MonoBehaviour
{
    public CharacterController enemyController;
    public Animator anim;
    public float damage, minCercania, maxCercania, velocidad, distanciaAtaque, movimiento = 10f, xpDaEnemy;
    private float gravedad = 20f, x, y;
    //public int random;
    public Transform detector;
    private Transform character;
    public bool perseguir = false;
    public float salud;
    public int enemy_ID;

    public GameObject fuego, hielo;
    public float damageMagia, efecto;
    public bool quemadura = false, slow = false;

    public QuestManager manager;
    public bool recibirAtaque;

    public LevelScript lvlScript;

    public string tipoEnemyMuerto;
    public TPManager tPManager;

    Vector3 eje;

    void Awake(){
        enemyController = GetComponent<CharacterController>();
    }

    void Update(){
        character = ManagerMagos.mago.transform;
        Chase();
        Morir();
        Efectos();
        RecibirAtaque();
    }

    void Ataque()
    {
        Debug.Log("te estoy atacando");
        RaycastHit golpe;
        if (Physics.Raycast(detector.position, transform.forward, out golpe, distanciaAtaque))           
            if (golpe.collider.tag == "Player")
            {
                HealthLife.health -= damage;
                Debug.Log("Vida actual: " + HealthLife.health);
            }
    }

    void RecibirAtaque()
    {
        if (recibirAtaque==true)
        {
            anim.Play("Hit");
            recibirAtaque = false;
        }       
    }

    public void FixedUpdate(){
         Move();
    }

    public void Move()
    {
        eje = transform.forward * eje.z + transform.right * eje.x;
        eje.Normalize();
        eje.y = eje.y - (gravedad * Time.fixedDeltaTime);
        enemyController.Move(eje * movimiento * Time.fixedDeltaTime);
    }

    public void Chase()
    {
        float distancia = Vector3.Distance(transform.position, character.position);
        if (distancia < maxCercania && distancia > minCercania)
            perseguir = true;
        else
            perseguir = false;

        Vector3 rotacion = new Vector3(character.position.x, transform.position.y, character.position.z);

        if (perseguir)
        {
            transform.LookAt(rotacion);
            anim.SetBool("Detectado", true);
            anim.SetBool("AtaqueBase", false);
            enemyController.Move(transform.forward * velocidad * Time.deltaTime);
        }
        else
            anim.SetBool("Detectado", false);

        if (distancia <= minCercania)
        {
            perseguir = false;
            anim.SetBool("AtaqueBase", true);
        }
    }

    public void Morir()
    {
        if (salud <= 0)
        {
            lvlScript.SendMessage("RecibirXP", xpDaEnemy);
            salud = 0;
            perseguir = false;
            anim.SetBool("AtaqueBase", false);
            anim.SetBool("Die", true);
            manager.MuerteEnemiga(enemy_ID);
            Destroy(this.gameObject.GetComponent<EnemyActor>());
            Destroy(this.gameObject.GetComponent<CharacterController>());
            tPManager.RecibirString(tipoEnemyMuerto);
            Destroy(this.gameObject, 3);
        }
    }

    public void Efectos()
    {
        if (slow)
        {
            StartCoroutine(MagiaAttack());
            hielo.SetActive(true);
        }
        if (quemadura)
        {
            StartCoroutine(MagiaAttack());
            fuego.SetActive(true);
        }
    }

    IEnumerator MagiaAttack()
    {
        if (quemadura)
        {
            salud -= damageMagia * Time.deltaTime;
            yield return new WaitForSeconds(efecto);
            quemadura = false;
            fuego.SetActive(false);
        }
        if (slow)
        {
            velocidad = 2.5f;
            anim.speed = 0.5f;
            yield return new WaitForSeconds(efecto);
            velocidad = 5;
            anim.speed = 1;
            slow =false;
            hielo.SetActive(false);
        }
    }

}
