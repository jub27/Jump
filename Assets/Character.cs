using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    private const float MAX_FORCE = 10f;
    private Rigidbody2D _rigidbody;
    private Vector2 downPosition;
    private bool _isCanJump;
    private SpriteRenderer _spriteRenderer;
    public Color idleColor;
    public Color onJumpColor;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _isCanJump = (_rigidbody.linearVelocity == Vector2.zero) && (_rigidbody.angularVelocity == 0);
        if(_isCanJump)
        {
            _spriteRenderer.color = idleColor;
        }
        else
        {
            _spriteRenderer.color = onJumpColor;
        }
    }

    public void OnPointerUp(BaseEventData eventData)
    {
        if (!_isCanJump)
            return;
        PointerEventData pointerEventData = (PointerEventData)eventData;
        Debug.Log(pointerEventData.position);
        Jump(downPosition - pointerEventData.position);
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        if (!_isCanJump)
            return;
        PointerEventData pointerEventData = (PointerEventData)eventData;
        Debug.Log(pointerEventData.position);
        downPosition = pointerEventData.position;
    }

    void Jump(Vector2 direction)
    {
        float force = Mathf.Min(direction.magnitude, MAX_FORCE);
        _rigidbody.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }
    
}
