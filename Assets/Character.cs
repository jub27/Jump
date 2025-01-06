using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    private const float MAX_FORCE = 13f;
    private Rigidbody2D _rigidbody;
    private Vector2 downPosition;
    private bool _isCanJump;
    private SpriteRenderer _spriteRenderer;
    public Color idleColor;
    public Color onJumpColor;
    [SerializeField] private Transform arrow;

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

    public void OnPointerMove(BaseEventData eventData)
    {
        Debug.Log("Move");
        PointerEventData pointerEventData = (PointerEventData)eventData;
        arrow.localScale = Vector3.one * Mathf.Min( (downPosition - pointerEventData.position).magnitude / 100.0f, MAX_FORCE / 2.0f );
        arrow.right = downPosition - pointerEventData.position;
    }

    public void OnPointerUp(BaseEventData eventData)
    {
        if (!_isCanJump)
            return;
        PointerEventData pointerEventData = (PointerEventData)eventData;
        Jump(downPosition - pointerEventData.position);
        arrow.gameObject.SetActive(false);
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        if (!_isCanJump)
            return;
        PointerEventData pointerEventData = (PointerEventData)eventData;
        downPosition = pointerEventData.position;
        arrow.gameObject.SetActive(true);
        arrow.localScale = Vector3.one * (downPosition - pointerEventData.position).magnitude / 100.0f;
        arrow.right = downPosition - pointerEventData.position;
    }

    void Jump(Vector2 direction)
    {
        float force = Mathf.Min(direction.magnitude / 50.0f, MAX_FORCE);
        _rigidbody.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }
    
}
