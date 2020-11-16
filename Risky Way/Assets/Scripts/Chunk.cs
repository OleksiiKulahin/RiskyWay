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
    public UnityEvent ColliderHighGroundEnterEvent;


    void Start()
    {
        _chunkPlacer = GameObject.Find("ChunkPlacer").GetComponent<ChunkPlacer>();
        _knifeController = GameObject.Find("Knife").GetComponent<KnifeController>();
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

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
            if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Other){Handheld.Vibrate();}
            _knifeController.lifes--;
            _UIManager.updateLifes();
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

    public void onColliderHighGroundEnterEvent()
    {
        if (ColliderHighGroundEnterEvent != null)
        {
            if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Other) { Handheld.Vibrate(); }
            _knifeController.lifes--;
            _UIManager.updateLifes();
        }
    }
}
