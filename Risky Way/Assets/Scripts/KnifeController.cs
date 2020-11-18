using System;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    public bool pause;
    public int lifes;
    public int crystals; 

    private int _rotationAngle;
    private float _moveInput;
    private float _speed;
    private int _width;
    private float _distanceToCamera;
    private float _invulnerabilityTime;
    private float _stabbingTime;
    private float _koeficientAngleCenter = 1.06000444f;

    public Material defaultMaterial;
    public Material invulnerableMaterial;

    private GameObject _knifeCenter;
    private GameObject _camera;
    private Transform _transformKnife;
    private Transform _transformCenter;
    private Transform _transformCamera;
    private Rigidbody _rigidbodyCenter;
    private CapsuleCollider _colliderCenter;
    private Vector3 _defaultCameraPosition;
    private Quaternion _defaultCameraRotation;
    private Quaternion _direction;
    private UIManager _UIManager;

    public GameObject getKnifeCenter()
    {
        return _knifeCenter;
    }

    public void setPause(bool pause)
    {
        this.pause = pause;
    }

    public void setStabbingTime(float stabbingTime)
    {
        this._stabbingTime = stabbingTime;
    }

    public void setRotationAngle(int rotationAngle)
    {
        this._rotationAngle = rotationAngle;
    }

    void Start()
    {
        _camera = GameObject.Find("Main Camera");
        _knifeCenter = GameObject.Find("KnifeCenter"); 
        _transformKnife = GetComponent<Transform>();
        _transformCamera = _camera.GetComponent<Transform>();
        _defaultCameraPosition = _transformCamera.position;
        _defaultCameraRotation = _transformCamera.rotation;
        _rigidbodyCenter = _knifeCenter.GetComponent<Rigidbody>();
        _transformCenter = _knifeCenter.GetComponent<Transform>();
        _colliderCenter = _knifeCenter.GetComponent<CapsuleCollider>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        crystals = 0;
        setStartSettings();
    }

    private void FixedUpdate()
    {
        if (_rotationAngle != 0)
        {
            _direction = Quaternion.Euler(_direction.eulerAngles.x,
                _direction.eulerAngles.y + _rotationAngle / _koeficientAngleCenter * Time.deltaTime, _direction.eulerAngles.z);
        }
    }

    void Update()
    {
        float shift;
        _width = Screen.width;

        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Other){
            if (Input.touchCount > 0){
                _moveInput = Input.GetTouch(0).position.x;
            }
        }
        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows
            || SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX)
        {
            _moveInput = Input.mousePosition.x;
        }


        if (!pause){
            shift = -1 * (_moveInput - (_width / 2)) * (4.8f / _width);

            _rigidbodyCenter.velocity = new Vector3(_speed * (float)Math.Cos(_direction.eulerAngles.y * (Math.PI / 180)),
                    _rigidbodyCenter.velocity.y, _speed * (float)Math.Sin(_direction.eulerAngles.y * (Math.PI / 180)));

            _transformKnife.rotation = Quaternion.Euler(-90, -_direction.eulerAngles.y, _transformKnife.rotation.z);

            _transformCamera.rotation = Quaternion.Euler(_defaultCameraRotation.eulerAngles.x,
                    _defaultCameraRotation.eulerAngles.y - _direction.eulerAngles.y, _defaultCameraRotation.eulerAngles.z);

            _transformCamera.position = new Vector3(_transformCenter.position.x - _distanceToCamera * (float)Math.Sin((-_direction.eulerAngles.y + 75f) * (Math.PI / 180)),
                    _transformCamera.position.y, _transformCenter.position.z - _distanceToCamera * (float)Math.Cos((-_direction.eulerAngles.y + 75f) * (Math.PI / 180)));

            if (shift > -2.4f && shift < 2.4f) {
                _transformKnife.position = new Vector3(_transformCenter.position.x+shift * (float)Math.Cos((_direction.eulerAngles.y+90) * (Math.PI / 180)),
                    _transformCenter.position.y, _transformCenter.position.z+shift * (float)Math.Sin((_direction.eulerAngles.y+90) * (Math.PI / 180)));
                _colliderCenter.center = new Vector3(shift * (float)Math.Cos((_direction.eulerAngles.y + 90) * (Math.PI / 180)), 
                    0, shift * (float)Math.Sin((_direction.eulerAngles.y + 90) * (Math.PI / 180)));
            }
            else{
                if (shift <= -2.4f) {
                    _transformKnife.position = new Vector3(_transformCenter.position.x + (-2.4f) * (float)Math.Cos((_direction.eulerAngles.y + 90) * (Math.PI / 180)),
                        _transformCenter.position.y, _transformCenter.position.z + (-2.4f) * (float)Math.Sin((_direction.eulerAngles.y + 90) * (Math.PI / 180)));
                    _colliderCenter.center = new Vector3(-2.4f * (float)Math.Cos((_direction.eulerAngles.y + 90) * (Math.PI / 180)),
                        0, -2.4f * (float)Math.Sin((_direction.eulerAngles.y + 90) * (Math.PI / 180)));
                }
                else {
                    _transformKnife.position = new Vector3(_transformCenter.position.x + 2.4f * (float)Math.Cos((_direction.eulerAngles.y + 90) * (Math.PI / 180)),
                        _transformCenter.position.y, _transformCenter.position.z + 2.4f * (float)Math.Sin((_direction.eulerAngles.y + 90) * (Math.PI / 180)));
                    _colliderCenter.center = new Vector3(2.4f * (float)Math.Cos((_direction.eulerAngles.y + 90) * (Math.PI / 180)),
                            0, 2.4f * (float)Math.Sin((_direction.eulerAngles.y + 90) * (Math.PI / 180)));
                }
            }                        
            if (_rotationAngle==0){
                stabilizeDirection();
            }  
        }
        else{
            _rigidbodyCenter.velocity = new Vector3(0, 0, 0);
            _transformCamera.RotateAround(_transformKnife.position, Vector3.up, 30 * Time.deltaTime);
        }

        if (lifes<1){
            _UIManager.setLoseScreen(true);
            pause = true;
        }
        if (_invulnerabilityTime > 0){
            knifeFlickering();
        }
        else{
            GetComponent<Renderer>().material = defaultMaterial;
        }
        if (_stabbingTime>0){
            knifeStabbing();
        }
    }

    public void setStartSettings()
    {
        pause = true;
        _speed = 12;
        lifes = 3;
        _direction = Quaternion.Euler(0, 0, 0);
        _transformCenter.position = new Vector3(2.5f, 5.5f, 0);
        _transformKnife.position = _transformCenter.position;
        _transformCamera.position = new Vector3(_transformCenter.position.x + _defaultCameraPosition.x,
                    _transformCamera.position.y, _transformCenter.position.z + _defaultCameraPosition.z);
        _transformCamera.rotation = Quaternion.Euler(_defaultCameraRotation.eulerAngles.x,
            _defaultCameraRotation.eulerAngles.y, _defaultCameraRotation.eulerAngles.z);
        _transformKnife.rotation = Quaternion.Euler(-90, 0, _transformKnife.rotation.z);
        _UIManager.updateLifes();

        _distanceToCamera = (float)Math.Sqrt(Math.Pow((_transformCamera.position.x - _transformCenter.position.x), 2)
                    + Math.Pow((_transformCamera.position.z - _transformCenter.position.z), 2));
    }

    public void knifeStabbing()
    {
        _stabbingTime -= 8*Time.deltaTime;
        _transformKnife.position = new Vector3(_transformKnife.position.x,
            _transformKnife.position.y- _stabbingTime, _transformKnife.position.z);
    }

    public void knifeFlickering()
    {
        _invulnerabilityTime -= Time.deltaTime;
        GetComponent<Renderer>().material = invulnerableMaterial;
        Color tempColor = invulnerableMaterial.color;
        tempColor.a = (float)Math.Sin(30 * _invulnerabilityTime);
        invulnerableMaterial.color = tempColor;
    }

    public void addCrystal()
    {
        _stabbingTime = 1f;
        crystals++;
        _UIManager.updateCrystals();
    }

    public void addLife()
    {
        _stabbingTime = 1f;
        lifes++;
        _UIManager.updateLifes();
    }

    public void loseLife()
    {
        if (_invulnerabilityTime<=0){
            lifes--;
            if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Other) { Handheld.Vibrate(); }
            _invulnerabilityTime = 1f;
            _UIManager.updateLifes();
        }
    }

    private void stabilizeDirection()
    {
        if (_direction.eulerAngles.y > -20 && _direction.eulerAngles.y < 20)
        {
            _direction = Quaternion.Euler(_direction.eulerAngles.x, 0, _direction.eulerAngles.z);
        }
        if (_direction.eulerAngles.y > 70 && _direction.eulerAngles.y < 110)
        {
            _direction = Quaternion.Euler(_direction.eulerAngles.x, 90, _direction.eulerAngles.z);
        }
        if (_direction.eulerAngles.y > 160 && _direction.eulerAngles.y < 200)
        {
            _direction = Quaternion.Euler(_direction.eulerAngles.x, 180, _direction.eulerAngles.z);
        }
        if (_direction.eulerAngles.y > 250 && _direction.eulerAngles.y < 290)
        {
            _direction = Quaternion.Euler(_direction.eulerAngles.x, 270, _direction.eulerAngles.z);
        }
    }    
}
