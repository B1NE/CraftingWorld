using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseCraft : BaseObject
{
    public override void Hit(int damage)
    {
        m_CurHP -= damage;
        if (m_CurHP <= 0)
        {
            m_CurHP = 0;
            DestroyObject();
            return;
        }

        ShakeObject();
        m_HpBar.GameObject.SetActive(true);
        m_HpBar.SetValue(HpValue);
    }

    protected override void DestroyObject()
    {
        base.DestroyObject();
        GameManager.Instance.TileManager.MakeCrafts(ObjectID, ZoneID);
    }
}
