using UnityEngine;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour
{
    private const float MAX_FORCE = 10f;
    private Rigidbody2D rigidbody;
    private Vector2 downPosition;
    public void OnPointerUp(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        Debug.Log(pointerEventData.position);
        Jump(downPosition - pointerEventData.position);
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        Debug.Log(pointerEventData.position);
        downPosition = pointerEventData.position;
    }

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }    

    void Jump(Vector2 direction)
    {
        float force = Mathf.Min(direction.magnitude, MAX_FORCE);
        rigidbody.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }
    
}
