using UnityEngine;
using UnityEngine.Events;

public class Chunk : MonoBehaviour
{
    public bool isFinish;
    public int roadRotation;
    public Transform begin;
    public Transform end;
    public Transform circleCenter;
    public AnimationCurve chanceFromDistance;
    private KnifeController _knifeController;
    private ChunkPlacer _chunkPlacer;
    private LevelManager _levelManager;
    private UIManager _UIManager;

    public UnityEvent ColliderChunkExitEvent;
    public UnityEvent ColliderLavaEnterEvent;
    public UnityEvent ColliderLavaExitEvent;
    public UnityEvent ColliderRotationEnterEvent;
    //public UnityEvent ColliderRotationEnterEvent;


    void Start()
    {
        _chunkPlacer = GameObject.Find("ChunkPlacer").GetComponent<ChunkPlacer>();
        _knifeController = GameObject.Find("Knife").GetComponent<KnifeController>();
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        if (ColliderChunkExitEvent!=null)
        {

        }
    }

    /*private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player"&& roadRotation!=0)
        {
            BoxCollider boxCollider= this.GetComponent<BoxCollider>();
            boxCollider.enabled = false;
            _knifeController.changeDirection(roadRotation, begin,end);
        }
    }*/

    /*private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            _chunkPlacer.setTraversedChunks();
        }
        if (collider.tag == "Player"&&tag=="Finish")
        {
            _knifeController.setPause(true);
            _UIManager._finishScreen = true;
        }
    }*/

    public void onColliderChunkExitEvent()
    {
        if (ColliderChunkExitEvent!=null)
        {
            _chunkPlacer.iterateTraversedChunks();
            if (tag == "Finish")
            {
                _knifeController.setPause(true);
                _UIManager.finishScreen = true;
            }
        }
    }


    public void onColliderLavaEnterEvent()
    {
        if (ColliderLavaEnterEvent != null)
        {
            _knifeController._knifeCenter.transform.position
                =new Vector3(_knifeController._knifeCenter.transform.position.x,
                _knifeController._knifeCenter.transform.position.y-3,
                _knifeController._knifeCenter.transform.position.z);
            _knifeController.lifes--;
        }
    }

    public void onColliderLavaExitEvent()
    {
        if (ColliderLavaExitEvent != null)
        {
            _knifeController._knifeCenter.transform.position
                =new Vector3(_knifeController._knifeCenter.transform.position.x,
                _knifeController._knifeCenter.transform.position.y+3,
                _knifeController._knifeCenter.transform.position.z);
        }
    }
    public void onColliderRotationEnterEvent()
    {
        if (ColliderRotationEnterEvent != null)
        {
            _knifeController.changeDirection(roadRotation, begin, end);
        }
    }
}
