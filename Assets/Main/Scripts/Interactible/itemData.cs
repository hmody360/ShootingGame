using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "BoyAnamoly/Item")]
public class itemData : ScriptableObject
{
    
    public int itemID;
    public string itemName;
    public Sprite icon;
}
