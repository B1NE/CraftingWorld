using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColliderChecker : MonoObject
{
    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<BaseItem>();
        if(item != null)
        {
            item.FollowPlayer(Transform);
        }
    }


}
