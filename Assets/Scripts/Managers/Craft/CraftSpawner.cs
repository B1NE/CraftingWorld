using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnData
{
    public int zoneID;
    public int id;
    public float x;
    public float y;
    public float time;

    public SpawnData(int _zoneID, int _id, float _time, float _x, float _y)
    {
        zoneID = _zoneID;
        id = _id;
        time = _time;
        x = _x;
        y = _y;
    }
}

public class CraftSpawner : MonoBehaviour
{
    public Action<SpawnData> OnSpawn;
    public Dictionary<int, List<SpawnData>> m_SpawnDatas = new Dictionary<int, List<SpawnData>>();

    public void AddSpawnData(int zoneID, int craftID, float time, float x, float y)
    {
        if(m_SpawnDatas.TryGetValue(zoneID, out var spawnDatas))
        {
            spawnDatas.Add(new SpawnData(zoneID, craftID, time, x, y));
        }
        else
        {
            m_SpawnDatas.Add(zoneID, new List<SpawnData>() { new SpawnData(zoneID, craftID, time, x, y) });
        }
    }

    public void RemoveSpawnData(int zoneID)
    {
        if(m_SpawnDatas.ContainsKey(zoneID))
        {
            m_SpawnDatas.Remove(zoneID);
        }
    }

    void Update()
    {
        if(m_SpawnDatas.Count > 0)
        {
            List<int> removeDataKey = new List<int>();
            List<int> removeDataIndex = new List<int>();
            foreach (var datas in m_SpawnDatas)
            {
                for(int i = 0;i < datas.Value.Count;++i)
                {
                    var data = datas.Value[i];
                    data.time -= Time.deltaTime;

                    if (data.time <= 0)
                    {
                        OnSpawn.SafeEvent(data);
                        removeDataKey.Add(datas.Key);
                        removeDataIndex.Add(i);
                    }
                }
            }

            for(int i = 0;i < removeDataKey.Count;++i)
            {
                m_SpawnDatas[removeDataKey[i]].RemoveAt(removeDataIndex[i]);

                if(m_SpawnDatas[removeDataKey[i]].Count == 0)
                {
                    m_SpawnDatas.Remove(removeDataKey[i]);
                }
            }
        }
    }
}