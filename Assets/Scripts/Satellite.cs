using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Satellite : BaseEntity
{
    public float moveSpeed = 5f;
    public float endX = -20f;
    private bool moving = false;
    private int health = 3;

    public override void Initialize()
    {
        moving = true;
    }

    private void Update()
    {
        if (!moving) return;

        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);

        if (transform.position.x <= endX)
        {
            ObjectPool.I.Despawn(gameObject);
        }
    }

    public void TakeHit()
    {
        health--;
        if (health <= 0)
        {
            OnDestroyed();
        }
    }

    public override void OnDestroyed()
    {
        ObjectPool.I.Despawn(gameObject);
    }
}
