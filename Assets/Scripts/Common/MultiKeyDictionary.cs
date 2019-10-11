using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 멀티 딕셔너리 클래스 구현
/// 키가 추가됨에 따라 class MultiKeydictionary 형식으로 늘려쓰면 됩니다.
/// 참조는 dic[key1][key2], dic[key1][key2][key3] ... 로 하시면 됩니다.
/// </summary>
public class MultiKeyDictionary<K1, K2, V> : Dictionary<K1, Dictionary<K2, V>>
{
    public V this[K1 key1, K2 key2]
    {
        get
        {
            if (!ContainsKey(key1) || !this[key1].ContainsKey(key2))
            {
                throw new ArgumentOutOfRangeException();
            }
            return base[key1][key2];
        }
        set
        {
            if (!ContainsKey(key1))
            {
                this[key1] = new Dictionary<K2, V>();
            }
            this[key1][key2] = value;
        }
    }

    public void Add(K1 key1, K2 key2, V value)
    {
        if (!ContainsKey(key1))
        {
            this[key1] = new Dictionary<K2, V>();
        }
        this[key1][key2] = value;
    }

    public bool ContainsKey(K1 key1, K2 key2)
    {
        return base.ContainsKey(key1) && this[key1].ContainsKey(key2);
    }

    public bool TryGetValue(K1 key1, K2 key2, out V value)
    {
        value = default(V);
        Dictionary<K2, V> k2;

        if (base.TryGetValue(key1, out k2))
        {
            if (k2.TryGetValue(key2, out value))
            {
                return true;
            }
        }

        return false;
    }
}

public class MultiKeyDictionary<K1, K2, K3, V> : Dictionary<K1, MultiKeyDictionary<K2, K3, V>>
{
    public V this[K1 key1, K2 key2, K3 key3]
    {
        get { return ContainsKey(key1) ? this[key1][key2, key3] : default(V); }
        set
        {
            if (!ContainsKey(key1))
            {
                this[key1] = new MultiKeyDictionary<K2, K3, V>();
            }
            this[key1][key2, key3] = value;
        }
    }


    public bool TryGetValue(K1 key1, K2 key2, out Dictionary<K3, V> value)
    {
        MultiKeyDictionary<K2, K3, V> k2;

        if (base.TryGetValue(key1, out k2))
        {
            if (k2.TryGetValue(key2, out value))
            {
                return true;
            }
        }

        value = null;
        return false;
    }

    public bool TryGetValue(K1 key1, K2 key2, K3 key3, out V value)
    {
        value = default(V);
        MultiKeyDictionary<K2, K3, V> k2;

        if (base.TryGetValue(key1, out k2))
        {
            Dictionary<K3, V> k3;

            if (k2.TryGetValue(key2, out k3))
            {
                if (k3.TryGetValue(key3, out value))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void Add(K1 key1, K2 key2, K3 key3, V value)
    {
        if (!ContainsKey(key1))
        {
            this[key1] = new MultiKeyDictionary<K2, K3, V>();
        }
        this[key1, key2, key3] = value;
    }

    public bool ContainsKey(K1 key1, K2 key2, K3 key3)
    {
        return base.ContainsKey(key1) && this[key1].ContainsKey(key2, key3);
    }
}

public class MultiKeyDictionary<K1, K2, K3, K4, V> : Dictionary<K1, MultiKeyDictionary<K2, K3, K4, V>>
{
    public V this[K1 key1, K2 key2, K3 key3, K4 key4]
    {
        get { return ContainsKey(key1) ? this[key1][key2, key3, key4] : default(V); }
        set
        {
            if (!ContainsKey(key1))
            {
                this[key1] = new MultiKeyDictionary<K2, K3, K4, V>();
            }
            this[key1][key2, key3, key4] = value;
        }
    }

    public bool TryGetValue(K1 key1, K2 key2, K3 key3, K4 key4, out V value)
    {
        value = default(V);
        MultiKeyDictionary<K2, K3, K4, V> k2;

        if (base.TryGetValue(key1, out k2))
        {
            MultiKeyDictionary<K3, K4, V> k3;

            if (k2.TryGetValue(key2, out k3))
            {
                Dictionary<K4, V> k4;

                if (k3.TryGetValue(key3, out k4))
                {
                    return k4.TryGetValue(key4, out value);
                }
            }
        }

        return false;
    }

    public void Add(K1 key1, K2 key2, K3 key3, K4 key4, V value)
    {
        if (!ContainsKey(key1))
        {
            this[key1] = new MultiKeyDictionary<K2, K3, K4, V>();
        }
        this[key1][key2, key3, key4] = value;
    }

    public bool Containskey(K1 key1, K2 key2, K3 key3, K4 key4)
    {
        return base.ContainsKey(key1) && this[key1].ContainsKey(key2, key3, key4);
    }
}