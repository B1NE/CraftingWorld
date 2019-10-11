using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTree : BaseCraft
{
    protected override void DestroyObject()
    {
        base.DestroyObject();
        ItemPoolManager.Instance.SpanwItem(100000, 5, Transform.position);
    }
}
