using System;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    public bool pause;
    private float _moveInput;
    public float speed;
    private int _width;
    public GameObject _camera;
    public GameObject _knifeCenter; 
    private Transform _transformKnife;
    private Transform _transformCenter;
    private Transform _transformCamera;
    private Rigidbody _rigidbodyCenter;
    private CapsuleCollider _colliderCenter;
    private Vector3 _defaultCameraPosition;
    private Quaternion _defaultCameraRotation;
    private Quaternion _direction;

    public void setPause(bool pause)
    {
        this.pause = pause;
    }
    void Start()
    {
        _transformKnife = GetComponent<Transform>();
        _camera = GameObject.Find("Main Camera");
        _knifeCenter = GameObject.Find("KnifeCenter"); 
        _transformCamera = _camera.GetComponent<Transform>();
        _defaultCameraPosition = _transformCamera.position;
        _defaultCameraRotation = _transformCamera.rotation;
        _rigidbodyCenter = _knifeCenter.GetComponent<Rigidbody>();
        _transformCenter = _knifeCenter.GetComponent<Transform>();
        _colliderCenter = _knifeCenter.GetComponent<CapsuleCollider>();

        setStartSettings();
    }

    void Update()
    {
        float shift;
        _width = Screen.width;
        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Other)
        {
            if (Input.touchCount > 0)
            {
                _moveInput = Input.GetTouch(0).position.x;
                pause = false;
            }
        }
        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows)
        {
            if (Input.GetKey(KeyCode.Return)) { pause = false; }
            _moveInput = Input.mousePosition.x;
        }

        if (!pause)
        {
            shift = -1 * (_moveInput - (_width / 2)) * (4.8f / _width);

            _rigidbodyCenter.velocity = new Vector3(speed * (float)Math.Cos(_direction.eulerAngles.y * (Math.PI / 180)),
                _rigidbodyCenter.velocity.y, speed * (float)Math.Sin(_direction.eulerAngles.y * (Math.PI / 180)));

            if (shift > -2.4f && shift < 2.4f) 
            {
                _transformKnife.position = new Vector3(_transformCenter.position.x+shift * (float)Math.Cos((_direction.eulerAngles.y+90) * (Math.PI / 180)),
                    _transformCenter.position.y, _transformCenter.position.z+shift * (float)Math.Sin((_direction.eulerAngles.y+90) * (Math.PI / 180)));
                _colliderCenter.center = new Vector3(shift * (float)Math.Cos((_direction.eulerAngles.y + 90) * (Math.PI / 180)), 
                    0, shift * (float)Math.Sin((_direction.eulerAngles.y + 90) * (Math.PI / 180)));
            }
            else
            {
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

            if (_direction.eulerAngles.y>-1|| _direction.eulerAngles.y<1)
            {
                _transformCamera.position = new Vector3(_transformCenter.position.x + _defaultCameraPosition.x,
                    _transformCamera.position.y, _transformCenter.position.z + _defaultCameraPosition.z);
                _transformCamera.rotation = Quaternion.Euler(_defaultCameraRotation.eulerAngles.x,
                    _defaultCameraRotation.eulerAngles.y, _defaultCameraRotation.eulerAngles.z);
                _transformKnife.rotation = Quaternion.Euler(0,0, _transformKnife.rotation.z);
            }
            if (_direction.eulerAngles.y > 89 || _direction.eulerAngles.y < 91)
            {
                _transformCamera.position = new Vector3(_transformCenter.position.x - _defaultCameraPosition.z,
                    _transformCamera.position.y, _transformCenter.position.z + _defaultCameraPosition.x);
                _transformCamera.rotation = Quaternion.Euler(_defaultCameraRotation.eulerAngles.x,
                    _defaultCameraRotation.eulerAngles.y+270, _defaultCameraRotation.eulerAngles.z);
                _transformKnife.rotation = Quaternion.Euler(0, 90, _transformKnife.rotation.z);
            }
            if (_direction.eulerAngles.y > 179 || _direction.eulerAngles.y < 181)
            {
                _transformCamera.position = new Vector3(_transformCenter.position.x - _defaultCameraPosition.x,
                    _transformCamera.position.y, _transformCenter.position.z - _defaultCameraPosition.z);
                _transformCamera.rotation = Quaternion.Euler(_defaultCameraRotation.eulerAngles.x,
                    _defaultCameraRotation.eulerAngles.y+180, _defaultCameraRotation.eulerAngles.z);
                _transformKnife.rotation = Quaternion.Euler(0, 180, _transformKnife.rotation.z);
            }
            if (_direction.eulerAngles.y > 269 || _direction.eulerAngles.y < 271)
            {
                _transformCamera.position = new Vector3(_transformCenter.position.x + _defaultCameraPosition.z,
                    _transformCamera.position.y, _transformCenter.position.z - _defaultCameraPosition.x);
                _transformCamera.rotation = Quaternion.Euler(_defaultCameraRotation.eulerAngles.x,
                    _defaultCameraRotation.eulerAngles.y+90, _defaultCameraRotation.eulerAngles.z);
                _transformKnife.rotation = Quaternion.Euler(0, 270, _transformKnife.rotation.z);
            }
        }
        else
        {
            _rigidbodyCenter.velocity = new Vector3(0, 0, 0);
        }
        if (pause)
        {
            _transformCamera.RotateAround(_transformKnife.position, Vector3.up, 30 * Time.deltaTime);
        }
    }

    public void changeDirection(int angle, Transform begin, Transform end)
    {
        _direction = Quaternion.Euler(_direction.eulerAngles.x, _direction.eulerAngles.y+ angle, _direction.eulerAngles.z);
        
        /*float b = (float)Math.Sqrt((Math.Pow(end.position.x - begin.position.x, 2)
            + Math.Pow(end.position.z - begin.position.z, 2)));
        float radius = (float)(b/ (2*Math.Cos(((180-angle)/2) * (Math.PI / 180))));*/
    }

    public void setStartSettings()
    {
        pause = true;
        speed = 20;
        _direction = Quaternion.Euler(0, 0, 0);
        _transformCenter.position = new Vector3(0, 5, 0);

        _transformCamera.position = new Vector3(_transformCenter.position.x + _defaultCameraPosition.x,
                    _transformCamera.position.y, _transformCenter.position.z + _defaultCameraPosition.z);
        _transformCamera.rotation = Quaternion.Euler(_defaultCameraRotation.eulerAngles.x,
            _defaultCameraRotation.eulerAngles.y, _defaultCameraRotation.eulerAngles.z);
        _transformKnife.rotation = Quaternion.Euler(0, 0, _transformKnife.rotation.z);
    }

}
