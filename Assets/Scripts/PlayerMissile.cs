using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerMissile : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    public Vector3 direction;
    public float explodeRadius = 2f;
    public LayerMask damageMask;

    private bool alive = false;

    private void Awake()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    public void Initialize(Vector3 dir)
    {
        direction = new Vector3(dir.x, 0, dir.z).normalized;
        alive = true;
    }

    private void Update()
    {
        if (!alive) return;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!alive) return;

        if (other.TryGetComponent<EnemyMissile>(out var enemy))
        {
            enemy.DestroyByExplosion();
            Explode();
            return;
        }

        if (other.TryGetComponent<IDamageable>(out var dmg))
        {
            dmg.TakeDamage(1);
            Explode();
            return;
        }

        Explode();
    }

    public void Explode()
    {
        if (!alive) return;
        alive = false;

        Collider[] hits = Physics.OverlapSphere(transform.position, explodeRadius, damageMask);
        foreach (var h in hits)
        {
            if (h.TryGetComponent<EnemyMissile>(out var e))
                e.DestroyByExplosion();
            if (h.TryGetComponent<IDamageable>(out var d))
                d.TakeDamage(1);
        }

        ObjectPool.I.Despawn(gameObject);
    }
}
