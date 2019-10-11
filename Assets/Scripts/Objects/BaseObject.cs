using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseObject : MonoObject
{
    [SerializeField] protected UIHpBar m_HpBar = null;
    [SerializeField] protected Collider m_AttackedCollider = null;

    protected float m_CurHP = 100;
    protected float m_MaxHP = 100;

    public int ObjectID
    {
        get;
        set;
    } = 0;

    public int ZoneID
    {
        get;
        set;
    } = 0;

    public int X
    {
        get;
        set;
    } = 0;

    public int Y
    {
        get;
        set;
    } = 0;

    public float HpValue
    {
        get { return (float)m_CurHP / (float)m_MaxHP; }
    }

    private void OnEnable()
    {
        m_AttackedCollider.enabled = true;
        m_CurHP = m_MaxHP;
        m_HpBar.GameObject.SetActive(false);
    }

    public virtual void Init(int objectID, int zoneID, int x, int y, int hp)
    {
        ObjectID = objectID;
        ZoneID = zoneID;
        X = x;
        Y = y;

        m_CurHP = hp;
        m_MaxHP = hp;
    }

    public virtual void Hit(int damage)
    {

    }

    protected virtual void ShakeObject()
    {
        Transform.DOShakePosition(0.05f, 0.04f);
    }

    protected virtual void DestroyObject()
    {
        GameObject.SetActive(false);
    }
}
