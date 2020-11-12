using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    public int countChunks;
    public int traversedChunks;
    public GameObject road;
    public GameObject knifeCenter;
    public Chunk[] chunkPrefabs;
    public Transform knife;
    private List<Chunk> _spawnedChunks ;
    private List<Chunk> _generatedChunks;
    private Quaternion _direction;
    private InterfaceManager _interfaceManager;

    private int totalSpawnedChunks;

    void Start()
    {
        _interfaceManager = GameObject.Find("Canvas").GetComponent<InterfaceManager>();
        startSettings();
    }

    void Update()
    {
        if (getLocalLenght(this.knifeCenter.transform.position, this._spawnedChunks[_spawnedChunks.Count - 1].transform.position) < 40
            && traversedChunks<_generatedChunks.Count&& !_interfaceManager._finishScreen&& totalSpawnedChunks<=countChunks)
        {
            SpawnChunk();
        }
    }

    public void setTraversedChunks()
    {
        traversedChunks++;
    }

    private void generateRoad()
    {
        for (int i = 0; i < countChunks; i++)
        {
            _generatedChunks.Add(getRandomChunk(i));
        }
        for (int i = 0; i < 2; i++)
        {
            _generatedChunks[UnityEngine.Random.Range(0, _generatedChunks.Count)] 
                = chunkPrefabs[UnityEngine.Random.Range(chunkPrefabs.Length - 2, chunkPrefabs.Length)];
        }

        _generatedChunks[_generatedChunks.Count-1] = chunkPrefabs[chunkPrefabs.Length - 3];
        _generatedChunks[_generatedChunks.Count-2] = chunkPrefabs[chunkPrefabs.Length - 5];
    }

    private Chunk getRandomChunk(int _traversedChunks)
    {
        List<float> chances = new List<float>();
        for (int i = 0; i < chunkPrefabs.Length-4; i++)
        {
            chances.Add(chunkPrefabs[i].chanceFromDistance.Evaluate(_traversedChunks));
        }
        float value = UnityEngine.Random.Range(0, chances.Sum());
        float sum = 0;
        for (int i = 0; i < chances.Count; i++)
        {
            sum += chances[i];
            if (value< sum)
            {
                return chunkPrefabs[i];
            }
        }
        return chunkPrefabs[chunkPrefabs.Length - 3];
    }

    private void SpawnChunk()
    {
        Chunk newChunk=null;
        totalSpawnedChunks++;
        {
            newChunk = Instantiate(_generatedChunks[totalSpawnedChunks-1], road.transform);
            newChunk.transform.rotation = _direction;
            _direction = Quaternion.Euler(0, _direction.eulerAngles.y - _generatedChunks[totalSpawnedChunks-1].roadRotation, 0);
        }
        newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].end.position-newChunk.begin.localPosition;

        _spawnedChunks.Add(newChunk);

        if (getLocalLenght(this.knifeCenter.transform.position, this._spawnedChunks[0].transform.position) > 10)
        {
            Destroy(_spawnedChunks[0].gameObject);
            _spawnedChunks.RemoveAt(0);
        }
    }

    public float getTotalLenght(List<float>roadLenght)
    {
        float lenght = 0;
        for (int i = 0; i < roadLenght.Count; i++){ lenght += roadLenght[i];}
        return lenght;
    }

    public float getLocalLenght(Vector3 movingPoint, Vector3 end)
    {
        float lenght = (float)Math.Sqrt((Math.Pow((double)(end.x - movingPoint.x), 2)
            + Math.Pow((double)(end.z - movingPoint.z), 2)));
        return lenght;
    }

    public void startSettings()
    {
        traversedChunks = 1;
        totalSpawnedChunks = 1;

        if(_generatedChunks!=null&&_generatedChunks.Count>1)
        {
            for (int i = 0; i < _spawnedChunks.Count; i++)
            {
                Destroy(_spawnedChunks[i].gameObject);
            }
        }
        _direction = Quaternion.Euler(0, 0, 0);
        _spawnedChunks = new List<Chunk>();
        _generatedChunks = new List<Chunk>(); 
        Chunk newChunk = Instantiate(chunkPrefabs[chunkPrefabs.Length - 4], road.transform);
        newChunk.transform.rotation = _direction;
        newChunk.transform.position = new Vector3(0,0,0);
        _generatedChunks.Add(newChunk);
        _spawnedChunks.Add(newChunk);
        countChunks = UnityEngine.Random.Range(30, 60);
        generateRoad();
    }
}
