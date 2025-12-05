using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemlist = new List <GameObject>();

    public void Additem(GameObject item)
    {
        itemlist.Add(item);
    }
}
