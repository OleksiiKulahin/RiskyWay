using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxColliderChunk : MonoBehaviour
{
    public Chunk parentChunk;
    public void OnTriggerExit(Collider collider) 
    {
        parentChunk.ColliderChunkExitEvent.Invoke();
    }
}
