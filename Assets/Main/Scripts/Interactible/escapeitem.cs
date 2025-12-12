using UnityEngine;

public class escapeitem : MonoBehaviour, Iinteractable
{
    
    public itemData itemData;

    [SerializeField] private GameObject Player;
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
                Destroy(gameObject);
            }
        }
    }
}
