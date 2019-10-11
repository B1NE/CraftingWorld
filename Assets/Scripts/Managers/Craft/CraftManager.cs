using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : BaseManager
{
    [SerializeField] private Transform m_Parent = null;
    [SerializeField] private CraftSpawner m_Spawner = null;

    // id, zoneID, objs
    private MultiKeyDictionary<int, int, List<BaseCraft>> m_Data = new MultiKeyDictionary<int, int, List<BaseCraft>>();

    // id, obj
    private Dictionary<int, BaseCraft> m_Prefabs = new Dictionary<int, BaseCraft>();

    private Dictionary<int, ObjectPool> m_ObjectPool = new Dictionary<int, ObjectPool>();

    private void OnEnable()
    {
        m_Spawner.OnSpawn += OnSpawn;
    }

    private void OnDisable()
    {
        m_Spawner.OnSpawn -= OnSpawn;
    }

    public void Init()
    {
        m_Data.Clear();
        m_Prefabs.Clear();

        LoadCraftPrefab(500000);
        LoadCraftPrefab(500100);
    }

    private void LoadCraftPrefab(int id)
    {
        var pref = Resources.Load<BaseCraft>($"Crafts/{id}");
        var pool = new ObjectPool();
        pool.Init(2, pref.GameObject, m_Parent);

        m_Prefabs.Add(id, pref);
        m_ObjectPool.Add(id, pool);
    }

    private void Spawn(float x, float y, int id, int zoneID)
    {
        if(m_ObjectPool.TryGetValue(id, out var objectPool))
        {
            var obj = objectPool.GetNext<BaseCraft>();
            obj.Transform.position = Vector3.zero.sXYZ(x, 0, y);
            obj.GameObject.SetActive(true);
            obj.Init(id, zoneID, (int)x, (int)y, 100);

            if(m_Data.TryGetValue(id, zoneID, out var baseCrafts))
            {
                baseCrafts.Add(obj);
            }
            else
            {
                m_Data.Add(id, zoneID, new List<BaseCraft>() { obj });
            }
        }
    }

    public void AddCraft(int x, int y, int id, int zoneID, float time)
    {
        m_Spawner.AddSpawnData(zoneID, id, time, x, y);
    }

    private void OnSpawn(SpawnData data)
    {
        Spawn(data.x, data.y, data.id, data.zoneID);
    }
}
