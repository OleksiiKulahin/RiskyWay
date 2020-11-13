using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderRotation : MonoBehaviour
{
    public Chunk parentChunk;
    public void OnTriggerEnter(Collider collider)
    {
        BoxCollider boxCollider = this.GetComponent<BoxCollider>();
        boxCollider.enabled = false; 
        parentChunk.ColliderRotationEnterEvent.Invoke();
    }
}
