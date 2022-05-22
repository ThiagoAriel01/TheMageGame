using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData : MonoBehaviour
{
    public BaseItem[] items;

    public void CreateArray()
    {
        items = new BaseItem[9];
    }

    public void AddItemToArray(BaseItem _item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = _item;
                return;
            }
        }
    }

    public void DeleteItem(int slot)
    {
        items[slot] = null;
    }

    public BaseItem GetItem(int slot)
    {
        return (items[slot]);
    }
}
