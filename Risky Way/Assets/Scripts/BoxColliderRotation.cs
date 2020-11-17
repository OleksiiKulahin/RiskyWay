using UnityEngine;

public class BoxColliderRotation : MonoBehaviour
{
    public Chunk parentChunk;
    public void OnTriggerEnter(Collider collider)
    {
        parentChunk.ColliderRotationEnterEvent.Invoke();
    }
    public void OnTriggerExit(Collider collider)
    {
        parentChunk.ColliderRotationExitEvent.Invoke();
    }
}
