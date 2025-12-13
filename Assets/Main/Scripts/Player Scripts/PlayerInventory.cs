using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<itemData> itemlist = new List<itemData>();
    [SerializeField] private AudioSource _inventoryAudioSource;
    [SerializeField] private AudioClip[] _inventoryAudioClips;
    private bool isOpen = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //Open/Close Inventory UI and play the respective sound
        {
            UIManger.instance.ToggleCollectibles();

            if (isOpen)
            {
                _inventoryAudioSource.PlayOneShot(_inventoryAudioClips[0]);
            }
            else
            {
                _inventoryAudioSource.PlayOneShot(_inventoryAudioClips[1]);
            }

            isOpen = isOpen ? false : true;
        }

        
    }



    public void Additem(itemData item) //Add item to the inventory and update UI
    {
        itemlist.Add(item);
        _inventoryAudioSource.PlayOneShot(_inventoryAudioClips[2]);
        UIManger.instance.AddCollectible(item.icon);
        checkFinalMissionTrigger();
    }

    public void removeItem(int ID) //Remove item from the inventory and update UI
    {
        itemData itemToRemove = itemlist.Find(item => item.itemID == ID);
        itemlist.Remove(itemToRemove);
        UIManger.instance.RemoveCollectible(itemToRemove.icon);
    }

    public void checkFinalMissionTrigger() //Check if all 3 Main Collectibles have been collected to activate the Final Mission
    {
        if(itemlist.Count >= 3)
        {
            UIManger.instance.objectiveList.Find(obj => obj.id == 6).isActive = true;
            UIManger.instance.updateObjectiveList();
        }
    }

    public List<itemData> getInventoryList() // return the inventory.
    {
        return itemlist;
    }
}
