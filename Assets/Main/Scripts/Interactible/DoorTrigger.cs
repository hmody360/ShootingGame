using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public bool canOpen = true;

    private Animator _doorAnimator;
    private AudioSource _doorAudioSource;

    [SerializeField] private AudioClip[] _doorClipList;

    private void Awake()
    {
        _doorAnimator = GetComponent<Animator>();
        _doorAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player")) && !canOpen) //if player tries to open the ddor while closed, play Deny sound
        {
            _doorAudioSource.PlayOneShot(_doorClipList[0]);
        }
        if ((other.CompareTag("Player") || other.CompareTag("Enemy")) && canOpen) //if player or enemy is close to the door, and the door can be opened, open the door and make open door sound
        {
            _doorAudioSource.PlayOneShot(_doorClipList[1]);
            Debug.Log("player opened the door");
            _doorAnimator.SetBool("isOpen", true);
        }
    }

    private void OnTriggerExit(Collider other) // if player or enemy leaves the vicinity of the door close it and play close door sound
    {
        if ((other.CompareTag("Player") || other.CompareTag("Enemy")) && canOpen)
        {
            _doorAudioSource.PlayOneShot(_doorClipList[2]);
            Debug.Log("player closed the door");
            _doorAnimator.SetBool("isOpen", false);
        }
    }
}
