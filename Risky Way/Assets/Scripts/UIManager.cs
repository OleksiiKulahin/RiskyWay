using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool finishScreen;
    public bool loseScreen;
    public List<Image> lifesList;
    private GameObject nextButtonObj;
    private GameObject startButtonObj;
    private GameObject retryButtonObj;
    private GameObject lifesObj;
    private Image _loadingBar;
    private Image _staticBar;
    private Image _currentLevelImage;
    private Image _finishImage;
    private Image _crystalImage;
    private Button _nextButton;
    private Button _startButton;
    private Button _retryButton;
    private Text _currentLevel;
    private Text _crystalsCount;
    private ChunkPlacer _chunkPlacer;
    private Canvas _canvas;
    private RectTransform _rtCanvas;
    private KnifeController _knifeController;
    private LevelManager _levelManager;
    void Start()
    {
        finishScreen = false;
        loseScreen = false;

        nextButtonObj = GameObject.Find("NextButton");
        startButtonObj = GameObject.Find("StartButton");
        retryButtonObj = GameObject.Find("RetryButton");
        lifesObj = GameObject.Find("Lifes"); 
        
        _loadingBar = GameObject.Find("LoadingBar").GetComponent<Image>();
        _staticBar = GameObject.Find("StaticBar").GetComponent<Image>();
        _currentLevelImage = GameObject.Find("CurrentLevelImage").GetComponent<Image>();
        _finishImage = GameObject.Find("FinishImage").GetComponent<Image>();
        _crystalImage = GameObject.Find("CrystalsImage").GetComponent<Image>();
        _nextButton = nextButtonObj.GetComponent<Button>();
        _startButton = startButtonObj.GetComponent<Button>();
        _retryButton = retryButtonObj.GetComponent<Button>();

        _currentLevel = GameObject.Find("CurrentLevel").GetComponent<Text>();
        _crystalsCount = GameObject.Find("CrystalsCount").GetComponent<Text>();
        _chunkPlacer = GameObject.Find("ChunkPlacer").GetComponent<ChunkPlacer>();
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _rtCanvas = _canvas.GetComponent(typeof(RectTransform)) as RectTransform;
        _knifeController = GameObject.Find("Knife").GetComponent<KnifeController>();
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void Update()
    {
        _staticBar.transform.position = new Vector3(_rtCanvas.rect.width / 2, _rtCanvas.rect.height - _rtCanvas.rect.height / 10);
        _loadingBar.transform.position = new Vector3(_rtCanvas.rect.width / 2, _rtCanvas.rect.height - _rtCanvas.rect.height / 10);
        _currentLevelImage.transform.position = new Vector3(_rtCanvas.rect.width/8, _rtCanvas.rect.height - _rtCanvas.rect.height/10);        
        _finishImage.transform.position = new Vector3(_rtCanvas.rect.width - _rtCanvas.rect.width / 8, _rtCanvas.rect.height - _rtCanvas.rect.height / 10);
        _currentLevel.transform.position = _currentLevelImage.transform.position;
        lifesObj.transform.position = new Vector3(_staticBar.transform.position.x- _rtCanvas.rect.width/10,
            _staticBar.transform.position.y+ _rtCanvas.rect.height / 25);
        _crystalImage.transform.position = new Vector3(_staticBar.transform.position.x+ _rtCanvas.rect.width/5,
            _staticBar.transform.position.y+ _rtCanvas.rect.height / 25);

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
        _retryButton.transform.localScale
            = new Vector3((_rtCanvas.rect.width) / (230),
            (_rtCanvas.rect.width) / (230), 1);
        lifesObj.transform.localScale
            = new Vector3((_rtCanvas.rect.width) / (230),
            (_rtCanvas.rect.width) / (230), 1);
        _crystalImage.transform.localScale
            = new Vector3((_rtCanvas.rect.width) / (230),
            (_rtCanvas.rect.width) / (230), 1);

        _loadingBar.fillAmount = (float)_chunkPlacer.traversedChunks/ (float)_chunkPlacer.countChunks;
        nextButtonObj.SetActive(finishScreen);
        retryButtonObj.SetActive(loseScreen);
    }

    public void updateLifes()
    {
        int lifes = GameObject.Find("Knife").GetComponent<KnifeController>().lifes;
            
        for (int i = 0; i < 3; i++)
        {
            if (i<lifes)
            {
                lifesList[i].color = Color.red;
            }
            else
            {
                lifesList[i].color = Color.black;
            }
        }
    }

    public void updateCrystals()
    {
        _crystalsCount.text = _knifeController.crystals.ToString();
    }
    public void onFinishButton()
    {        
        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Other)
        {
            finishScreen = false;
            _knifeController.setStartSettings();
            _chunkPlacer.startSettings();
            _levelManager.levelUp();
            startButtonObj.SetActive(true);
        }
        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows)
        {
            finishScreen = false;
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

    public void onRetryButton()
    {
        loseScreen = false;
        _knifeController.setStartSettings();
        _chunkPlacer.restartSettings();
        retryButtonObj.SetActive(false);
        startButtonObj.SetActive(true);
    }
}
