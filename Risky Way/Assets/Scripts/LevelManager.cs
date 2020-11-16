using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int _level;
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
    public void levelUp()
    {
        _level++;
        _currentLevel.text = _level.ToString();
        //тут будет увеличение сложности      

    }
}
