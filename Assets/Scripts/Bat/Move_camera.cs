using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move_camera : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Camera movement speed")]
    private float _movementSpeed = 10f;
   
    // Start is called before the first frame update

    
    [SerializeField]
    [Tooltip("Acceleration at camera movement is active")]
    private bool _enableSpeedAcceleration = true;
    private float _currentIncrease = 1;
    private float _currentIncreaseMem = 0;
    [SerializeField]
    [Tooltip("Rate which is applied during camera movement")]
    private float _speedAccelerationFactor = 1.5f;
    private Vector3 _initPosition;
    private Vector3 _initRotation;
    public GameObject camera_xr;

    public InputActionProperty pinchAnimationAction;
    public InputActionProperty buttonA;
    public InputActionProperty buttonB;
    private void Start()
    {
        _initPosition = transform.position;
        _initRotation = transform.eulerAngles;
    }

    // Update is called once per frame a
    void Update()
    {
        float trigger = pinchAnimationAction.action.ReadValue<float>();
        float aButton = buttonA.action.ReadValue<float>();
        float bButton = buttonB.action.ReadValue<float>();
        Vector3 deltaPosition = Vector3.zero;
        float currentSpeed = _movementSpeed;
        if (bButton>0.5)
        {
            deltaPosition += camera_xr.transform.up;                  
        }
            

        if (aButton>0.5)
        {
            Debug.Log("press a button");
            deltaPosition -= camera_xr.transform.up;
        }
            

        CalculateCurrentIncrease(deltaPosition != Vector3.zero);
        camera_xr.transform.position += deltaPosition * currentSpeed * _currentIncrease;
        if (trigger>0.5)
        {
            Debug.Log("press");
            camera_xr.transform.position = _initPosition;
            camera_xr.transform.eulerAngles = _initRotation;
        }
    }
    private void CalculateCurrentIncrease(bool moving)
    {
        _currentIncrease = Time.deltaTime;

        if (!_enableSpeedAcceleration || _enableSpeedAcceleration && !moving)
        {
            _currentIncreaseMem = 0;
            return;
        }

        _currentIncreaseMem += Time.deltaTime * (_speedAccelerationFactor - 1);
        _currentIncrease = Time.deltaTime + Mathf.Pow(_currentIncreaseMem, 3) * Time.deltaTime;
    }
}
