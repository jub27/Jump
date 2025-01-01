using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = target.position;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.15f);
    }
}
