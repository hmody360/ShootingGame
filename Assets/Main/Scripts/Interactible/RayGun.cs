using System.Linq;
using UnityEngine;

public class RayGun : MonoBehaviour, Iinteractable
{

    public GameObject shotPrefab; //Prefab to shoot from gun
    //RayGun Parameters
    public int maxAmmo = 30;
    public int currentAmmo = 10;
    public float shootForce = 50f;
    public float fireRate = 0.2f;
    public bool isEqipped = false;

    private Rigidbody _weaponRB;
    private BoxCollider _weaponCollider;
    private Transform _weaponSlot;
    private AudioSource _weaponAudioSource;
    private bool _firsteqipped = false;

    [SerializeField] private AudioClip[] _weaponSounds;
    [SerializeField] private float _fireTimer;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private ParticleSystem _MuzzleShot;
    public string ActionName => "Eqip Raygun";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _weaponRB = GetComponent<Rigidbody>();
        _weaponCollider = GetComponent<BoxCollider>();
        _weaponAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _weaponSlot = GameObject.FindGameObjectWithTag("WeaponSlot").transform; //Get the slot that the weapon is placed in.
        _cameraTransform = Camera.main.transform; //get the camera to shoot from its center.
        UIManger.instance.UpdateAmmo(currentAmmo, maxAmmo); //update the ammo in the UI from the begininng.
    }

    private void Update()
    {
        _fireTimer += Time.deltaTime;
        shoot();
        unEquip();
    }

    public void interact()
    {
        if (!isEqipped) //if the weapon is not equipped, and the weapon slot is not null, equip it
        {
            if (_weaponSlot != null)
            {
                transform.SetParent(_weaponSlot.transform);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                _weaponRB.isKinematic = true;
                _weaponCollider.enabled = false;
                _weaponAudioSource.PlayOneShot(_weaponSounds[0]);
                UIManger.instance.ToggleWeaponHUD();
                isEqipped = true;

                if (!_firsteqipped) //this is to check off the objective once the gun is picked up.
                {
                    UIManger.instance.objectiveList.Find(obj => obj.id == 2).isActive = false;
                    UIManger.instance.updateObjectiveList();
                    _firsteqipped = true;
                }
            }
        }
    }

    private void unEquip() //if the gun is eqipped, and unEqip button is pressed, drop the gun.
    {
        if (isEqipped && Input.GetKeyDown(KeyCode.Q))
        {
            this.transform.SetParent(null);
            _weaponRB.isKinematic = false;
            _weaponCollider.enabled = true;
            _weaponAudioSource.PlayOneShot(_weaponSounds[1]);
            UIManger.instance.ToggleWeaponHUD();

            isEqipped = false;
        }
    }

    private void shoot() //Shoot from the center of the camera if there's ammo, play the related sounds and VFX, and update the UI if no ammo, play the no ammo sound.
    {
        if (Input.GetMouseButtonDown(1) && _fireTimer > fireRate && isEqipped)
        {
            if (currentAmmo > 0)
            {
                _fireTimer = 0f;
                currentAmmo--;
                _weaponAudioSource.PlayOneShot(_weaponSounds[2]);
                _MuzzleShot.Play();

                Vector3 spawnPos = _cameraTransform.position + _cameraTransform.forward * 0.7f;
                GameObject Shot = Instantiate(shotPrefab, spawnPos, Quaternion.identity);

                Rigidbody shotRB = Shot.GetComponent<Rigidbody>();
                shotRB.linearVelocity = _cameraTransform.forward * shootForce;
                UIManger.instance.UpdateAmmo(currentAmmo, maxAmmo);
            }
            else
            {
                _weaponAudioSource.PlayOneShot(_weaponSounds[3]);
                _fireTimer = 0f;
            }
        }
        {
        }
    }

    public void reloadRayGun(int noOfAmmo) //This is used by AmmoBoxes to reload the ammo, and updates it in UI
    {
        currentAmmo += noOfAmmo;

        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }

        UIManger.instance.UpdateAmmo(currentAmmo, maxAmmo);
    }
}
