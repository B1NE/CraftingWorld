using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : BaseManager
{
    [SerializeField] private Transform m_Parent = null;
    [SerializeField] private MonsterSpawner m_Spawner = null;

    // id, zoneID, objs
    private MultiKeyDictionary<int, int, List<BaseMonster>> m_Data = new MultiKeyDictionary<int, int, List<BaseMonster>>();

    // id, obj
    private Dictionary<int, BaseMonster> m_Prefabs = new Dictionary<int, BaseMonster>();

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

        LoadCraftPrefab(900100);
    }

    private void LoadCraftPrefab(int id)
    {
        var pref = Resources.Load<BaseMonster>($"Monsters/{id}");
        var pool = new ObjectPool();
        pool.Init(1, pref.GameObject, m_Parent);

        m_Prefabs.Add(id, pref);
        m_ObjectPool.Add(id, pool);
    }

    private void SpawnCraft(float x, float y, int id, int zoneID)
    {
        if (m_ObjectPool.TryGetValue(id, out var objectPool))
        {
            var obj = objectPool.GetNext<BaseMonster>();
            obj.Transform.position = Vector3.zero.sXYZ(x, 0, y);
            obj.GameObject.SetActive(true);
            obj.Init(id, zoneID, (int)x, (int)y, 100);

            if (m_Data.TryGetValue(id, zoneID, out var baseMonsters))
            {
                baseMonsters.Add(obj);
            }
            else
            {
                m_Data.Add(id, zoneID, new List<BaseMonster>() { obj });
            }
        }
    }

    public void AddCraft(int x, int y, int id, int zoneID, float time)
    {
        m_Spawner.AddSpawnData(zoneID, id, time, x, y);
    }

    private void OnSpawn(SpawnData data)
    {
        SpawnCraft(data.x, data.y, data.id, data.zoneID);
    }
}
