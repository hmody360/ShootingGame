using UnityEngine;

public class HealthVial : MonoBehaviour, Iinteractable
{
    public float healAmount;
    public float damageAmount;

    private PlayerHealth _healthToAffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _healthToAffect = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void interact()
    {
        int healOrDamage = Random.Range(0, 2);

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
