using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Values")]
    public float maxStamina = 100;
    public float currentStamina;

    [Header("Drain")]
    public float runDrainAmount = 8f;  
    public float walkDrainAmount = 3f;
    public float jumpCost = 15f;        

    [Header("Regen")]
    public float regenRate = 10f;      

    [Header("State Check")]
    public bool isRunning;
    public bool isWalking;
    public bool isGrounded;

    void Start()
    {
        currentStamina = maxStamina;
    }

    void Update()
    {
        HandleStamina();
    }

    void HandleStamina()
    {
       
        if (isWalking && currentStamina > 0)
            currentStamina -= walkDrainAmount * Time.deltaTime;

        
        if (isRunning && currentStamina > 0)
            currentStamina -= runDrainAmount * Time.deltaTime;

       
        if (!isWalking && !isRunning)
            currentStamina += regenRate * Time.deltaTime;

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

    public void OnJump()
    {
        if (currentStamina > jumpCost)
            currentStamina -= jumpCost;
    }
}
