using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BasicTurret : Turret
{
    protected override void Fire(Vector3 targetPos)
    {
        if (missilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("Turret no tiene prefab o firePoint asignado.");
            return;
        }

        Vector3 dir = (targetPos - firePoint.position).normalized;

        var go = ObjectPool.I.Spawn(missilePrefab, firePoint.position, Quaternion.LookRotation(dir));

        if (go.TryGetComponent<PlayerMissile>(out var missile))
        {
            missile.Initialize(dir);
        }
    }

    private void OnEnable()
    {
        if (EventBus.I != null)
            EventBus.I.OnPlayerClick += HandlePlayerClick;
        else
            Debug.LogError("EventBus no encontrado en la escena!");
    }

    private void OnDisable()
    {
        if (EventBus.I != null)
            EventBus.I.OnPlayerClick -= HandlePlayerClick;
    }

    private void HandlePlayerClick(Vector3 pos)
    {
        Fire(pos);
    }
}
