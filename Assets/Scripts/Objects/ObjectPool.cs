using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private List<GameObject> m_PoolObjects = new List<GameObject>();
    private GameObject m_Pref = null;
    private Transform m_Parent = null;

    public void Init(int count, GameObject pref, Transform parent)
    {
        this.AllDestroy();

        m_Pref = pref;
        m_Parent = parent;
        for (int i = 0; i < count; ++i)
        {
            MakeObject();
        }
    }

    public void AllClear()
    {
        foreach(var obj in m_PoolObjects)
        {
            obj.gameObject.SetActive(false);
        }
    }

    public void AllDestroy()
    {
        foreach (var obj in m_PoolObjects)
        {
            GameObject.DestroyImmediate(obj);
        }

        m_PoolObjects.Clear();
    }

    public T GetNext<T>() where T : Object
    {
        var targetObject = m_PoolObjects.Find(x => x.activeSelf == false);
        if(targetObject == null)
        {
            targetObject = MakeObject();
        }

        if(targetObject is T)
        {
            return targetObject as T;
        }

        return targetObject.GetComponent<T>();
    }

    private GameObject MakeObject()
    {
        var obj = Object.Instantiate(m_Pref, m_Parent);
        obj.gameObject.SetActive(false);
        m_PoolObjects.Add(obj);

        return obj;
    }
}