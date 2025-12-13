using UnityEngine;

public class UnCroucher : MonoBehaviour
{
    public bool Toggle;

    private void OnTriggerEnter(Collider other) //Disable or Enable Crouching based on player entry.
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().toggleUncroucher(Toggle);
        }
    }

}
