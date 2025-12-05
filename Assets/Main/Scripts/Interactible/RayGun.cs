using UnityEngine;

public class RayGun : MonoBehaviour, Iinteractable
{

    public GameObject shotPrefab;
    public int maxAmmo = 30;
    public int currentAmmo = 10;
    public float shootForce = 50f;
    public float fireRate = 0.2f;

    private Rigidbody _weaponRB;
    private BoxCollider _weaponCollider;
    private Transform _weaponSlot;
    private AudioSource _weaponAudioSource;

    [SerializeField] private AudioClip[] _weaponSounds;
    [SerializeField] private float _fireTimer;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private ParticleSystem _MuzzleShot;
    [SerializeField] private bool isEqipped = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _weaponRB = GetComponent<Rigidbody>();
        _weaponCollider = GetComponent<BoxCollider>();
        _weaponAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _weaponSlot = GameObject.FindGameObjectWithTag("WeaponSlot").transform;
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        _fireTimer += Time.deltaTime;
        shoot();
        unEquip();
    }

    public void interact()
    {
        if (!isEqipped)
        {
            if (_weaponSlot != null)
            {
                transform.SetParent(_weaponSlot.transform);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                _weaponRB.isKinematic = true;
                _weaponCollider.enabled = false;
                _weaponAudioSource.PlayOneShot(_weaponSounds[0]);

                isEqipped = true;
            }
        }
    }

    private void unEquip()
    {
        if (isEqipped && Input.GetKeyDown(KeyCode.Q))
        {
            this.transform.SetParent(null);
            _weaponRB.isKinematic = false;
            _weaponCollider.enabled = true;
            _weaponAudioSource.PlayOneShot(_weaponSounds[1]);

            isEqipped = false;
        }
    }

    private void shoot()
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

            }
        }
        {
        }
    }

    public void reloadRayGun(int noOfAmmo)
    {
        currentAmmo += noOfAmmo;

        if(currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
    }
}
