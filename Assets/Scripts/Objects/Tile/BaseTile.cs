using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoObject
{
    [SerializeField] private MeshRenderer m_Renderer = null;

    public void SetColor(Color color)
    {
        m_Renderer.material.SetColor("_Color", color);
    }
}
