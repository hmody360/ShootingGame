using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth;
    public float currentHealth;

    private PlayerMovement _pMovement;
    
    [SerializeField] private AudioSource _deathAudioSource;


    private void Awake()
    {
        _pMovement = GetComponent<PlayerMovement>();
    }


    public void takeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    public void onDeath()
    {
        if (_pMovement != null)
        {
            _pMovement.canMove = false;
            _deathAudioSource.Play();
        }
    }




}
