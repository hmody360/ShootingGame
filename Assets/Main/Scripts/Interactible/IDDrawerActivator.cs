using UnityEngine;

public class IDDrawerActivator : MonoBehaviour, Iinteractable
{
    private Animator _drawerAnimator;
    private Outline _outline;
    private Collider _collider;

    private void Awake()
    {
        _drawerAnimator = GetComponent<Animator>();
        _outline = GetComponent<Outline>();
        _collider = GetComponent<Collider>();
    }

    public void interact()
    {
        _drawerAnimator.SetBool("isOpen", true);
        _collider.enabled = false;
        Destroy(_outline);
    }


}
