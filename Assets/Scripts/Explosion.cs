using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Explosion : MonoBehaviour
{
    public float radius = 1.5f;
    public LayerMask damageMask;
    public float lifeTime = 0.4f;

    private void OnEnable()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, damageMask);
        foreach (var h in hits)
        {
            if (h.TryGetComponent<IDamageable>(out var d)) d.TakeDamage(1);
            if (h.TryGetComponent<EnemyMissile>(out var em)) em.DestroyByExplosion();
        }
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
