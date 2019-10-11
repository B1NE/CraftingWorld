using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseItem : MonoObject
{
    Transform m_PlayerTransform = null;

    private void OnDisable()
    {
        m_PlayerTransform = null;
    }

    public void Active()
    {
        BoxCollider.enabled = false;
        Transform.DOMoveY(0.0f, Random.Range(0.5f, 0.7f), false).SetEase(Ease.OutBounce).OnComplete(() => { BoxCollider.enabled = true; });
    }

    public void FollowPlayer(Transform player)
    {
        m_PlayerTransform = player;
        StartCoroutine(followPlayer());
    }

    IEnumerator followPlayer()
    {
        float timer = 0;
        Vector3 startPosition = Transform.position;
        while(timer <= 1.0f)
        {
            timer += Time.deltaTime * 6.0f;
            Transform.position = Vector3.Lerp(startPosition, m_PlayerTransform.position, timer);
            yield return null;
        }

        GameObject.SetActive(false);
    }
}
