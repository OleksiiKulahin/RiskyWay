using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderLava : MonoBehaviour
{
    public Chunk parentChunk;
    private bool deadInLava;
    public void OnTriggerExit(Collider collider)
    {
        if (!deadInLava)
        {
            parentChunk.ColliderLavaExitEvent.Invoke();
        }
    }
    public void OnTriggerEnter(Collider collider)
    {
        parentChunk.ColliderLavaEnterEvent.Invoke();
        if (GameObject.Find("Knife").GetComponent<KnifeController>().lifes < 1)
        {
            deadInLava = true;
        }        
    }
}
