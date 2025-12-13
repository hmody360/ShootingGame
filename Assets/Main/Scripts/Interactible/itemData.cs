using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "BoyAnomaly/Item")] //Creating the menu selection for creating ItemData.
public class itemData : ScriptableObject
{
    public int itemID; //Used to identify the item picked.
    public string itemName;
    public Sprite icon; //used to display in the Player's Inventory.
}
