using UnityEngine;

public class HealthVial : MonoBehaviour, Iinteractable
{
    public float healAmount;
    public float damageAmount;
    private AudioSource _audioSource;

    private PlayerHealth _healthToAffect;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _healthToAffect = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void interact()
    {
        if (_healthToAffect.currentHealth >= _healthToAffect.maxHealth)
        {
            _audioSource.Play();
            return;
        }
            
        int healOrDamage = Random.Range(0, 10); //Randomly select between healing or damaging the player 50/50 %

        if (healOrDamage == 0)
        {
            _healthToAffect.takeDamage(damageAmount);
        }
        else
        {
            _healthToAffect.heal(healAmount);
        }

        Destroy(gameObject);
    }
}
