using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move_camera : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Camera movement speed")]
    private float moveSpeed = 2f;
   
    // Start is called before the first frame update

    
    [SerializeField]
    [Tooltip("Rate which is applied during camera movement")]
    private Vector3 _initPosition;
    private Vector3 _initRotation;
    public GameObject HMD;

    public InputActionProperty pinchAnimationAction;
    public InputActionProperty LeftTrigger;

    public GameObject Right_hand;
    public GameObject Left_hand;
    public float Flying_threshold=0.1f;
    private void Start()
    {
        _initPosition = transform.position;
        _initRotation = transform.eulerAngles;

    }

    // Update is called once per frame a
    void Update()
    {
        float trigger = pinchAnimationAction.action.ReadValue<float>();
        Vector3 deltaPosition = Vector3.zero;
       
        if (trigger>0.5)
        {
            Debug.Log("press");
            HMD.transform.position = _initPosition;
            HMD.transform.eulerAngles = _initRotation;
        }
        
        if (Right_hand.transform.position.y - Left_hand.transform.position.y > Flying_threshold) //fly to left
        {
            Debug.Log("Left");
            HMD.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if(Left_hand.transform.position.y - Right_hand.transform.position.y > Flying_threshold)// fly to right 
        {
            Debug.Log("Right");
            HMD.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

}
