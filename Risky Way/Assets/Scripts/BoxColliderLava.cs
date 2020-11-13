using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderLava : MonoBehaviour
{
    public Chunk parentChunk;
    public void OnTriggerExit(Collider collider)
    {
        parentChunk.ColliderLavaExitEvent.Invoke();
    }
    public void OnTriggerEnter(Collider collider)
    {
        parentChunk.ColliderLavaEnterEvent.Invoke();
    }
}
