using UnityEngine;

public class escapeitem : MonoBehaviour, Iinteractable
{
    [SerializeField] private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        
    }
    public void interact()
    {
        if (Player != null)
        {
            Player.GetComponent<PlayerInventory>().Additem(gameObject);
        }
    }
}
