using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Transform begin;
    public Transform end;
    public Transform circleCenter;
    public int roadRotation;

    //private ChunkPlacer _chunkPlacer;
    private KnifeController _knifeController;
    // Start is called before the first frame update
    void Start()
    {
        //_chunkPlacer = GameObject.Find("ChunkPlacer").GetComponent<ChunkPlacer>();
        _knifeController = GameObject.Find("Knife").GetComponent<KnifeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player"&& roadRotation!=0)
        {
            BoxCollider boxCollider= this.GetComponent<BoxCollider>();
            boxCollider.enabled = false;
            _knifeController.changeDirection(roadRotation, begin,end);
            //Debug.Log(roadRotation);
        }
    }
}
