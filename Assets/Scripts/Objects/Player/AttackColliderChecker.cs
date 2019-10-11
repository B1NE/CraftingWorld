using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderChecker : MonoObject
{
    List<BaseObject> m_AttackedObject = new List<BaseObject>();

    private void OnTriggerEnter(Collider other)
    {
        var craft = other.GetComponent<BaseObject>();
        if(craft != null)
        {
            if (m_AttackedObject.Count == 0)
            {
                Invoke("OnAttackCheck", 0.0f);
            }

            m_AttackedObject.Add(craft);
        }
    }

    private void OnAttackCheck()
    {
        m_AttackedObject.Sort((x, y) => Vector3.Distance(x.Transform.position, Transform.position).CompareTo(Vector3.Distance(y.Transform.position, Transform.position)));
        m_AttackedObject[0].Hit(20);

        m_AttackedObject.Clear();
    }
}
