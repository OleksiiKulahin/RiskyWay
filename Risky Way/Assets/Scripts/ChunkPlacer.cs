using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    public Transform knife;
    public Chunk[] chunkPrefabs;
    public Chunk firstChunk;
    private List<Chunk> _spawnedChunks = new List<Chunk>();
    private List<Chunk> _generatedChunks = new List<Chunk>();
    private Quaternion direction;
    public GameObject road;
    public GameObject knifeCenter;
    public int countChunks;
    private int _traversedChunks=0;

    void Start()
    {
        _spawnedChunks.Add(firstChunk);
        direction = Quaternion.Euler(0, 0, 0);
        countChunks = UnityEngine.Random.Range(60, 80);
        generateRoad();
    }

    void Update()
    {
        if (getLocalLenght(this.knifeCenter.transform.position, this._spawnedChunks[_spawnedChunks.Count - 1].transform.position) < 50
            && _traversedChunks<_generatedChunks.Count)
        {
            SpawnChunk();
        }

    }

    public void setTraversedChunks()
    {
        _traversedChunks++;
        //Debug.Log(_traversedChunks);
    }

    private void generateRoad()
    {
        for (int i = 0; i < countChunks; i++)
        {
            _generatedChunks.Add(getRandomChunk(i));
        }
        for (int i = 0; i < 3; i++)
        {
            _generatedChunks[UnityEngine.Random.Range(0, _generatedChunks.Count)] 
                = chunkPrefabs[UnityEngine.Random.Range(chunkPrefabs.Length - 2, chunkPrefabs.Length)];
        }
    }

    private Chunk getRandomChunk(int _traversedChunks)
    {
        List<float> chances = new List<float>();
        for (int i = 0; i < chunkPrefabs.Length-2; i++)
        {
            chances.Add(chunkPrefabs[i].chanceFromDistance.Evaluate(_traversedChunks));
        }
        //Debug.Log(_traversedChunks);
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
        /*if (UnityEngine.Random.Range(0, 100) > 5)
        {
            newChunk = Instantiate(chunkPrefabs[UnityEngine.Random.Range(0, chunkPrefabs.Length-2)],road.transform);
            newChunk.transform.rotation = direction;
        }
        else
        {
            if (countRotationChunks(_spawnedChunks) < 2)
            {
                int chunkID = UnityEngine.Random.Range(chunkPrefabs.Length - 2, chunkPrefabs.Length);
                newChunk = Instantiate(chunkPrefabs[chunkID], road.transform);
                newChunk.transform.rotation = direction;
                if (chunkID == chunkPrefabs.Length - 1) 
                { 
                    direction = Quaternion.Euler(0, direction.eulerAngles.y 
                        - chunkPrefabs[chunkPrefabs.Length - 1].roadRotation, 0); 
                }
                if (chunkID == chunkPrefabs.Length - 2) 
                { 
                    direction = Quaternion.Euler(0, direction.eulerAngles.y 
                        - chunkPrefabs[chunkPrefabs.Length - 2].roadRotation, 0); 
                }
            }            
        }*/
        newChunk = Instantiate(_generatedChunks[_traversedChunks], road.transform);
        newChunk.transform.rotation = direction;
        direction = Quaternion.Euler(0, direction.eulerAngles.y - _generatedChunks[_traversedChunks].roadRotation, 0);
        //Debug.Log(direction.eulerAngles);

        newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].end.position-newChunk.begin.localPosition;

        _spawnedChunks.Add(newChunk);

        if (getLocalLenght(this.knifeCenter.transform.position, this._spawnedChunks[0].transform.position) > 10)
        {
            Destroy(_spawnedChunks[0].gameObject);
            _spawnedChunks.RemoveAt(0);
        }
    }

    private int countRotationChunks(List<Chunk> spawnedChunks)
    {
        int count = 0;
        for (int i = 0; i < spawnedChunks.Count; i++)
        {
            if (spawnedChunks[i].roadRotation!=0)
            {
                count++;
            }
        }
        return count;
    }

    /*public void calculateLenght(Transform chunkEnd)
    {
        this._chunkEnd = chunkEnd;
        float tempLenght = (float)Math.Sqrt((Math.Pow((double)(chunkEnd.position.x - chunkBegin.position.x), 2)
            + Math.Pow((double)(chunkEnd.position.y - chunkBegin.position.y), 2)));
        _roadTotalLenght.Add(tempLenght);
        _roadLocalLenght.Add(tempLenght);

        Debug.Log(getTotalLenght(_roadLocalLenght) + "    " + getTotalLenght(_roadTotalLenght));

        chunkBegin = chunkEnd;
    }*/

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
        //Debug.Log("local lenght  " + lenght);

        return lenght;
    }
}
