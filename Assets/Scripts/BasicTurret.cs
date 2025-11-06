using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BasicTurret : Turret
{
    [SerializeField] private int maxMissiles = 10;
    private int currentMissiles;

    private void Start()
    {
        currentMissiles = maxMissiles;
    }

    protected override void Fire(Vector3 targetPos)
    {
        if (missilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("Turret no tiene prefab o firePoint asignado.");
            return;
        }

        if (currentMissiles <= 0)
        {
            Debug.Log("Sin misiles disponibles en " + gameObject.name);
            return;
        }

        Vector3 dir = (targetPos - firePoint.position).normalized;
        var go = ObjectPool.I.Spawn(missilePrefab, firePoint.position, Quaternion.LookRotation(dir));

        if (go.TryGetComponent<PlayerMissile>(out var missile))
            missile.Initialize(dir);

        currentMissiles--;
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

    public void Reload(int amount)
    {
        currentMissiles = Mathf.Min(currentMissiles + amount, maxMissiles);
    }
    public int GetRemainingMissiles() => currentMissiles;
}
