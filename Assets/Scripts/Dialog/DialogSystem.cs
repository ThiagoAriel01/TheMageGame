using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem instance { get; private set; }
    public static DialogSpeaker speakerActual;
    [SerializeField] private Dialog dialog;

    public ControladorPreguntas controladorPreguntas;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        dialog = FindObjectOfType<Dialog>();
        controladorPreguntas = FindObjectOfType<ControladorPreguntas>();
    }

    void Start() {
        MostrarUI(false);
    }

    public void MostrarUI(bool mostrar)
    {
        dialog.gameObject.SetActive(mostrar);
        if (!mostrar)
        {
            dialog.localIn = 0;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void SetConversacion(Conversacion conv, DialogSpeaker speaker)
    {
        if (speaker != null)
            speakerActual = speaker;
        else
        {
            //En caso de ser speaker null sig q vengo de una pregunta  
            //por lo q reseteo el localIn para recorrer toda la conversacion producto de la respuesta elegida
            dialog.conversacion = conv;
            dialog.localIn = 0;
            dialog.ActualizarTexto(0);
        }
        if (conv.finalizado && !conv.reUsar)
        {
            dialog.conversacion = conv;
            dialog.localIn = conv.dialogos.Length;
            dialog.ActualizarTexto(1);
        }
        else
        {
            dialog.conversacion = conv;
            dialog.localIn = speakerActual.dialLocalIn;
            dialog.ActualizarTexto(0);
        }
    }

    public void CambiarEstadoReUsable(Conversacion conv, bool estadoDeseado){
        conv.reUsar = estadoDeseado;
    }

    public void BloqueDesbloqueoConv(Conversacion conv, bool desbloquear){
        conv.desbloqueada = desbloquear;
    }
}
