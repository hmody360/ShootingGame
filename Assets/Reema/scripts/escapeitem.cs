using UnityEngine;

public class escapeitem : MonoBehaviour, Iinteractable
{
    [SerializeField] private GameObject Player;
    private RayInetractor Hit; //bring in an element fromanother script, private (script name) (name in this file)

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Destroy(Hit.transform.root.gameObject); //wtvthe ray hits (mesh or collider) the "root" part gets to the parent of the object and destroys it
    }
    public void interact()
    {
        if (Player != null)
        {
            Player.GetComponent<PlayerInventory>().Additem(gameObject);
        }
    }
}
