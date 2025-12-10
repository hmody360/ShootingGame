using UnityEngine;

public class escapeitem : MonoBehaviour, Iinteractable
{
    
    public itemData itemData;

    private AudioSource _audioSource;
    private Collider _collider;
    private Renderer _renderer;

    [SerializeField] private GameObject Player;
    [SerializeField] private bool destroyOnInteract = true; //to destroy the oxygen tank and teddy NOT the ID drawer

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
    }

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
                _audioSource.Play();
                _collider.enabled = false;
                _renderer.enabled = false;
                Destroy(gameObject, 3f);
            }
        }
    }
}
