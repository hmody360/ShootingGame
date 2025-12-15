using UnityEngine;

public class IDDrawerActivator : MonoBehaviour, Iinteractable
{
    private Animator _drawerAnimator;
    private Outline _outline;
    private Collider _collider;
    private AudioSource _DrawerAudio;
    [SerializeField] private AudioClip[] _audioClipList;
    public string ActionName => "Look For ID";


    private void Awake()
    {
        _drawerAnimator = GetComponent<Animator>();
        _outline = GetComponent<Outline>();
        _collider = GetComponent<Collider>();
        _DrawerAudio = GetComponent<AudioSource>();
    }

    public void interact() //On Interact Start the Drawer Minigame, and play its music
    {
        if (UIManger.instance.PauseGamePanel.activeSelf)
            return;
        UIManger.instance.StartDrawerMinigame();
        _DrawerAudio.clip = _audioClipList[0];
        _DrawerAudio.Play();
    }

    public void OpenDrawer() //If the player wins, this opens the drawer, and disables the collider and outline (no longer can open the mini game)
    {
        _drawerAnimator.SetBool("isOpen", true);
        _DrawerAudio.clip = _audioClipList[1];
        _DrawerAudio.Play();
        _collider.enabled = false;
        Destroy(_outline);
    }

    public void CloseGame() //If the game is closed stop the mini game's music.
    {
        _DrawerAudio.Stop();
    }


}
