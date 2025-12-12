using UnityEngine;

public class escapeitem : MonoBehaviour, Iinteractable
{
    
    public itemData itemData;

    [SerializeField] private GameObject Player;
    [SerializeField] private int objectiveID;
    [SerializeField] private bool destroyOnInteract = true; //to destroy the oxygen tank and teddy NOT the ID drawer

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void interact()
    {
        if (Player != null)
        {
            Player.GetComponent<PlayerInventory>().Additem(itemData);
            if (destroyOnInteract)  //-for destroying the picked up items not the ID drawer
            {

                if(objectiveID != 0)
                {
                    UIManger.instance.objectiveList.Find(obj => obj.id == objectiveID).isActive = false;
                    UIManger.instance.updateObjectiveList();
                }

                Destroy(gameObject);
            }
        }
    }
}
