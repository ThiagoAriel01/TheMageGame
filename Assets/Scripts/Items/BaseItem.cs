using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

[System.Serializable]
public class BaseItem
{
    [SerializeField] protected PC pc;
    protected BaseCharacter.Stats stat;

    public int ID, cantidad;
    public Text textoCantidad;
    public Sprite icon;
    public string name, type, description;

    [HideInInspector]
    public bool pickedUp;
    //equipped

    [HideInInspector]
    public GameObject weaponManager, weapon;

    public bool playersWeapon;

    private void Start()
    {
        weaponManager = GameObject.FindWithTag("WeaponManager");

        if (!playersWeapon)
        {
            int allWeapons = weaponManager.transform.childCount;
            for (int i = 0; i < allWeapons; i++)
            {
                if (weaponManager.transform.GetChild(i).gameObject.GetComponent<BaseItem>().ID==ID)
                {
                    weapon = weaponManager.transform.GetChild(i).gameObject;
                }
            }
        }
    }

    public void Awake(){
        pc = GameObject.Find("Player").GetComponent<PlayerActor>().playableCharacter;
    }

    public void Update()
    {
        /*if (equipped)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                equipped = false;
            if (!equipped)
                gameObject.SetActive(false);
        }*/
    }


    public virtual void AddIcon(Sprite icon){
        this.icon = icon;
    }

    public void ItemUsage()
    {
        if (type == "equipable")
        {
            weapon.SetActive(true);
            //weapon.GetComponent<BaseItem>().equipped = true;          
        }
        /*else if (type == "temporal")
        {
            boost.SetActive(true);
            weapon.GetComponent<BaseItem>().equipped = true;
        }
        else if (type == "consumible")
        {
            potion.SetActive(true);
            weapon.GetComponent<BaseItem>().equipped = true;
        }*/
    }
}

[System.Serializable]
public class ItemData
{
    public BaseItem[] items;
}