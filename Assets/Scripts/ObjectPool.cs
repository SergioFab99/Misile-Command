using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool I { get; private set; }
    private Dictionary<string, Queue<GameObject>> pools = new();

    private void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        string key = prefab.name;
        if (!pools.ContainsKey(key)) pools[key] = new Queue<GameObject>();

        if (pools[key].Count > 0)
        {
            var obj = pools[key].Dequeue();
            obj.transform.SetPositionAndRotation(pos, rot);
            obj.SetActive(true);
            return obj;
        }

        var inst = Instantiate(prefab, pos, rot);
        inst.name = prefab.name;
        return inst;
    }

    public void Despawn(GameObject obj)
    {
        obj.SetActive(false);
        string key = obj.name;
        if (!pools.ContainsKey(key)) pools[key] = new Queue<GameObject>();
        pools[key].Enqueue(obj);
    }
}
