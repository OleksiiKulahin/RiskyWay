using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderRotation : MonoBehaviour
{
    public Chunk parentChunk;
    public void OnTriggerEnter(Collider collider)
    {
        GetComponent<BoxCollider>().enabled = false; 
        parentChunk.ColliderRotationEnterEvent.Invoke();
    }
}
