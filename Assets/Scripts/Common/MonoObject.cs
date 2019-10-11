using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoObject : MonoBehaviour
{
    private Transform m_Transform;
    public Transform Transform
    {
        get { if (m_Transform == null) m_Transform = transform; return m_Transform; }
    }

    private GameObject m_GameObject;
    public GameObject GameObject
    {
        get { if (m_GameObject == null) m_GameObject = gameObject; return m_GameObject; }
    }

    private BoxCollider m_BoxCollider;
    public BoxCollider BoxCollider
    {
        get { if (m_BoxCollider == null) m_BoxCollider = GetComponent<BoxCollider>();  return m_BoxCollider; }
    }
}
