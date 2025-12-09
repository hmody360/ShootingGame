using UnityEngine;

public class PlayerHealth1 : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public MainMenu ui; 

    void Start()
    {
        currentHealth = maxHealth;
        //UpdateUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

       // UpdateUI();

        if (currentHealth == 0)
        {
            ui.LoseScreen();    
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        //UpdateUI();
    }

    void UpdateUI()
    {
       // UIManager_FightScene.instance.updatePlayerHealth(currentHealth, maxHealth);
    }
}

