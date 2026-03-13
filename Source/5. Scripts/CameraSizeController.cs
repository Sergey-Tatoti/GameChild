using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeController : MonoBehaviour
{
    [Header("Множитель для корректного отображения")]
    [SerializeField] private float _value;

    private Camera _mainCamera;
    private int _lastScreenWidth;
    private int _lastScreenHeight;
    private float _baseOrthographicSize;

    void Start()
    {
        _mainCamera = Camera.main;
        _lastScreenWidth = Screen.width;
        _lastScreenHeight = Screen.height;
        _baseOrthographicSize = _mainCamera.orthographicSize;
    }

    void Update()
    {
        if (Screen.width != _lastScreenWidth || Screen.height != _lastScreenHeight)
        {
            AdjustCameraSize();
            _lastScreenWidth = Screen.width;
            _lastScreenHeight = Screen.height;
        }
    }

    void AdjustCameraSize()
    {
        float aspectRatio = (float)Screen.width / Screen.height;

        _mainCamera.orthographicSize = _baseOrthographicSize / aspectRatio * _value;
        Mathf.Clamp(_mainCamera.orthographicSize, 0, _baseOrthographicSize);
    }
}