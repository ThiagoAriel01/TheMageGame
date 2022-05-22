using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestActor db;
    //QUEST comenzadas(aun incompletas/en proceso)
    public List<Quest> activeQuest = new List<Quest>();

    //QUEST terminadas(se completaron los requisitos)
    public List<Quest> finishedQuest = new List<Quest>();

    //QUEST terminadas(se cobro la recompensa)
    public List<Quest> completeQuest = new List<Quest>();

    //Lista q almacena los NPCs q nos dieron las quest
    [HideInInspector] public List<QuestGiver> rewarders = new List<QuestGiver>();

    public void MuerteEnemiga(int enemy_ID)
    {
        if (activeQuest.Count > 0)
        {
            for (int i = 0; i < activeQuest.Count; i++)
            {
                if (activeQuest[i].idEnemigo == enemy_ID)
                {
                    activeQuest[i].currentAmount++;
                    if (activeQuest[i].currentAmount < activeQuest[i].totalAmount)
                        print("Cantidad restante de enemigos: " + (activeQuest[i].totalAmount - activeQuest[i].currentAmount));
                    ActualizarQuest(activeQuest[i].ID, activeQuest[i].questType);
                    break;
                }
            }
        }
    }

    public void ActualizarQuest(int quest_ID, Quest.QuestType type, int? cantItems = null)//parametro opcional
    {
        var val = activeQuest.Find(x => x.ID == quest_ID);// expresion LAMBDA(SI ENCUENTRA DENTRO DE LA QUEST UN ID IGUAL AL  Q LE PASAMOS POR PARAMETRO LO METE NE LA VARIABLE val)
        
        if (type == Quest.QuestType.Matar)
        {
            if (val.currentAmount >= val.totalAmount)
            {
                Debug.LogWarning("Quest: " + db.misions[val.ID].name + " completada!" );
                val.complete = true;
            }
            else
                print("Aun no has completado la quest: " + db.misions[val.ID].name);
        }

        if (type == Quest.QuestType.Entrega)
        {
            if (val.destino.GetComponent<Destino_Script>().reached)
            {
                Debug.LogWarning("Quest: " + db.misions[val.ID].name + " completada!");
                val.complete = true;
            }
            else
                print("Aun no has llegado al objetivo.");
        }

        if (type == Quest.QuestType.Recoleccion)
        {
            foreach(var item in val.itemsRecogers)
            {
                if (cantItems != null)
                {
                    if (cantItems == item.cantidad)
                    {
                        Debug.LogWarning("Quest: " + db.misions[val.ID].name + " completada!");
                        val.complete = true;
                    }
                    else
                        print("Aun no has recolectado todos los items. Te falatan: " + (item.cantidad - cantItems));
                }
            }
        }
    }
    
    public void VerificarItem(int item_ID)
    {
        Quest q = null;
        if (activeQuest.Count > 0)
        {
            if (activeQuest.Exists(x => x.itemsRecogers.Exists(a => a.itemId == item_ID))) // si existe un objeto dentro de las activeQuest q contenga en su lista de itemsRecoger un item con el mismo id del parametro, asignale esa Quest a q
                q = activeQuest.Find(x => x.itemsRecogers.Exists(a => a.itemId == item_ID));
            else{
                q = null;
                return;
            }

            for (int i = 0; i < activeQuest.Count; i++)
            {
                if (q.itemsRecogers[0].itemId==item_ID && activeQuest[i].ID == q.ID)
                {
                    int cantidad = DiscriminacionItems(db.misions[activeQuest[i].ID].datos[0].itemId);
                    ActualizarQuest(activeQuest[i].ID, activeQuest[i].questType, cantidad);
                    q = null;
                    break;
                }
            }
        }
    }

    public int DiscriminacionItems(int id)
    {
        int itemsMached = 0;
        foreach (var item in GetComponent<PlayerQuest>().invLocal)
        {
            if (item.GetComponent<ItemInGame>().id == id)
                itemsMached++;
        }
        return itemsMached;
    }
}
