using UnityEngine;
using UnityEngine.Pool;

public class CollisionEffect : MonoBehaviour
{
    public ObjectPool<CollisionEffect> objectPool;
    private ParticleSystem _particleSystem;

    private void Awake() {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void OnGetFromPool()
    {
        gameObject.SetActive(true);
        _particleSystem.Play(true);
    }

    public void OnReleaseFromPool()
    {
        gameObject.SetActive(false);
    }

    public void OnDestroyFromPool()
    {
        Destroy(gameObject);
    }

    private void OnParticleSystemStopped() {
        objectPool.Release(this);
    }
}
