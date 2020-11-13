using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private int _level;
    private Text _currentLevel;

    public int getLevel()
    {
        return _level;
    }
    void Start()
    {
        _currentLevel = GameObject.Find("CurrentLevel").GetComponent<Text>();
        _level = 1;
    }

    void Update()
    {

    }
    public void levelUp()
    {
        _level++;
        _currentLevel.text = _level.ToString();
        //тут увеличение сложности      

    }
}
