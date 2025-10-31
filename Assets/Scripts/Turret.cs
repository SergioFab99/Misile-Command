using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class Turret : BaseEntity, IDamageable
{
    [SerializeField] protected GameObject missilePrefab;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected int maxAmmo = 10;
    [SerializeField] protected float fireCooldown = 0.3f;
    [SerializeField] protected float disabledDuration = 5f;

    protected int ammo;
    protected bool isDisabled;
    protected float lastFire;

    private System.Action<Vector3> clickHandler;

    public override void Initialize()
    {
        ammo = maxAmmo;
        clickHandler = pos => TryFire(pos);
        EventBus.I.OnPlayerClick += clickHandler;
        EventBus.I.AmmoChanged(ammo);
    }

    public override void OnDestroyed()
    {
        if (EventBus.I != null) EventBus.I.OnPlayerClick -= clickHandler;
    }

    protected void TryFire(Vector3 pos)
    {
        if (isDisabled || ammo <= 0 || Time.time - lastFire < fireCooldown) return;
        lastFire = Time.time;
        ammo--;
        EventBus.I.AmmoChanged(ammo);
        Fire(pos);
    }

    protected abstract void Fire(Vector3 pos);

    public void TakeDamage(int amount)
    {
        if (isDisabled) return;
        StartCoroutine(DisableForSeconds());
    }

    private IEnumerator DisableForSeconds()
    {
        isDisabled = true;
        yield return new WaitForSeconds(disabledDuration);
        isDisabled = false;
    }
}
