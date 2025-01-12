using JyCustomTool;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

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
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip collisionSound;
    [SerializeField] private CollisionEffect collisionEffect1;
    [SerializeField] private CollisionEffect collisionEffect2;
    private ObjectPool<CollisionEffect> effect1Pool;
    private ObjectPool<CollisionEffect> effect2Pool;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        effect1Pool = new ObjectPool<CollisionEffect>(OnCreateEffect1, OnGetEffect, OnRealeaseEffect, OnDestroyFromPool, true, 5, 25);
        effect2Pool = new ObjectPool<CollisionEffect>(OnCreateEffect2, OnGetEffect, OnRealeaseEffect, OnDestroyFromPool, true, 5, 25);
    }

    private CollisionEffect OnCreateEffect1()
    {
        CollisionEffect effect = Instantiate(collisionEffect1);
        effect.objectPool = effect1Pool;
        return effect;
    }
    private CollisionEffect OnCreateEffect2()
    {
        CollisionEffect effect = Instantiate(collisionEffect2);
        effect.objectPool = effect2Pool;
        return effect;
    }

    private void OnGetEffect(CollisionEffect effect)
    {
        effect.OnGetFromPool();
    }

    private void OnRealeaseEffect(CollisionEffect effect)
    {
        effect.OnReleaseFromPool();
    }

    private void OnDestroyFromPool(CollisionEffect effect)
    {
       effect.OnDestroyFromPool();
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
        SoundManager.Instance.PlaySE(jumpSound, true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        foreach( var contact in other.contacts)
        {
            CollisionEffect effect = Random.Range(0, 2) == 0 ? effect1Pool.Get() : effect2Pool.Get();
            effect.transform.position = contact.point;
            SoundManager.Instance.PlaySE(collisionSound, true);
        }
    }
    
}
