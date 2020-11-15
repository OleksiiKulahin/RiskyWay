using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public UnityEvent ColliderItemEvent;
    private KnifeController _knifeController;
    private UIManager _UIManager;
    void Start()
    {
        _knifeController = GameObject.Find("Knife").GetComponent<KnifeController>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    public void onColliderItemEvent()
    {
        if (ColliderItemEvent != null)
        {
            if (_knifeController.lifes < 3)
            {
                _knifeController.lifes++;
                _UIManager.updateLifes();
            }
        }
    }
}
