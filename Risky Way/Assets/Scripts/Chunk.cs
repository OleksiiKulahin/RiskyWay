using UnityEngine;
using UnityEngine.Events;

public class Chunk : MonoBehaviour
{
    public bool isFinish;
    public int rotationAngle;
    public Transform begin;
    public Transform end;
    public Transform circleCenter;
    public AnimationCurve chanceFromDistance;

    private KnifeController _knifeController;
    private ChunkPlacer _chunkPlacer;
    private UIManager _UIManager;

    public UnityEvent ColliderChunkExitEvent;
    public UnityEvent ColliderLavaEnterEvent;
    public UnityEvent ColliderLavaExitEvent;
    public UnityEvent ColliderRotationEnterEvent;
    public UnityEvent ColliderRotationExitEvent;
    public UnityEvent ColliderHighGroundEnterEvent;

    void Start()
    {
        _chunkPlacer = GameObject.Find("ChunkPlacer").GetComponent<ChunkPlacer>();
        _knifeController = GameObject.Find("Knife").GetComponent<KnifeController>();
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
                _UIManager.setFinishScreen(true);
            }
        }
    }

    public void onColliderLavaEnterEvent()
    {
        if (ColliderLavaEnterEvent != null)
        {
            _knifeController.getKnifeCenter().transform.position
                =new Vector3(_knifeController.getKnifeCenter().transform.position.x,
                _knifeController.getKnifeCenter().transform.position.y-3,
                _knifeController.getKnifeCenter().transform.position.z);
            _knifeController.loseLife();
        }
    }

    public void onColliderLavaExitEvent()
    {
        if (ColliderLavaExitEvent != null)
        {
            _knifeController.getKnifeCenter().transform.position
                =new Vector3(_knifeController.getKnifeCenter().transform.position.x,
                _knifeController.getKnifeCenter().transform.position.y+3,
                _knifeController.getKnifeCenter().transform.position.z);
        }
    }
    public void onColliderRotationEnterEvent()
    {
        if (ColliderRotationEnterEvent != null)
        {
            _knifeController.setRotationAngle(rotationAngle);
        }
    }
    public void onColliderRotationExitEvent()
    {
        if (ColliderRotationExitEvent != null)
        {
            _knifeController.setRotationAngle(0);
        }
    }

    public void onColliderHighGroundEnterEvent()
    {
        if (ColliderHighGroundEnterEvent != null)
        {
            _knifeController.loseLife();
        }
    }
}
