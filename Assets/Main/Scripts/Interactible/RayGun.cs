using UnityEngine;

public class RayGun : MonoBehaviour, Iinteractable
{
    public int maxAmmo = 30;
    public int currentAmmo = 10;

    [SerializeField] private bool isEqipped = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void interact()
    {
        if (!isEqipped)
        {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            this.gameObject.transform.SetParent(Player.transform, false);
            isEqipped = true;
        }
        
    }
}
