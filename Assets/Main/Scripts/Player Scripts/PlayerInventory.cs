using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<itemData> itemlist = new List<itemData>();

    public void Additem(itemData item)
    {
        itemlist.Add(item);
    }

    public void removeItem(int ID)
    {
        itemlist.Remove(itemlist.Find(item => item.itemID == ID));
    }

    public List<itemData> getInventoryList()
    {
        return itemlist;
    }
}
