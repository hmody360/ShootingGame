using Unity.VisualScripting;
using UnityEngine;

public class AmmoBox : MonoBehaviour, Iinteractable
{
    public int ammoAmount;

    private RayGun _weapon;
    private AudioSource _reloadSound;

    private void Awake()
    {
        _reloadSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<RayGun>();
    }

    public void interact()
    {
        if( _weapon != null )
        {
            if (_weapon.isEqipped && _weapon.currentAmmo < _weapon.maxAmmo)
            {
                _weapon.reloadRayGun(ammoAmount);
                _reloadSound.Play();
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<MeshCollider>().enabled = false;
                Destroy(gameObject,0.5f);
            }
            else
            {
                Debug.Log("Your Weapon is not eqipped or you have max ammo");
            }
        }
        else
        {
            Debug.Log("Weapon Not Found In Scene!!");
        }
    }
}
