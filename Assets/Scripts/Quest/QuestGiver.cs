using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public QuestActor dataB;
    public int id_Mision;
    public QuestPanel questPanel;

    public bool isStarted = false;
    public Quest quest;
    public bool rewarded = false;
    public bool isRewardGiver;
    //Quien entrega la recompensa
    public QuestGiver questRewarder;
    //El NPC q dirigio al jugador hasta este NPC
    public QuestGiver prevQuestGiver;

    private PlayerQuest player;

    void Start()
    {
        for (int i = 0; i < dataB.misions.Length; i++)
        {
            if (dataB.misions[i].ID == this.id_Mision)
            {
                quest.name = dataB.misions[i].name;
                quest.ID = dataB.misions[i].ID;
                quest.idEnemigo = dataB.misions[i].enemyID;
                quest.totalAmount = dataB.misions[i].cantidad;
                quest.retieneItems = dataB.misions[i].seQuedaConItems;
                quest.questType = (Quest.QuestType)dataB.misions[i].questType;
                if (dataB.misions[i].datos != null)
                {
                    if (dataB.misions[i].datos.Count > 0)
                    {
                        quest.itemsRecogers.Add(dataB.misions[i].datos[0]);
                        if (quest.itemsRecogers.Count > 1)
                            quest.itemsRecogers.RemoveAt(1);
                    }
                }
                if (quest.destino != null)
                {
                    quest.destino.GetComponent<Destino_Script>().id_Quest = quest.ID;
                }
            }
        }
        if (isRewardGiver)
            questRewarder = this;
        else
        {
            questRewarder.dataB = this.dataB;
            questRewarder.id_Mision = this.id_Mision;
            questRewarder.isStarted = this.isStarted;
            questRewarder.rewarded = this.rewarded;
            questRewarder.quest = this.quest;
            questRewarder.prevQuestGiver = this.prevQuestGiver;
            questRewarder.quest.destino = this.quest.destino;
            questRewarder.questPanel = this.questPanel;

            if (quest.destino != null)
                quest.destino.GetComponent<Destino_Script>().id_Quest = quest.ID;
            if (questRewarder.questRewarder == questRewarder)
                questRewarder.isRewardGiver = true;
            else
                questRewarder.isRewardGiver = false;
        }
    }

    public void ContactoJugador(PlayerQuest jugador)
    {
        player = jugador;

        if (!rewarded)
        {
            if (!isStarted)
            {
                if (prevQuestGiver == null)
                {
                    questPanel.accept_Button.gameObject.SetActive(true);
                    questPanel.deny_Button.gameObject.SetActive(false);

                    questPanel.ActualizarPanel(dataB.misions[id_Mision].name, dataB.misions[id_Mision].description);

                    questPanel.accept_Button.onClick.RemoveAllListeners();
                    questPanel.accept_Button.onClick.AddListener(AceptarQuest);
                    questPanel.accept_Button.onClick.AddListener(jugador.questTrakerPanel.ActualizarBotones);
                    questPanel.deny_Button.onClick.RemoveAllListeners();
                    questPanel.deny_Button.onClick.AddListener(delegate () { questPanel.gameObject.SetActive(false); });
                }
                else
                {
                    questPanel.accept_Button.gameObject.SetActive(false);
                    questPanel.deny_Button.gameObject.SetActive(false);
                    questPanel.ActualizarPanel("", "No tengo ninguna mision para ti amigo");
                }
            }
            else
            {
                //Esto es un seguro por si hay mas de 2 QuestGivers previos al q da la recompensa
                questRewarder.isStarted = true;

                if (jugador.questManager.activeQuest.Exists(x => x.ID == id_Mision))
                {
                    if (jugador.questManager.activeQuest.Find(x => x.ID == id_Mision).complete)
                    {
                        if (isRewardGiver)
                        {
                            jugador.questManager.rewarders.Remove(this);
                            jugador.Recompenzar(quest);
                            rewarded = true;

                            var questTerminada = jugador.questManager.activeQuest.Find(x => x.ID == id_Mision);
                            jugador.questManager.completeQuest.Add(questTerminada);
                            jugador.questManager.activeQuest.Remove(questTerminada);

                            if (quest.destino != null)
                            {
                                Destroy(quest.destino);
                                quest.destino = null;
                            }

                            if (prevQuestGiver != null)
                                prevQuestGiver.rewarded = true;
                        }
                        else
                        {
                            Debug.LogWarning("Para finalizar debes ver a " + questRewarder.name);
                            questPanel.accept_Button.gameObject.SetActive(false);
                            questPanel.deny_Button.gameObject.SetActive(false);
                            questPanel.ActualizarPanel(dataB.misions[id_Mision].name, "Para finalizar debes ver a " + questRewarder.name);
                        }
                    }
                    else
                    {
                        questPanel.accept_Button.gameObject.SetActive(false);
                        questPanel.deny_Button.gameObject.SetActive(false);

                        if (dataB.misions[id_Mision].questType == QuestActor.Mision.QuestType.Recoleccion)
                        {
                            jugador.questManager.ActualizarQuest(id_Mision, (Quest.QuestType)dataB.misions[id_Mision].questType, jugador.questManager.DiscriminacionItems(dataB.misions[id_Mision].datos[0].cantidad));
                            questPanel.ActualizarPanel(dataB.misions[id_Mision].name, "Aun no has completado el objetivo: " + (jugador.questManager.DiscriminacionItems(dataB.misions[id_Mision].datos[0].itemId) - dataB.misions[id_Mision].datos[0].cantidad));
                        }
                        else
                        {
                            jugador.questManager.ActualizarQuest(id_Mision, (Quest.QuestType)dataB.misions[id_Mision].questType);
                            questPanel.ActualizarPanel(dataB.misions[id_Mision].name, "Aun no has completado el objetivo");
                        }
                    }
                }
            }
        }
    }

    public void AceptarQuest()
    {
        questPanel.gameObject.SetActive(false);
        Debug.LogWarning("Mision " + dataB.misions[id_Mision].name + " iniciada.");
        player.questManager.activeQuest.Add(new Quest { ID = id_Mision, totalAmount = quest.totalAmount, idEnemigo = quest.idEnemigo, itemsRecogers = quest.itemsRecogers, questType = quest.questType, destino = quest.destino});

        //Verificamos esto pq puede pasar q el jugador ya tenga en su inventario los items a recolectar
        if (dataB.misions[id_Mision].questType == QuestActor.Mision.QuestType.Recoleccion)
            player.questManager.ActualizarQuest(id_Mision, (Quest.QuestType)dataB.misions[id_Mision].questType, player.questManager.DiscriminacionItems(dataB.misions[id_Mision].datos[0].itemId));
        else
            player.questManager.ActualizarQuest(id_Mision, (Quest.QuestType)dataB.misions[id_Mision].questType);

        questRewarder.isStarted = true;
        this.isStarted = true;

        if (player.questManager.activeQuest.Exists(x => x.ID == id_Mision))
            if (player.questManager.activeQuest.Find(x => x.ID == id_Mision).complete)
                ContactoJugador(player);

        player.questManager.rewarders.Add(questRewarder);   
    }
}
