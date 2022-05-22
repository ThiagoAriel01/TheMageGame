using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public struct ObjetoInvId
    {
        public int id, cantidad;
        public ObjetoInvId(int id, int cantidad)
        {
            this.id = id;
            this.cantidad = cantidad;
        }
    }

    [SerializeField] InventoryData data;

    [Header("Variables del Drag and Drop")]
    public GraphicRaycaster graphRay;
    private PointerEventData pointerData;
    private List<RaycastResult> raycastResults;
    public static Transform canvas;
    public GameObject objSelec;
    public Transform exParent;
    public bool inventoryEnabled=false;

    [Header("Pref y Items")]
    public static GameObject description;
    public CartelEliminacion cartelEliminacion;
    public int OSC, OSID;

    public Transform slotHandler;
    public ItemActor item;
    public List<ObjetoInvId> inventario = new List<ObjetoInvId>();

    public List<ItemInGame> itemInGames = new List<ItemInGame>();
    public Transform itemsRespawn;

    public PlayerActor player;

    private void Awake()
    {
        if(ManagerMagos.Intance != null)
            ManagerMagos.Intance.onMagoChanged += OnMagoChanged;
    }
    void OnMagoChanged(PlayerActor player)
    {
        this.player = player;
    }
    void Start()
    {
        InventoryUpdate();
        pointerData = new PointerEventData(null);
        raycastResults = new List<RaycastResult>();
        canvas = transform.parent;
        description = GameObject.Find("Descripcion");
        cartelEliminacion.gameObject.SetActive(false);
        
    }

    void Update(){
        Arrastrar();          
    }

    public void AddItem( int itemID, int cantidad)
    {
        for (int i = 0; i < inventario.Count; i++)
        {
            if (inventario[i].id == itemID && data.baseDatos[itemID].acumulable)
            {
                inventario[i] = new ObjetoInvId(inventario[i].id, inventario[i].cantidad + cantidad);
                InventoryUpdate();
                return;
            }
        }
        if (!data.baseDatos[itemID].acumulable)
            inventario.Add(new ObjetoInvId(itemID, 1));
        else
            inventario.Add(new ObjetoInvId(itemID, cantidad));

        InventoryUpdate();
    }
    public void EliminarItem(int itemID, int cantidad)
    {
        for (int i = 0; i < inventario.Count; i++)
        {
            if (inventario[i].id == itemID)
            {
                inventario[i] = new ObjetoInvId(inventario[i].id, inventario[i].cantidad - cantidad);
                for (int n = 0; n < itemInGames.Count; n++)
                {
                    if (itemInGames[n].id == itemID)
                    {
                        itemInGames[n].gameObject.SetActive(true);
                        itemInGames[n].transform.position = itemsRespawn.position;
                        itemInGames[n].transform.SetParent(null);
                        itemInGames.Remove(itemInGames[n]);
                    }
                }
                if (inventario[i].cantidad<=0)
                {
                    inventario.Remove(inventario[i]);
                    InventoryUpdate();
                    break;
                }            
            }
            InventoryUpdate();
        }
    }

    void Arrastrar() {

        if (Input.GetMouseButtonDown(1))
        {
            pointerData.position = Input.mousePosition;
            graphRay.Raycast(pointerData, raycastResults);
            if (raycastResults.Count > 0){
                if (raycastResults[0].gameObject.GetComponent<ItemActor>()){
                    objSelec = raycastResults[0].gameObject;
                    OSC = objSelec.GetComponent<ItemActor>().cantidad;
                    OSID = objSelec.GetComponent<ItemActor>().ID;
                    exParent = objSelec.transform.parent;
                    exParent.GetComponent<Image>().fillCenter = false;
                    objSelec.transform.SetParent(canvas);

                }
            }
        }

        if (objSelec!=null)
            objSelec.GetComponent<RectTransform>().localPosition = CanvasScreem(Input.mousePosition);       

        if (objSelec != null) { 
            if (Input.GetMouseButtonUp(1)){
                pointerData.position = Input.mousePosition;
                raycastResults.Clear();
                graphRay.Raycast(pointerData, raycastResults);
                objSelec.transform.SetParent(exParent);
                if (raycastResults.Count > 0){
                    foreach(var resultado in raycastResults){
                        if (resultado.gameObject == objSelec) continue;
                        if (resultado.gameObject.CompareTag("Slot")){
                            if (resultado.gameObject.GetComponentInChildren<ItemActor>() == null){
                                //Slot libre
                                objSelec.transform.SetParent(resultado.gameObject.transform);
                                Debug.Log("Slot Libre");
                            }
                        }
                        if (resultado.gameObject.CompareTag("Item")){
                            if (resultado.gameObject.GetComponentInChildren<ItemActor>().ID == objSelec.GetComponent<ItemActor>().ID){
                                //Mismo ID
                                resultado.gameObject.GetComponentInChildren<ItemActor>().cantidad += objSelec.GetComponent<ItemActor>().cantidad;
                                Destroy(objSelec.gameObject);
                            }
                            else{
                                //Distinto ID
                                objSelec.transform.SetParent(resultado.gameObject.transform.parent);
                                resultado.gameObject.transform.SetParent(exParent);
                                resultado.gameObject.transform.localPosition = Vector3.zero;
                            }
                        }
                        if (resultado.gameObject.CompareTag("Eliminar"))
                        {
                            if (objSelec.gameObject.GetComponent<ItemActor>().cantidad >= 2)
                                cartelEliminacion.gameObject.SetActive(true);
                            else{
                                cartelEliminacion.gameObject.SetActive(false);
                                EliminarItem(objSelec.gameObject.GetComponent<ItemActor>().ID, objSelec.gameObject.GetComponent<ItemActor>().cantidad);
                            }
                        }
                    }
                }
                objSelec.transform.localPosition = Vector3.zero;
                objSelec = null;
            }
        }
        raycastResults.Clear();
    }

    public Vector2 CanvasScreem(Vector2 screemPos)
    {
        Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(screemPos);//error
        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        return (new Vector2(viewportPoint.x * canvasSize.x, viewportPoint.y * canvasSize.y)-(canvasSize/2));
    }

    List<ItemActor> pool = new List<ItemActor>();
    public void InventoryUpdate()
    {
        for (int i = 0; i < pool.Count; i++){
            if (i<inventario.Count){
                ObjetoInvId o = inventario[i];
                pool[i].ID = o.id;
                pool[i].GetComponent<Image>().sprite = data.baseDatos[o.id].icon;
                pool[i].GetComponent<RectTransform>().localPosition = Vector3.zero; //pasaba un error
                pool[i].cantidad = o.cantidad;
                pool[i].button.onClick.RemoveAllListeners();
                pool[i].button.onClick.AddListener(() => gameObject.SendMessage(data.baseDatos[o.id].Void, SendMessageOptions.DontRequireReceiver));
                pool[i].gameObject.SetActive(true);
            }
            else{
                pool[i].gameObject.SetActive(false);
                pool[i].description_.SetActive(false);
                pool[i].gameObject.transform.parent.GetComponent<Image>().fillCenter = false;
            }
        }
        if (inventario.Count > pool.Count){
            for (int i = pool.Count; i < inventario.Count; i++){
                ItemActor it = Instantiate(item, slotHandler.GetChild(i));
                pool.Add(it);

                if (slotHandler.GetChild(0).childCount >= 2){ //si ya hay un objeto en el slot lo agrega al primer espacio libre 
                    for (int s = 0; s < slotHandler.childCount; s++){
                        if (slotHandler.GetChild(s).childCount==0){
                            it.transform.SetParent(slotHandler.GetChild(s));
                            break;
                        }
                    }
                }
                it.transform.position = Vector3.zero;
                it.transform.localScale = Vector3.one;

                ObjetoInvId o = inventario[i];
                pool[i].ID = o.id;
                pool[i].GetComponent<RectTransform>().localPosition = Vector3.zero;
                pool[i].GetComponent<Image>().sprite = data.baseDatos[o.id].icon;
                pool[i].cantidad = o.cantidad;
                pool[i].button.onClick.RemoveAllListeners();
                pool[i].button.onClick.AddListener(() => gameObject.SendMessage(data.baseDatos[o.id].Void, SendMessageOptions.DontRequireReceiver));
                pool[i].gameObject.SetActive(true);
            }
        }
    }

    void PocionVida()
    {
        HealthLife.health += 20f;
        EliminarItem(1,1);
    }

    void PocionMana()
    {
        HealthLife.mana += 20f;
        EliminarItem(2, 1);
    }

    void Espada()
    {
        player.estados = 1;
        player.espada = true;
    }

    void Daga()
    {
        player.estados = 2;
        player.daga = true;
    }
}
