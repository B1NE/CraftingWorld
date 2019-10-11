using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : BaseObject
{
    [SerializeField] private Animator m_Animator = null;

    public override void Hit(int damage)
    {
        m_HpBar.GameObject.SetActive(true);

        m_CurHP -= damage;
        if (m_CurHP <= 0)
        {
            m_CurHP = 0;
            DestroyObject();
        }

        ShakeObject();
        m_HpBar.SetValue(HpValue);
    }

    protected override void DestroyObject()
    {
        m_HpBar.GameObject.SetActive(false);
        m_Animator.Play("Dead", 0, 0);
        m_AttackedCollider.enabled = false;

        StartCoroutine("waitDead");
    }

    private IEnumerator waitDead()
    {
        float time = m_Animator.GetCurrentAnimatorStateInfo(0).length;
        while(time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        m_AttackedCollider.enabled = true;
        base.DestroyObject();
    }
}
