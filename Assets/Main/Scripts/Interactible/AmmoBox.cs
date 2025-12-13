using Unity.VisualScripting;
using UnityEngine;

public class AmmoBox : MonoBehaviour, Iinteractable
{
    public int ammoAmount;

    private RayGun _weapon;
    private AudioSource _reloadSoundSource;
    [SerializeField] private AudioClip[] _ammoBoxClips;

    private void Awake()
    {
        _reloadSoundSource = GetComponent<AudioSource>();
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
