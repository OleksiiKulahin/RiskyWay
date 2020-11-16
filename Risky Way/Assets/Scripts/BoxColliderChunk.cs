using UnityEngine;

public class BoxColliderChunk : MonoBehaviour
{
    public Chunk parentChunk;
    public void OnTriggerExit(Collider collider) 
    {
        parentChunk.ColliderChunkExitEvent.Invoke();
    }
}
