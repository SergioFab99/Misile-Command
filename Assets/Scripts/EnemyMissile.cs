using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyMissile : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    private Vector3 target;
    private bool alive = true;

    private void Awake()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnEnable()
    {
        Invoke(nameof(DespawnSelf), 6f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void Initialize(Vector3 dir, City targetCity)
    {
        target = targetCity != null ? targetCity.transform.position : transform.position + dir * 100f;
        target.y = transform.position.y;
        alive = true;
    }

    public void SetTarget(Vector3 newTarget)
    {
        target = newTarget;
        target.y = transform.position.y;
    }

    private void Update()
    {
        if (!alive) return;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!alive) return;

        if (other.TryGetComponent<PlayerMissile>(out var playerMissile))
        {
            playerMissile.Explode();
            DestroyByExplosion();
            return;
        }

        if (other.TryGetComponent<IDamageable>(out var dmg))
        {
            dmg.TakeDamage(1);
            DestroyByExplosion();
            return;
        }
    }

    public void DestroyByExplosion()
    {
        if (!alive) return;
        alive = false;
        ObjectPool.I.Despawn(gameObject);
    }

    private void DespawnSelf()
    {
        if (gameObject.activeSelf)
            ObjectPool.I.Despawn(gameObject);
    }
}
