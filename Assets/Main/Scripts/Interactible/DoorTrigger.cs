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
        if ((other.CompareTag("Player")) && !canOpen)
        {
            _doorAudioSource.PlayOneShot(_doorClipList[0]);
        }
        if ((other.CompareTag("Player") || other.CompareTag("Enemy")) && canOpen)
        {
            _doorAudioSource.PlayOneShot(_doorClipList[1]);
            Debug.Log("player opened the door");
            _doorAnimator.SetBool("isOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("Enemy")) && canOpen)
        {
            _doorAudioSource.PlayOneShot(_doorClipList[2]);
            Debug.Log("player closed the door");
            _doorAnimator.SetBool("isOpen", false);
        }
    }
}
