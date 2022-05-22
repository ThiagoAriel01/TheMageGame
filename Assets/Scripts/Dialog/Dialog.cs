using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    public Conversacion conversacion; //Conversacion actual mostrada

    [SerializeField]
    private GameObject convContainer;
    [SerializeField]
    private GameObject pregContainer;

    [SerializeField]
    private Image speakIm;
    [SerializeField]
    private TextMeshProUGUI nombre, convText;

    [SerializeField]
    private Button continuarButton, anterionButton;

    private AudioSource audioSource;

    public int localIn = 0;
    //Recorre cada dialogo dentro de la conversacion actual, lo mismo que "dialLocalIn" en DialogSpeaker
    // solo que este adopta el valor en base al que tenga puesto el DialogSpeaker al momento de hablar.

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

        convContainer.SetActive(true);
        pregContainer.SetActive(false);

        continuarButton.gameObject.SetActive(true);
        anterionButton.gameObject.SetActive(false);
    }

    public void ActualizarTexto(int comportamiento)
    {
        convContainer.SetActive(true);
        pregContainer.SetActive(false);

        switch (comportamiento)
        {
            case -1:
                if (localIn > 0)
                {
                    print("Dialogo Anterior");
                    localIn--;

                    nombre.text = conversacion.dialogos[localIn].personaje.nombre;
                    convText.text = conversacion.dialogos[localIn].dialogo;
                    speakIm.sprite = conversacion.dialogos[localIn].personaje.imagen;

                    if (conversacion.dialogos[localIn].sonido != null)
                    {
                        audioSource.Stop();
                        audioSource.PlayOneShot(conversacion.dialogos[localIn].sonido);
                    }
                    anterionButton.gameObject.SetActive(localIn > 0);
                }

                DialogSystem.speakerActual.dialLocalIn = localIn;
                break;

            case 0:
                print("Dialogo Actualizado");

                nombre.text = conversacion.dialogos[localIn].personaje.nombre;
                convText.text = conversacion.dialogos[localIn].dialogo;
                speakIm.sprite = conversacion.dialogos[localIn].personaje.imagen;

                if (conversacion.dialogos[localIn].sonido != null)
                {
                    audioSource.Stop();
                    audioSource.PlayOneShot(conversacion.dialogos[localIn].sonido);
                }
                anterionButton.gameObject.SetActive(localIn > 0);

                if (localIn >= conversacion.dialogos.Length -1)
                    continuarButton.GetComponentInChildren<TextMeshProUGUI>().text = "Finalizar";
                else
                    continuarButton.GetComponentInChildren<TextMeshProUGUI>().text = "Continuar";

                break;//Avanza el texto

            case 1:
                if (localIn < conversacion.dialogos.Length -1)
                {
                    print("Dialogo Siguiente");
                    localIn++;
                    nombre.text = conversacion.dialogos[localIn].personaje.nombre;
                    convText.text = conversacion.dialogos[localIn].dialogo;
                    speakIm.sprite = conversacion.dialogos[localIn].personaje.imagen;

                    if (conversacion.dialogos[localIn].sonido != null)
                    {
                        audioSource.Stop();
                        audioSource.PlayOneShot(conversacion.dialogos[localIn].sonido);
                    }
                    anterionButton.gameObject.SetActive(localIn > 0);

                    if (localIn >= conversacion.dialogos.Length - 1)
                        continuarButton.GetComponentInChildren<TextMeshProUGUI>().text = "Finalizar";
                    else
                        continuarButton.GetComponentInChildren<TextMeshProUGUI>().text = "Continuar";
                }
                else
                {
                    print("Dialogo Terminado");
                    localIn = 0;
                    DialogSystem.speakerActual.dialLocalIn = 0;
                    conversacion.finalizado = true;

                    if (conversacion.pregunta != null)
                    {
                        convContainer.SetActive(false);
                        pregContainer.SetActive(true);
                        DialogSystem.instance.controladorPreguntas.ActivarBotones(conversacion.pregunta.opciones.Length, conversacion.pregunta.pregunta, conversacion.pregunta.opciones);
                        return;
                    }
                    DialogSystem.instance.MostrarUI(false);
                    return;
                }
                DialogSystem.speakerActual.dialLocalIn = localIn;
                break;

            default:
                Debug.LogWarning("Estas pasando un valor no admitido( solo se aceptan estos valores -1, 0, 1)");
                break;
        }
    }
}
