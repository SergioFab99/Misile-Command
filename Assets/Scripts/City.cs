using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class City : BaseEntity, IDamageable
{
    [SerializeField] private int health = 1;

    public bool IsAlive { get; private set; } = true;

    public override void Initialize()
    {
        GameManager.I?.RegisterCity(this);
        IsAlive = gameObject.activeSelf;
    }

    public void TakeDamage(int amount)
    {
        if (!IsAlive) return;
        health -= amount;
        if (health <= 0) OnDestroyed();
    }

    public override void OnDestroyed()
    {
        if (!IsAlive) return;
        IsAlive = false;
        gameObject.SetActive(false);
        EventBus.I?.Message("Ciudad destruida");
        GameManager.I?.NotifyCityDestroyed(this);
    }

    public void Restore(int hp = 1)
    {
        health = hp;
        IsAlive = true;
        gameObject.SetActive(true);
    }
}
