using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemPoolManager : MonoBehaviour
{
    static private ItemPoolManager m_instance = null;
    static public ItemPoolManager Instance
    {
        get { return m_instance; }
    }

    [SerializeField] private Transform m_ItemParent = null;

    private Dictionary<int, ObjectPool> m_DictItemPool = new Dictionary<int, ObjectPool>();

    private void Awake()
    {
        m_instance = this;
        this.Init();
    }

    public void Init()
    {
        LoadItemPrefabs(100000);
        LoadItemPrefabs(100100);
    }

    private void LoadItemPrefabs(int id)
    {
        if (m_DictItemPool.ContainsKey(id)) return;

        var prefab = Resources.Load<GameObject>($"Items/{id}");
        var pool = new ObjectPool();
        pool.Init(10, prefab, m_ItemParent);
        m_DictItemPool.Add(id, pool);
    }

    public void SpanwItem(int id, int count, Vector3 originPos)
    {
        if(m_DictItemPool.TryGetValue(id, out var objectPool))
        {
            for(int i = 0;i < count;++i)
            {
                var item = objectPool.GetNext<BaseItem>();
                item.Transform.parent =  m_ItemParent;
                item.GameObject.SetActive(true);
                item.Transform.position = originPos.pXYZ(Random.Range(0.1f, 1.0f), Random.Range(0.6f, 1.0f), Random.Range(0.1f, 1.0f));
                item.Active();
            }
        }
    }
}