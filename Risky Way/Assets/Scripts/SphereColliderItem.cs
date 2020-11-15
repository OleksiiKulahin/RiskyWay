using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereColliderItem : MonoBehaviour
{
    public Item parentItem;
    public void OnTriggerEnter(Collider collider)
    {
        parentItem.ColliderItemEvent.Invoke();
        parentItem.GetComponent<MeshRenderer>().enabled = false;
    }
}
