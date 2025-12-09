using UnityEngine;

[RequireComponent(typeof(Outline))]
public class OutlineController : MonoBehaviour
{
    private Outline outline;
    private bool playerNearby = false;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false; // start disabled
    }

    void Update()
    {
        if (playerNearby)
        {
            // Cast a ray from the center of the screen
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    outline.enabled = true; // looking at this object
                }
                else
                {
                    outline.enabled = false; // looking elsewhere
                }
            }
        }
        else
        {
            outline.enabled = false; // not nearby
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
