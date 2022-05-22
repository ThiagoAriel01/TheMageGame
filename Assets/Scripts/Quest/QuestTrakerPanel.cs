using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTrakerPanel : MonoBehaviour
{
    public QuestActor dataB;
    public PlayerQuest player;

    public Text questDescriptionText;
    public Text infoRecompensaText;
    public Button questNameButton;
    public Transform buttonContainer1;
    public Transform buttonContainer2;

    private bool showQuestFinished = false;
    private List<Button> poolButton = new List<Button>();

    private void Awake(){       
         ManagerMagos.Intance.onMagoChanged += OnMagoChanged;
    }
    void OnMagoChanged(PlayerActor player){
        this.player = player.GetComponent<PlayerQuest>();
    }

    public void ActualizarBotones()
    {
        List<Quest> questsT;

        if (!showQuestFinished)
            questsT = player.questManager.activeQuest;
        else
            questsT = player.questManager.completeQuest;

        if (poolButton.Count >= questsT.Count)
        {
            for (int i = 0; i < poolButton.Count; i++)
            {
                if (i < questsT.Count)
                {
                    int a = dataB.misions[questsT[i].ID].ID;
                    poolButton[i].GetComponentInChildren<Text>().text = dataB.misions[questsT[i].ID].name;
                    poolButton[i].onClick.RemoveAllListeners();
                    poolButton[i].onClick.AddListener(() => ActualizarDescripcionesConInfo(a)); //Delegada Asigna funciones
                    poolButton[i].GetComponentInChildren<Text>().color = Color.white;
                    Transform x;
                    x = showQuestFinished == false ? x = buttonContainer1 : x = buttonContainer2; //si showQuestFinished es false, x = buttonContainer1 (contenedor de missiones activas), sino x = buttonContainer2 (contenedor de misiones completadas)
                    poolButton[i].transform.SetParent(x);
                    poolButton[i].gameObject.SetActive(true);
                }
                else
                    poolButton[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = poolButton.Count; i < questsT.Count; i++)
            {
                Transform x;
                x = showQuestFinished == false ? x = buttonContainer1 : x = buttonContainer2;
                var nuevoButton = Instantiate(questNameButton, x);
                int a = dataB.misions[questsT[i].ID].ID;
                nuevoButton.GetComponentInChildren<Text>().text = dataB.misions[questsT[i].ID].name;
                nuevoButton.onClick.RemoveAllListeners();
                nuevoButton.onClick.AddListener(()=> { ActualizarDescripcionesConInfo(a); });
                poolButton.Add(nuevoButton);
            }
            ActualizarBotones();
        }
    }
    public void ActualizarDescripcionesConInfo(int id)
    {
        if (id==-1)
        {
            infoRecompensaText.text = string.Empty;
            questDescriptionText.text = string.Empty;
            return;
        }

        questDescriptionText.text = dataB.misions[id].description;

        if (!player.questManager.completeQuest.Exists(x=>x.ID == id))
        {
            switch (dataB.misions[id].questType)
            {
                case QuestActor.Mision.QuestType.Recoleccion:

                    if (player.questManager.DiscriminacionItems(dataB.misions[id].datos[0].itemId) < dataB.misions[id].datos[0].cantidad)
                        infoRecompensaText.text = "Items recogidos: " + "\n" + player.questManager.DiscriminacionItems(dataB.misions[id].datos[0].itemId) + "/" + dataB.misions[id].datos[0].cantidad;
                    else
                        infoRecompensaText.text = "Has recogido todos los items! Ve con: " + player.questManager.rewarders.Find(x => x.id_Mision == id).name + " para finalizar con la mision.";

                    break;

                case QuestActor.Mision.QuestType.Matar:

                    if (player.questManager.activeQuest.Find(x => x.ID == id).currentAmount < dataB.misions[id].cantidad)
                        infoRecompensaText.text = "Enemigos asesinados: " + "\n" + player.questManager.activeQuest.Find(x => x.ID == id).currentAmount + "/" + dataB.misions[id].cantidad;
                    else
                        infoRecompensaText.text = "Has eliminado a todos los enemigos! Ve con: " + player.questManager.rewarders.Find(x => x.id_Mision == id).name + " para finalizar con la mision.";

                    break;

                case QuestActor.Mision.QuestType.Entrega:

                    if (!player.questManager.activeQuest.Find(x => x.ID == id).destino.GetComponent<Destino_Script>().reached)
                        infoRecompensaText.text = "Aun no has llegado. ";
                    else
                        infoRecompensaText.text = "Completado! Ve con: " + player.questManager.rewarders.Find(x => x.id_Mision == id).name + " para finalizar con la mision.";

                    break;

                default:
                    infoRecompensaText.text = "[INFO]";
                    break;
            }
        }
    }

    public void SwapMisiones()
    {
        ActualizarDescripcionesConInfo(-1);
        showQuestFinished = !showQuestFinished;
    }
}
