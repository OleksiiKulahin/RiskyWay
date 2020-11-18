using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    public int spawnDistance;
    public Chunk[] chunkPrefabs;

    private int _countChunks;
    private int _traversedChunks;
    private int _totalSpawnedChunks;
    private GameObject _road;
    private GameObject _knifeCenter;
    private List<Chunk> _spawnedChunks;
    private List<Chunk> _generatedChunks;
    private Quaternion _direction;
    private UIManager _UIManager;

    public int getTraversedChunks()
    {
        return _traversedChunks;
    }
    
    public int getCountChunks()
    {
        return _countChunks;
    }

    void Start()
    {
        _knifeCenter = GameObject.Find("KnifeCenter");
        _road = GameObject.Find("Road");
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        startSettings();
    }

    void Update()
    {
        if (getLocalLenght(this._knifeCenter.transform.position, this._spawnedChunks[_spawnedChunks.Count - 1].transform.position) < spawnDistance
            && _traversedChunks<_generatedChunks.Count&& !_UIManager.getFinishScreen() && _totalSpawnedChunks<=_countChunks)
        {
            SpawnChunk();
        }
    }

    public void startSettings()
    {
        _traversedChunks = 1;
        _totalSpawnedChunks = 1;

        if (_generatedChunks != null && _generatedChunks.Count > 1)
        {
            for (int i = 0; i < _spawnedChunks.Count; i++)
            {
                Destroy(_spawnedChunks[i].gameObject);
            }
        }
        _direction = Quaternion.Euler(0, 0, 0);
        _spawnedChunks = new List<Chunk>();
        _generatedChunks = new List<Chunk>();
        Chunk newChunk = Instantiate(chunkPrefabs[chunkPrefabs.Length - 4], _road.transform);
        newChunk.transform.rotation = _direction;
        newChunk.transform.position = new Vector3(0, 0, 0);
        _generatedChunks.Add(newChunk);
        _spawnedChunks.Add(newChunk);
        _countChunks = UnityEngine.Random.Range(60, 100);
        generateRoad();
    }

    public void iterateTraversedChunks()
    {
        _traversedChunks++;
    }

    private void generateRoad()
    {
        for (int i = 0; i < _countChunks; i++)
        {
            _generatedChunks.Add(getRandomChunk(i));
        }
        for (int i = 0; i < 2; i++)
        {
            int id = UnityEngine.Random.Range(chunkPrefabs.Length - 2, chunkPrefabs.Length - 1);
            int place = UnityEngine.Random.Range(5, _generatedChunks.Count-5);
            if (_generatedChunks[place - 1]!= chunkPrefabs[chunkPrefabs.Length - 2]
                && _generatedChunks[place - 1] != chunkPrefabs[chunkPrefabs.Length - 1]
                && _generatedChunks[place + 1] != chunkPrefabs[chunkPrefabs.Length - 2]
                && _generatedChunks[place + 1] != chunkPrefabs[chunkPrefabs.Length - 1]){
                _generatedChunks[place]
                = chunkPrefabs[id];
            }            
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
        _totalSpawnedChunks++;
        {
            newChunk = Instantiate(_generatedChunks[_totalSpawnedChunks-1], _road.transform);
            newChunk.transform.rotation = _direction;
            _direction = Quaternion.Euler(0, _direction.eulerAngles.y - _generatedChunks[_totalSpawnedChunks-1].rotationAngle, 0);
        }
        newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].end.position-newChunk.begin.localPosition;

        _spawnedChunks.Add(newChunk);

        if (getLocalLenght(this._knifeCenter.transform.position, this._spawnedChunks[0].transform.position) > 20)
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
      
    public void restartSettings()
    {
        _traversedChunks = 1;
        _totalSpawnedChunks = 1;

        if (_spawnedChunks != null && _spawnedChunks.Count > 1)
        {
            for (int i = 0; i < _spawnedChunks.Count; i++)
            {
                Destroy(_spawnedChunks[i].gameObject);
            }
        }
        _direction = Quaternion.Euler(0, 0, 0);
        _spawnedChunks = new List<Chunk>();
        Chunk newChunk = Instantiate(chunkPrefabs[chunkPrefabs.Length - 4], _road.transform);
        newChunk.transform.rotation = _direction;
        newChunk.transform.position = new Vector3(0, 0, 0);
        _spawnedChunks.Add(newChunk);
    }
}
