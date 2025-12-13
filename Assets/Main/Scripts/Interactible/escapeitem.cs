using UnityEngine;

public class escapeitem : MonoBehaviour, Iinteractable
{
    
    public itemData itemData; //itemData Scriptable Object

    [SerializeField] private GameObject Player;
    [SerializeField] private int objectiveID;
    [SerializeField] private bool destroyOnInteract = true;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void interact()
    {
        if (Player != null)
        {
            Player.GetComponent<PlayerInventory>().Additem(itemData); //Add item to player Inventory
            if (destroyOnInteract)  //-for destroying the picked up items
            {

                if(objectiveID != 0) //if objective ID isn't 0, deactivate the related objective and update the UI to display its removal
                {
                    UIManger.instance.objectiveList.Find(obj => obj.id == objectiveID).isActive = false;
                    UIManger.instance.updateObjectiveList();
                }

                Destroy(gameObject);
            }
        }
    }
}
