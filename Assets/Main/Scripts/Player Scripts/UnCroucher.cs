using UnityEngine;

public class UnCroucher : MonoBehaviour
{
    public bool Toggle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().toggleUncroucher(Toggle);
        }
    }

}
