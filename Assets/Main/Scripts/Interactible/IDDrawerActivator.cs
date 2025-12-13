using UnityEngine;

public class IDDrawerActivator : MonoBehaviour, Iinteractable
{
    private Animator _drawerAnimator;
    private Outline _outline;
    private Collider _collider;
    private AudioSource _DrawerAudio;
    [SerializeField] private AudioClip[] _audioClipList;


    private void Awake()
    {
        _drawerAnimator = GetComponent<Animator>();
        _outline = GetComponent<Outline>();
        _collider = GetComponent<Collider>();
        _DrawerAudio = GetComponent<AudioSource>();
    }

    public void interact()
    {
        UIManger.instance.StartDrawerMinigame();
        _DrawerAudio.clip = _audioClipList[0];
        _DrawerAudio.Play();
    }

    public void OpenDrawer()
    {
        _drawerAnimator.SetBool("isOpen", true);
        _DrawerAudio.clip = _audioClipList[1];
        _DrawerAudio.Play();
        _collider.enabled = false;
        Destroy(_outline);
    }

    public void CloseGame()
    {
        _DrawerAudio.Stop();
    }


}
