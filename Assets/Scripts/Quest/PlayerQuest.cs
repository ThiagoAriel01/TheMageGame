using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    [Header("Quest")]
    [HideInInspector]
    public List<GameObject> invLocal = new List<GameObject>();
    public int exp;
    public int oro;
    public GameObject inventario;
    public QuestActor dataBase;
    public QuestManager questManager;
    public QuestTrakerPanel questTrakerPanel;
    public QuestPanel questPanel;
    public LevelScript level;
    public void Start()
    {
        
        questManager = GetComponent<QuestManager>();
        questTrakerPanel.gameObject.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("NPC_Mision"))
            if (Input.GetKeyDown(KeyCode.E))
            {
                QuestGiver questG = other.GetComponent<QuestGiver>();
                if(questG!=null)
                    questG.ContactoJugador(this);
            }
                
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destino"))
        {
            other.GetComponent<Destino_Script>().reached = true;
            questManager.ActualizarQuest(other.GetComponent<Destino_Script>().id_Quest, Quest.QuestType.Entrega);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC_Mision"))
            questPanel.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            questTrakerPanel.ActualizarBotones();
            questTrakerPanel.ActualizarDescripcionesConInfo(-1);
            questTrakerPanel.gameObject.SetActive(!questTrakerPanel.gameObject.activeSelf);
        }

        if (questTrakerPanel.gameObject.activeInHierarchy || questPanel.gameObject.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Recompenzar(Quest quest)
    {
        questTrakerPanel.ActualizarBotones();
        exp += dataBase.misions[quest.ID].xp;
        oro += dataBase.misions[quest.ID].gold;

        questPanel.accept_Button.gameObject.SetActive(false);
        questPanel.deny_Button.gameObject.SetActive(false);
        if (dataBase.misions[quest.ID].hasSpecialR)
        {
            if (dataBase.misions[quest.ID].specialR.Length > 1)
            {
                string s = "Bien hecho! Completaste " + dataBase.misions[quest.ID].name + ", como recompensa has obtenido ORO(" + dataBase.misions[quest.ID].gold + " Experiencia(" + dataBase.misions[quest.ID].xp + ") y los siguientes items: ";
                level.RecibirXP(dataBase.misions[quest.ID].xp);
                for (int i = 0; i < dataBase.misions[quest.ID].specialR.Length; i++)
                    s = string.Format("{0}{1}", s, dataBase.misions[quest.ID].specialR[i].nombre);
                questPanel.ActualizarPanel(quest.name, s);
            }
            else
            {
                questPanel.ActualizarPanel(dataBase.misions[quest.ID].name, "Bien hecho! Completaste " + dataBase.misions[quest.ID].name + ", como recompensa has obtenido ORO(" + dataBase.misions[quest.ID].gold + " Experiencia(" + dataBase.misions[quest.ID].xp + ") y " + dataBase.misions[quest.ID].specialR[0].nombre);
                level.RecibirXP(dataBase.misions[quest.ID].xp);
            }
        }
        else
        {
            questPanel.ActualizarPanel(dataBase.misions[quest.ID].name, "Bien hecho! Completaste " + dataBase.misions[quest.ID].name + ", como recompensa has obtenido ORO(" + dataBase.misions[quest.ID].gold + " Experiencia(" + dataBase.misions[quest.ID].xp + ").");
            level.RecibirXP(dataBase.misions[quest.ID].xp);

        }

        if (quest.retieneItems)
        {
            List<GameObject> its = new List<GameObject>();
            int cantidadEliminar = quest.itemsRecogers[0].cantidad;

            for (int i = 0; i < invLocal.Count; i++)
            {
                if (invLocal[i].GetComponent<ItemInGame>().id == quest.itemsRecogers[0].itemId && cantidadEliminar > 0)
                {
                    its.Add(invLocal[i]);
                    inventario.GetComponent<Inventory>().EliminarItem(invLocal[i].GetComponent<ItemInGame>().id, cantidadEliminar);
                    cantidadEliminar--;
                }
            }
            invLocal.RemoveAll(itemB => { return its.Find(itemA => itemA == itemB); });

            foreach (var item in its)
                Destroy(item);
            its.Clear();

            if (dataBase.misions[quest.ID].hasSpecialR)
            {
                foreach (var item in dataBase.misions[quest.ID].specialR)
                {
                    ItemInGame itSuelto = inventario.GetComponent<Inventory>().itemInGames.Find(x => x.id == item.reward.GetComponent<ItemInGame>().id);
                    inventario.GetComponent<Inventory>().AddItem(item.reward.GetComponent<ItemInGame>().id, item.reward.GetComponent<ItemInGame>().cantidad);

                    if (itSuelto != null)
                        itSuelto.cantidad += 1;
                    else{
                        var nuevoIt = Instantiate(item.reward.GetComponent<ItemInGame>());
                        nuevoIt.inv = inventario.GetComponent<Inventory>();
                        nuevoIt.transform.gameObject.transform.SetParent(this.transform);
                        inventario.GetComponent<Inventory>().itemInGames.Add(nuevoIt);
                        nuevoIt.gameObject.SetActive(false);
                    }
                    questManager.VerificarItem(item.reward.GetComponent<ItemInGame>().id);
                    print("Nuevo objeto obtenido: " + item.nombre);
                }
            }
        }
    }
}
