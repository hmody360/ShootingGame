using UnityEngine;

public class VentOpen : MonoBehaviour, Iinteractable
{
    private PlayerInventory _pInventory;
    private AudioSource _ventAudioSource;

    [SerializeField] private AudioClip[] _ventAudioList;

    private void Awake()
    {
        _ventAudioSource = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }


    public void interact()
    {
        if(_pInventory != null )
        {
            if(_pInventory.getInventoryList().Find(tool => tool.itemID == 1))
            {
                _ventAudioSource.PlayOneShot(_ventAudioList[1]);
                Destroy(gameObject, 3f);
            }
            else
            {
                _ventAudioSource.PlayOneShot(_ventAudioList[0]);
            }
        }
    }
}
