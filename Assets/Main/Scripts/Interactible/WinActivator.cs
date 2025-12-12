using System.Collections;
using UnityEngine;

public class WinActivator : MonoBehaviour, Iinteractable
{
    private GameObject _player;
    private AudioSource _winSequenceAudioSource;
    [SerializeField] private AudioClip[] _winSequenceAudioClips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        _winSequenceAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void interact()
    {
        _player.GetComponent<PlayerMovement>().canMove = false;
        _player.GetComponent<PlayerHealth>().destroyAllEnemies();
        Cursor.lockState = CursorLockMode.Confined;
        UIManger.instance.winScreen();
        StartCoroutine(VictoryThenLosePlayer());
    }

    IEnumerator VictoryThenLosePlayer()
    {
        _winSequenceAudioSource.Stop();
        _winSequenceAudioSource.clip = _winSequenceAudioClips[0];
        _winSequenceAudioSource.Play();

        yield return new WaitForSeconds(8f);

        _winSequenceAudioSource.Stop();
        _winSequenceAudioSource.clip = _winSequenceAudioClips[1];
        _winSequenceAudioSource.Play();
    }
}
