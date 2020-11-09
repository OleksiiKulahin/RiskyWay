using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    public float speedTransverse=10;
    public float speedLengthwise=2;
    private float _moveInput;
    private Rigidbody _rigidbody;
    private Transform _transformKnife;
    private Transform _transformCamera;
    private Transform _transformCenter;
    private Rigidbody _rigidbodyCenter;
    private bool _pause=true;
    public GameObject _camera;
    public GameObject _knifeCenter;
    private Vector3 _defaultCameraPosition;
    private Quaternion _defaultCameraRotation;

    private int width = Screen.width;

    private Quaternion _direction = Quaternion.Euler(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transformKnife = GetComponent<Transform>();
        _camera = GameObject.Find("Main Camera");
        _transformCamera = _camera.GetComponent<Transform>();
        _knifeCenter = GameObject.Find("KnifeCenter");
        _rigidbodyCenter = _knifeCenter.GetComponent<Rigidbody>();
        _transformCenter = _knifeCenter.GetComponent<Transform>();
        _defaultCameraPosition = _transformCamera.position;
        _defaultCameraRotation = _transformCamera.rotation;
    }


    // Update is called once per frame
    [Obsolete]
    void Update()
    {
        float shift;
        _pause = false;
        //if (Input.GetKey(KeyCode.Return)) { _pause = false; }
        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Other)
        {
            if (Input.touchCount > 0)
            {
                _moveInput = Input.GetTouch(0).position.x;
            }
        }

        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows)
        {
            _moveInput = Input.mousePosition.x;
        }


        if (!_pause)
        { 
            shift = -1 * (_moveInput - (width / 2)) * (4.8f / width);

            _rigidbodyCenter.velocity = new Vector3(speedLengthwise * (float)Math.Cos(_direction.eulerAngles.y * (Math.PI / 180)),
                _rigidbodyCenter.velocity.y, speedLengthwise * (float)Math.Sin(_direction.eulerAngles.y * (Math.PI / 180)));

            if (shift > -2.4f && shift < 2.4f) 
            {
                _transformKnife.position = new Vector3(_transformCenter.position.x+shift * (float)Math.Cos((_direction.eulerAngles.y+90) * (Math.PI / 180)),
                    _transformCenter.position.y, _transformCenter.position.z+shift * (float)Math.Sin((_direction.eulerAngles.y+90) * (Math.PI / 180)));
            }
            else
            {
                if (shift <= -2.4f) {
                    _transformKnife.position = new Vector3(_transformCenter.position.x + (-2.4f) * (float)Math.Cos((_direction.eulerAngles.y + 90) * (Math.PI / 180)),
                        _transformCenter.position.y, _transformCenter.position.z + (-2.4f) * (float)Math.Sin((_direction.eulerAngles.y + 90) * (Math.PI / 180)));
                }
                else {
                    _transformKnife.position = new Vector3(_transformCenter.position.x + 2.4f * (float)Math.Cos((_direction.eulerAngles.y + 90) * (Math.PI / 180)),
                        _transformCenter.position.y, _transformCenter.position.z + 2.4f * (float)Math.Sin((_direction.eulerAngles.y + 90) * (Math.PI / 180)));
                }
            }


            if (_direction.eulerAngles.y == 0)
            {
                _transformCamera.position = new Vector3(_transformCenter.position.x + _defaultCameraPosition.x,
                    _transformCamera.position.y, _transformCenter.position.z + _defaultCameraPosition.z);
                _transformCamera.rotation = Quaternion.Euler(_defaultCameraRotation.eulerAngles.x,
                    _defaultCameraRotation.eulerAngles.y, _defaultCameraRotation.eulerAngles.z);
            }
            if (_direction.eulerAngles.y == 90)
            {
                _transformCamera.position = new Vector3(_transformCenter.position.x - _defaultCameraPosition.z,
                    _transformCamera.position.y, _transformCenter.position.z + _defaultCameraPosition.x);
                _transformCamera.rotation = Quaternion.Euler(_defaultCameraRotation.eulerAngles.x,
                    _defaultCameraRotation.eulerAngles.y+270, _defaultCameraRotation.eulerAngles.z);
            }
            if (_direction.eulerAngles.y == 180)
            {
                _transformCamera.position = new Vector3(_transformCenter.position.x - _defaultCameraPosition.x,
                    _transformCamera.position.y, _transformCenter.position.z - _defaultCameraPosition.z);
                _transformCamera.rotation = Quaternion.Euler(_defaultCameraRotation.eulerAngles.x,
                    _defaultCameraRotation.eulerAngles.y+180, _defaultCameraRotation.eulerAngles.z);
            }
            if (_direction.eulerAngles.y == 270)
            {
                _transformCamera.position = new Vector3(_transformCenter.position.x + _defaultCameraPosition.z,
                    _transformCamera.position.y, _transformCenter.position.z - _defaultCameraPosition.x);
                _transformCamera.rotation = Quaternion.Euler(_defaultCameraRotation.eulerAngles.x,
                    _defaultCameraRotation.eulerAngles.y+90, _defaultCameraRotation.eulerAngles.z);
            }





        }
        else
        {
            _rigidbody.velocity = new Vector3(0, 0, 0);
        }
    }

    public void changeDirection(int angle, Transform begin, Transform end)
    {
        //Debug.Log(_direction.eulerAngles);
        _direction = Quaternion.Euler(_direction.eulerAngles.x, _direction.eulerAngles.y+ angle, _direction.eulerAngles.z);
        //Debug.Log(_direction.eulerAngles);
        float b = (float)Math.Sqrt((Math.Pow(end.position.x - begin.position.x, 2)
            + Math.Pow(end.position.z - begin.position.z, 2)));

        float radius = (float)(b/ (2*Math.Cos(((180-angle)/2) * (Math.PI / 180))));


    }
}
