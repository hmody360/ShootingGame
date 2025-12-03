using UnityEngine;

public class RayInetractor : MonoBehaviour
{
    private Camera _camera;
    private Ray _rayToCast;
    private GameObject _lastHitGameObject;
    [SerializeField] private float _maxDistance = 6f;
    [SerializeField] LayerMask _interactible;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        _rayToCast = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        if (Physics.Raycast(_rayToCast, out RaycastHit hit, _maxDistance, _interactible))
        {
            GameObject currentGameObject = hit.collider.gameObject;

            if (currentGameObject != _lastHitGameObject)
            {
                if(_lastHitGameObject != null)
                {
                    
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                Iinteractable item = currentGameObject.transform.GetComponent<Iinteractable>();

                if (item != null)
                {
                    item.interact();
                }
                Debug.Log("Looking at:" + hit.transform.name);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_rayToCast);

    }
}
