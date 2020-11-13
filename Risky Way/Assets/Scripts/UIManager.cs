using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool _finishScreen;
    private GameObject nextButtonObj;
    private GameObject startButtonObj;
    private Image _loadingBar;
    private Image _staticBar;
    private Image _currentLevelImage;
    private Image _finishImage;
    private Button _nextButton;
    private Button _startButton;
    private Text _currentLevel;
    private ChunkPlacer _chunkPlacer;
    private Canvas _canvas;
    private RectTransform _rtCanvas;
    private KnifeController _knifeController;
    private LevelManager _levelManager;
    void Start()
    {
        _finishScreen = false;
        _loadingBar = GameObject.Find("LoadingBar").GetComponent<Image>();
        _staticBar = GameObject.Find("StaticBar").GetComponent<Image>();
        _currentLevelImage = GameObject.Find("CurrentLevelImage").GetComponent<Image>();
        _finishImage = GameObject.Find("FinishImage").GetComponent<Image>();
        _nextButton = GameObject.Find("NextButton").GetComponent<Button>();
        _startButton = GameObject.Find("StartButton").GetComponent<Button>();

        _currentLevel = GameObject.Find("CurrentLevel").GetComponent<Text>();
        _chunkPlacer = GameObject.Find("ChunkPlacer").GetComponent<ChunkPlacer>();
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _rtCanvas = _canvas.GetComponent(typeof(RectTransform)) as RectTransform;
        _knifeController = GameObject.Find("Knife").GetComponent<KnifeController>();
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        nextButtonObj = GameObject.Find("NextButton");
        startButtonObj = GameObject.Find("StartButton");
    }

    void Update()
    {
        _staticBar.transform.position = new Vector3(_rtCanvas.rect.width / 2, _rtCanvas.rect.height - _rtCanvas.rect.height / 10);
        _loadingBar.transform.position = new Vector3(_rtCanvas.rect.width / 2, _rtCanvas.rect.height - _rtCanvas.rect.height / 10);
        _currentLevelImage.transform.position = new Vector3(_rtCanvas.rect.width/8, _rtCanvas.rect.height - _rtCanvas.rect.height/10);        
        _finishImage.transform.position = new Vector3(_rtCanvas.rect.width - _rtCanvas.rect.width / 8, _rtCanvas.rect.height - _rtCanvas.rect.height / 10);
        _currentLevel.transform.position = _currentLevelImage.transform.position;

        _staticBar.transform.localScale = new Vector3((_rtCanvas.rect.width) / (_staticBar.rectTransform.rect.width * 1.6f),
            (_rtCanvas.rect.height) / (_staticBar.rectTransform.rect.height * 16), 1);
        _loadingBar.transform.localScale = new Vector3((_rtCanvas.rect.width) / (_loadingBar.rectTransform.rect.width * 1.6f),
            (_rtCanvas.rect.height) / (_loadingBar.rectTransform.rect.height * 16), 1);
        _currentLevelImage.transform.localScale
            = new Vector3((_rtCanvas.rect.width) / (_currentLevelImage.rectTransform.rect.width * 12),
            (_rtCanvas.rect.width) / (_currentLevelImage.rectTransform.rect.width * 12), 1);
        _finishImage.transform.localScale
            = new Vector3((_rtCanvas.rect.width) / (_finishImage.rectTransform.rect.width * 12),
            (_rtCanvas.rect.width) / (_finishImage.rectTransform.rect.width * 12), 1);
        _currentLevel.transform.localScale
            = new Vector3((_rtCanvas.rect.width) / (_currentLevel.rectTransform.rect.width * 16),
            (_rtCanvas.rect.width) / (_currentLevel.rectTransform.rect.width * 16), 1);
        _nextButton.transform.localScale
            = new Vector3((_rtCanvas.rect.width) / (230),
            (_rtCanvas.rect.width) / (230), 1);
        _startButton.transform.localScale
            = new Vector3((_rtCanvas.rect.width) / (230),
            (_rtCanvas.rect.width) / (230), 1);

        _loadingBar.fillAmount = (float)_chunkPlacer.traversedChunks/ (float)_chunkPlacer.countChunks;
        nextButtonObj.SetActive(_finishScreen);
}
    public void onFinishButton()
    {        
        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Other)
        {
            _finishScreen = false;
            _knifeController.setStartSettings();
            _chunkPlacer.startSettings();
            _levelManager.levelUp();
            startButtonObj.SetActive(true);
        }
        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows)
        {
            _finishScreen = false;
            _chunkPlacer.startSettings();
            _knifeController.setStartSettings();
            _levelManager.levelUp();
            startButtonObj.SetActive(true);
        }
    }
    public void onStartButton()
    {
        startButtonObj.SetActive(false);
        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Other)
        {
            _knifeController.pause = false;
        }
        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows)
        {
            _knifeController.pause = false;
        }
    }
}
