using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour
{
    protected float rotacionX, rotacionY, velocidadRotacion = 100f, movimiento = 10f, gravedad = 9.8f;

    [SerializeField] protected Transform camara;
    [SerializeField] protected AudioSource shoot;

    public InventoryData inventoryData;

    [SerializeField] protected Animator anim;
    [SerializeField] protected float firaRate, timerDisparo;
    protected bool puedeDisparar;
    [SerializeField] protected Disparador disparador;

    public PC playableCharacter = null;

    Vector3 eje;
    CharacterController cc;

    protected void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Bloquea el cursor en el centro de la pantalla
        Cursor.visible = false; //Hace invisible el cursor
    }

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        playableCharacter = PCFabric.GetInstance().GetClass(); // DESCOMENTAR. Para arrancar desde la selección de personaje
        //playableCharacter = new MageFire(); // Para probar. Comentar al descomentar el anterior.
    }

    protected void Update()
    {
        GetInput();
        Rotate();
        if (puedeDisparar == false)               //La ballesta puede disparar de a una flecha por vez cada 2 segundos 
        {                                       //q es lo q tarda la animacionen completarse
            timerDisparo -= Time.deltaTime;
            if (timerDisparo <= 0)
                puedeDisparar = true;
        }
    }

    protected void FixedUpdate()
    {
        Move();
    }

    protected void Move()
    {
        eje = transform.forward * eje.z + transform.right * eje.x;
        eje.Normalize();
        eje.y = eje.y - (gravedad * Time.fixedDeltaTime);
        cc.Move(eje * movimiento * Time.fixedDeltaTime);
    }

    protected void GetInput()
    {
        eje.z = 0;
        eje.x = 0;

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

        //IZQUIERDA

       if (Input.GetMouseButtonDown(1))
        {
           if (puedeDisparar)
            {
                disparador.Disparar();
                //shoot.Play();
                timerDisparo = firaRate;
                //anim.SetTrigger("Disparar");
                puedeDisparar = false;
            }
        }
    }

    protected void Rotate()
    {
        rotacionX = Input.GetAxis("Mouse X");
        transform.Rotate(0, rotacionX * velocidadRotacion * Time.fixedDeltaTime, 0);
        rotacionY = -Input.GetAxis("Mouse Y");
        camara.Rotate(rotacionY * velocidadRotacion * Time.fixedDeltaTime, 0, 0);
        camara.localRotation = ClampRotationAroundXAxis(camara.localRotation);
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q) //Consigue bloquear el movimiento del mouse entre 90 y -90°
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;
        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, -45, 45);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);
        return q;
    }

    public void Bloquear() { }

    public void OnTriggerEnter(Collider collision) { }
}
