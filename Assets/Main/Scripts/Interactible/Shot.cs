using Unity.VisualScripting;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            IDamageable enemyHealth = other.gameObject.GetComponent<IDamageable>();

            enemyHealth.takeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
