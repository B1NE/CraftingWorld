using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftLava : BaseCraft
{
    protected override void DestroyObject()
    {
        base.DestroyObject();
        ItemPoolManager.Instance.SpanwItem(100100, 5, Transform.position);
    }
}
