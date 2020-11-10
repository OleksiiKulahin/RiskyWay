using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    private Image _loadingBar;
    private Image _staticBar;
    private Image _currentLevel;
    private Image _nextLevel;
    private ChunkPlacer _chunkPlacer;
    private Canvas _canvas;
    private RectTransform _rtCanvas;
    void Start()
    {
        _loadingBar = GameObject.Find("LoadingBar").GetComponent<Image>();
        _staticBar = GameObject.Find("StaticBar").GetComponent<Image>();
        _currentLevel = GameObject.Find("CurrentLevel").GetComponent<Image>();
        _nextLevel = GameObject.Find("NextLevel").GetComponent<Image>();
        _chunkPlacer = GameObject.Find("ChunkPlacer").GetComponent<ChunkPlacer>();
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _rtCanvas = _canvas.GetComponent(typeof(RectTransform)) as RectTransform;
    }

    void Update()
    {
        _staticBar.transform.position = new Vector3(_rtCanvas.rect.width / 2, _rtCanvas.rect.height - 25);
        _loadingBar.transform.position = new Vector3(_rtCanvas.rect.width / 2, _rtCanvas.rect.height - 25);
        _currentLevel.transform.position = new Vector3(20, _rtCanvas.rect.height - 25);
        _nextLevel.transform.position = new Vector3(_rtCanvas.rect.width - 20, _rtCanvas.rect.height - 25);

        _staticBar.transform.localScale = new Vector3((_rtCanvas.rect.width-55) / _staticBar.rectTransform.rect.width, 1,1);
        _loadingBar.transform.localScale = new Vector3((_rtCanvas.rect.width-55) / _loadingBar.rectTransform.rect.width, 1,1);
       
        _loadingBar.fillAmount = (float)_chunkPlacer.traversedChunks/ (float)_chunkPlacer.countChunks;
    }
}
