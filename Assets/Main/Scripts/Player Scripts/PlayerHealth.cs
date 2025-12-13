using Unity.Cinemachine;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth;
    public float currentHealth;

    private PlayerMovement _pMovement;

    [SerializeField] private AudioSource _damageAudioSource;
    [SerializeField] private AudioClip[] _damageAudioClips;
    [SerializeField] private GameObject _FirstPersonCamera;


    private void Awake()
    {
        _pMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        UIManger.instance.UpdateHealth(currentHealth, maxHealth);
    }


    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        _damageAudioSource.PlayOneShot(_damageAudioClips[0]);

        if (currentHealth <= 0)
        {
            onDeath();
            currentHealth = 0;
        }

        UIManger.instance.UpdateHealth(currentHealth, maxHealth);
    }

    public void heal(float healAmount)
    {
        currentHealth += healAmount;
        _damageAudioSource.PlayOneShot(_damageAudioClips[2]);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIManger.instance.UpdateHealth(currentHealth, maxHealth);
    }

    public void onDeath()
    {
        if (_pMovement != null)
        {
            _pMovement.canMove = false;
            _damageAudioSource.PlayOneShot(_damageAudioClips[1]);
            destroyAllEnemies();
            _FirstPersonCamera.SetActive(false);
            UIManger.instance.LoseScreen();
        }
    }

    public void destroyAllEnemies()
    {
        GameObject[] enemiesList = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject enemy in enemiesList)
        {
            Destroy(enemy);
        }
    }




}
