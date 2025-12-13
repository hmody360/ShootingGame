using Unity.VisualScripting;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float damage;
    [SerializeField] private ParticleSystem _impactVFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy")) //if the shot hits the enemy, damage it and make special VFX to que the player that the enemy was damaged, then destroy self, if anything else is hit, just destroy self.
        {
            IDamageable enemyHealth = other.gameObject.GetComponent<IDamageable>();

            enemyHealth.takeDamage(damage);
            _impactVFX.Play();
            Destroy(gameObject,0.5f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
