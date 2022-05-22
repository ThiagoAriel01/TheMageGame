using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour
{
    public float rotacionX, rotacionY, velocidadRotacion = 100f, movimiento = 10f, gravedad = 9.8f, x, y;

    [Header("Propiedades")]
    [SerializeField] public Transform camara;
    [SerializeField] protected AudioSource shoot;
    [SerializeField] public Animator anim;
    [SerializeField] protected float firaRate, timerDisparo, timerMove, damage;
    public bool puedeAtacar, puedeMoverse;
    [SerializeField] protected Disparador disparador;
    public PC playableCharacter = null;

    [Header("Armas")]
    public GameObject[] Armas;
    public int estados = 1;
    public bool espada = false, daga = false;

    [Header("Inventario")]
    public Inventory inventory;
    private bool inventoryEnabled;

    Vector3 eje;
    CharacterController cc;

    [Header("Enemy")]
    public Transform detector;

    public Habilitys habilitys;

    public ManagerScenas sceneManager;
    public GameObject panelGris;
    protected void Start()
    {
        inventory.gameObject.SetActive(false);
        panelGris.SetActive(false);

        if (infoParida.hayPartidaGuardada) cargarPartida();
    }

    void Awake(){
        cc = GetComponent<CharacterController>();
    }

    protected void Update()
    {
        GetInput();
        Rotate();
        Condicionales();
        Atacar();
        InventoryCosas();
    }
    public void Atacar()
    {
        if (estados == 0)
        {
            Armas[0].SetActive(false);
            Armas[1].SetActive(false);
        }
        if (estados == 1)
        {
            Armas[0].SetActive(true);
            Armas[1].SetActive(false);
        }
        if (estados == 2)
        {
            Armas[0].SetActive(false);
            Armas[1].SetActive(true);
        }


    }
    public void Condicionales()
    {
        if (inventory.gameObject.activeInHierarchy)
            puedeAtacar = false;
        else
            puedeAtacar = true;

        if (puedeAtacar == false)
        {
            timerDisparo -= Time.deltaTime;
            if (timerDisparo <= 0)
            {
                puedeAtacar = true;
                timerDisparo = 4f;
            }

        }
        if (puedeMoverse == false)
        {
            timerMove -= Time.deltaTime;
            if (timerMove <= 0)
            {
                puedeMoverse = true;
                timerMove = 1.5f;
            }
        }

        if (HealthLife.health <= 0)
        {
            panelGris.SetActive(true);
            cc.enabled = false;

            anim.Play("Die");
            Invoke("cargarPartida", 2.5f);

            panelGris.SetActive(false);
        }
    }

    public void FixedUpdate(){
        if(puedeMoverse)
            Move();
    }

    public void Move()
    {
        eje = transform.forward * eje.z + transform.right * eje.x;
        eje.Normalize();
        eje.y = eje.y - (gravedad * Time.fixedDeltaTime);
        cc.Move(eje * movimiento * Time.fixedDeltaTime);
    }

    public void InventoryCosas()
    {
        if (inventory.gameObject.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;          
        }
        if (!inventory.gameObject.activeInHierarchy && inventory.GetComponent<Inventory>().objSelec != null)
        {
            inventory.GetComponent<Inventory>().objSelec.gameObject.transform.SetParent(inventory.GetComponent<Inventory>().exParent);
            inventory.GetComponent<Inventory>().objSelec.gameObject.transform.localPosition = Vector3.zero;
            inventory.GetComponent<Inventory>().objSelec = null;
        }
    }

    protected void GetInput()
    {
        eje.z = 0;
        eje.x = 0;
        anim.SetFloat("VelY", 0);
        anim.SetFloat("VelX", 0);


        if (Input.GetKey(KeyCode.W))
        {
            eje.z = 1;
            anim.SetFloat("VelY", eje.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            eje.z = -1;
            anim.SetFloat("VelY", eje.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            eje.x = -1;
            anim.SetFloat("VelX", eje.x);
        }
        if (Input.GetKey(KeyCode.D))
        {
            eje.x = 1;
            anim.SetFloat("VelX", eje.x);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (puedeAtacar)
            {
                puedeMoverse = false;
                if (estados == 0)
                {
                    anim.Play("GolpeMagic");
                    disparador.Disparar();
                    shoot.Play();
                    timerDisparo = firaRate;
                    puedeAtacar = false;
                }
                if (estados == 1)
                    anim.Play("GolpeEspada");
                if (estados == 2)
                    anim.Play("GolpeEspada");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)&&espada)
            estados = 1;
        if (Input.GetKeyDown(KeyCode.Alpha5)&&daga)
            estados = 2;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            estados = 0;

        if (Input.GetKeyDown(KeyCode.R))
            inventory.gameObject.SetActive(!inventory.gameObject.activeSelf);

        if (Input.GetKeyDown(KeyCode.Z))
            anim.Play("Dance");

        if (Input.GetKeyDown(KeyCode.Space))
            anim.Play("Roll");

        if (Input.GetKeyDown(KeyCode.LeftShift))
            movimiento = 20;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            movimiento = 10;
    }

    void guardarPartida()
    {
        infoParida.Mage.position = transform.position;
        infoParida.Mage.manaActual = HealthLife.mana;
        infoParida.Mage.vidaActual = HealthLife.health;

        infoParida.hayPartidaGuardada = true;
    }
    void cargarPartida()
    {
        transform.position = infoParida.Mage.position;
        HealthLife.mana = infoParida.Mage.manaActual;
        HealthLife.health = infoParida.Mage.vidaActual;
        cc.enabled = true;
    }

    protected void Rotate()
    {
        rotacionX = Input.GetAxis("Mouse X");
        transform.Rotate(0, rotacionX * velocidadRotacion * Time.fixedDeltaTime, 0);
        rotacionY = -Input.GetAxis("Mouse Y");
        camara.Rotate(rotacionY * velocidadRotacion * Time.fixedDeltaTime, 0, 0);
        camara.localRotation = ClampRotationAroundXAxis(camara.localRotation);
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;
        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, -10, 45);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);
        return q;
    }

    public void Bloquear() { }

    public void OnTriggerEnter(Collider other) {
        GameObject sword, dagger;

        if (other.tag == "Sword")
        {
            sword = GameObject.Find("Sword");
            Destroy(sword);
        }

        if (other.tag == "Dagger")
        {
            dagger = GameObject.Find("Dagger");
            Destroy(dagger);
        }

        if(other.tag == "CheckPoint")
            guardarPartida();
    }

    void AtackWithSword()
    {
        RaycastHit hit;
        if (Physics.Raycast(detector.position, transform.forward, out hit, 3))
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<EnemyActor>().salud -= damage;
                hit.collider.gameObject.GetComponent<EnemyActor>().recibirAtaque = true;
            }
    }
}
