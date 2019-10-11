using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIHpBar : MonoObject
{
    [SerializeField] private SpriteRenderer m_GaugeSprite = null;

    private Transform GaugeTransform = null;

    private void Awake()
    {
        GaugeTransform = m_GaugeSprite.transform;
    }

    public void SetValue(float value)
    {
        GaugeTransform.DOScaleX(value, 0.1f);
    }
}
