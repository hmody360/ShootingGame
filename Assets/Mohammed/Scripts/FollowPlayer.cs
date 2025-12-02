using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Target;

    [SerializeField] private Vector3 offset = new Vector3(0f, 10f, -10f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Target.position + offset;
    }
}
