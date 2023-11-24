using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Continuous_moving : MonoBehaviour
{
    public float moveSpeed = 2f;
    public GameObject HMD;
    public bool move ;
    public bool leftRight;
    // Start is called before the first frame update


    private Vector3 _initPosition;
    private Vector3 _initRotation;
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty LeftTrigger;

    public GameObject Right_hand;
    public GameObject Left_hand;
    public float Flying_threshold = 0.1f;
    private Rigidbody rb;
    private bool end;
    public GameObject crystal;
    void Start()
    {
        leftRight = true;
        end = false;
        move = false;
        _initPosition = transform.position;
        _initRotation = transform.eulerAngles;
        rb = HMD.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        crystal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(end!=true)
        {
            float trigger = pinchAnimationAction.action.ReadValue<float>();
            bool leftTrigger = LeftTrigger.action.triggered;
            if (leftTrigger)
            {
                if (move == true)
                {
                    move = false;
                    Vector3 movement = new Vector3(0, 0, 0);
                    rb.velocity = movement;
                }
                else
                {
                    move = true;
                }
            }
            if (trigger > 0.5)
            {
                HMD.transform.position = _initPosition;
                HMD.transform.eulerAngles = _initRotation;
            }
            if (move)
            {
                Vector3 movement = new Vector3(0, 0, 1);
                rb.velocity = movement;
            }else
            {
                Vector3 movement = new Vector3(0, 0, 0);
                rb.velocity = movement;
            }
            if (leftRight)
            {
                if (Right_hand.transform.position.y - Left_hand.transform.position.y > Flying_threshold) //fly to left
                {
                    HMD.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                }
                if (Left_hand.transform.position.y - Right_hand.transform.position.y > Flying_threshold)// fly to right 
                {
                    HMD.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                }
            }

        }
        
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Destination")
        {
            Debug.Log("Destination");
            move = false;
            Vector3 movement = new Vector3(0, 0, 0);
            rb.velocity = movement;
            leftRight = false;
            end = true;
            StartCoroutine(MoveToZero());
        }
    }
    private IEnumerator MoveToZero()
    {
        Vector3 finalTargetPosition = new Vector3(_initPosition.x, _initPosition.y, transform.position.z);
        while (Vector3.Distance(transform.position, finalTargetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalTargetPosition, moveSpeed/5 * Time.deltaTime);
            yield return null;
        }
        // Ensure the camera reaches the exact target position
        transform.position = finalTargetPosition;
        crystal.SetActive(true);
    }
}
