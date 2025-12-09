using UnityEngine;

public class escapeitem : MonoBehaviour, Iinteractable
{
    [SerializeField] private GameObject Player;
    private RayInetractor Hit; //bring in an element fromanother script, private (script name) (name in this file)

    [SerializeField] private bool destroyOnInteract = true; //to destroy the oxygen tank and teddy NOT the ID drawer


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        //Destroy(Hit.transform.root.gameObject); //wtvthe ray hits (mesh or collider) the "root" part gets to the parent of the object and destroys it
    }
    public void interact()
    {
        if (Player != null)
        {
            Player.GetComponent<PlayerInventory>().Additem(gameObject);
            if (destroyOnInteract)  //-for destroying the picked up items not the ID drawer
            {
                Destroy(transform.root.gameObject);
            }
        }
    }
}
