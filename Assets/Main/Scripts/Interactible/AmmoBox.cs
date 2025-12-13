using Unity.VisualScripting;
using UnityEngine;

public class AmmoBox : MonoBehaviour, Iinteractable
{
    // Ammo Box Parameters
    public int ammoAmount;
    private RayGun _weapon;
    private AudioSource _reloadSoundSource;
    [SerializeField] private AudioClip[] _ammoBoxClips;

    private void Awake()
    {
        _reloadSoundSource = GetComponent<AudioSource>(); //Get Audio Source of Weapon
    }

    private void Start()
    {
        _weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<RayGun>(); //Find the Weapon using its tag
    }

    public void interact() // On interaction, if the weapon is held, and isn't full of ammo, reload the weapon and destroy the box, otherwise play deny sound.
    {
        if( _weapon != null )
        {
            if (_weapon.isEqipped && _weapon.currentAmmo < _weapon.maxAmmo)
            {
                _weapon.reloadRayGun(ammoAmount);
                _reloadSoundSource.PlayOneShot(_ammoBoxClips[1]);
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<MeshCollider>().enabled = false;
                Destroy(gameObject,0.5f);
            }
            else
            {
                _reloadSoundSource.PlayOneShot(_ammoBoxClips[0]);
                Debug.Log("Your Weapon is not eqipped or you have max ammo");
            }
        }
        else
        {
            Debug.Log("Weapon Not Found In Scene!!");
        }
    }
}
