using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public UnityEvent ColliderItemEvent;
    private KnifeController _knifeController;
    void Start()
    {
        _knifeController = GameObject.Find("Knife").GetComponent<KnifeController>();
    }

    public void onColliderItemEvent()
    {
        if (ColliderItemEvent != null)
        {
            if (name.Contains("Heart"))
            {
                if (_knifeController.lifes < 3)
                {
                    _knifeController.addLife();
                }
            }
            if (name.Contains("Crystal"))
            {
                _knifeController.addCrystal();
            }
            if (name.Contains("Bomb"))
            {
                transform.Find("Explosion").gameObject.SetActive(true);
                transform.Find("Explosion").gameObject.GetComponent<ParticleSystem>().Play();
                _knifeController.stabbingTime = 1f;
                _knifeController.loseLife();
            }
        }
    }
}
