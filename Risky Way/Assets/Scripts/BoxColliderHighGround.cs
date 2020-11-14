using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderHighGround : MonoBehaviour
{
    public Chunk parentChunk;
    public void OnTriggerEnter(Collider collider)
    {
        if (GameObject.Find("Knife").GetComponent<KnifeController>().lifes>0)
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
        parentChunk.ColliderHighGroundEnterEvent.Invoke();
    }
}
