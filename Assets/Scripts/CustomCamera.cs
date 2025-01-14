using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    public bool isFollow;
    
    public static CustomCamera Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isFollow)
            return;
        Vector3 targetPosition = target.position;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.15f);
    }
}
