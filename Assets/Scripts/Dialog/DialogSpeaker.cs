using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSpeaker : MonoBehaviour
{
    public List<Conversacion> conversacionesDisponibles = new List<Conversacion>();
    GameObject player;
    [SerializeField]
    private int indexDeconversaciones = 0; //recorre cada conversacion dentro de la lista de conversaciones disponibles
    [HideInInspector]
    public int dialLocalIn = 0;

    void Start()
    {
        player = null;
        indexDeconversaciones = 0;
        dialLocalIn = 0;
    }
    private void Update()
    {
        if (player!=null)
            Conversar();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            player = other.gameObject;

        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Z))
            DialogSystem.instance.CambiarEstadoReUsable(conversacionesDisponibles[indexDeconversaciones], !conversacionesDisponibles[indexDeconversaciones].reUsar);
    }

    public void Conversar()
    {
        if (indexDeconversaciones <= conversacionesDisponibles.Count - 1)
        {
            if (conversacionesDisponibles[indexDeconversaciones].desbloqueada)
            {
                if (conversacionesDisponibles[indexDeconversaciones].finalizado)
                {
                    if (ActualizarConversacion())
                    {
                        DialogSystem.instance.MostrarUI(true);
                        DialogSystem.instance.SetConversacion(conversacionesDisponibles[indexDeconversaciones], this);
                    }
                    DialogSystem.instance.SetConversacion(conversacionesDisponibles[indexDeconversaciones], this);
                    return;
                }
                DialogSystem.instance.MostrarUI(true);
                DialogSystem.instance.SetConversacion(conversacionesDisponibles[indexDeconversaciones], this);
            }
            else
            {
                Debug.LogWarning("La conversacion esta bloqueada");
                DialogSystem.instance.MostrarUI(false);
            }
        }
        else
        {
            print("Fin del Dialogo");
            DialogSystem.instance.MostrarUI(false);
        }
    }

    bool ActualizarConversacion()
    {
        if (!conversacionesDisponibles[indexDeconversaciones].reUsar)
        {
            if (indexDeconversaciones < conversacionesDisponibles.Count - 1)
            {
                indexDeconversaciones++;
                return true;
            }
            else
                return false;
        }
        else
            return true;
    }

    private void OnTriggerExit(Collider other)
    {     
        if (other.CompareTag("Player"))
        {
            player = null;
            DialogSystem.instance.MostrarUI(false);
        }
    }
}
