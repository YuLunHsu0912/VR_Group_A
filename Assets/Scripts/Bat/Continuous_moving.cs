using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class Continuous_moving : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 2f;
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
    public float accelerationDuration = 2f; // Set the duration of acceleration
    public float decelerationDuration = 2f; // Set the duration of deceleration
    private float currentSpeed = 0f;
    private float timer = 0f;

    public GameObject TargetPosition;
    public bool endtest;
    public GameObject Endtest;
    public float lerpTime=3f;
    public GameObject SoundSource;

    public bool stage_sound;
    public bool makeNoise;
    public bool stage_left;
    public bool stage_right;
    bool needleft;
    public TextMeshProUGUI  tutorial_text;
    public int count;
    public GameObject littleman;
    public ChangePose script;
    public int temp = 0;
    public GameObject flytoright;
    void Start()
    {
        if (endtest)
        {
            transform.position = Endtest.transform.position;
        }
        leftRight = false;
        end = false;
        move = false;
        _initPosition = transform.position;
        _initRotation = transform.eulerAngles;
        rb = HMD.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        crystal.SetActive(false);
        stage_sound = true;
        stage_left = false;
        stage_right = false;
        makeNoise = false;
        count = 0;
        needleft = false;
        littleman.SetActive(false);
        temp = 0;
        flytoright.SetActive(false);
    }
    private Vector3 velocity = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        
        //transform.position = Vector3.SmoothDamp(transform.position, TargetPosition.transform.position,ref velocity, 10f);
        if(stage_sound==false && stage_left==false && stage_right==false)
        {
            if (end != true)
            {
                float trigger = pinchAnimationAction.action.ReadValue<float>();
                bool leftTrigger = LeftTrigger.action.triggered;
                if (leftTrigger)
                {
                    if (move == true)
                    {
                        move = false;
                        leftRight = false;
                        Vector3 movement = new Vector3(0, 0, 0);
                        rb.velocity = movement;
                        timer = 0f;
                    }
                    else
                    {
                        move = true;
                        timer = 0f;
                        leftRight = true;
                    }
                }
                if (trigger > 0.5)
                {
                    HMD.transform.position = _initPosition;
                    HMD.transform.eulerAngles = _initRotation;
                }
                if (move)
                {
                    /// Vector3 movement = new Vector3(0, 0, moveSpeed);
                    ///rb.velocity = movement;
                    timer += Time.deltaTime;

                    // Use Mathf.Lerp to smoothly interpolate between 0 and targetSpeed over time
                    currentSpeed = Mathf.Lerp(0f, moveSpeed, timer / accelerationDuration);

                    // Update the Rigidbody's velocity with the new Z speed
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, currentSpeed);

                    if (timer >= accelerationDuration)
                    {
                        // Stop further acceleration
                        timer = accelerationDuration;
                    }
                }
                else
                {
                    ///Vector3 movement = new Vector3(0, 0, 0);
                    ///rb.velocity = movement;
                    timer += Time.deltaTime;

                    // Use Mathf.Lerp to smoothly interpolate between the current speed and 0 over time
                    currentSpeed = Mathf.Lerp(rb.velocity.z, 0f, timer / decelerationDuration);

                    // Update the Rigidbody's velocity with the new Z speed
                    rb.velocity = new Vector3(0, 0, currentSpeed);

                    if (timer >= decelerationDuration)
                    {
                        // Stop further deceleration
                        timer = decelerationDuration;
                    }
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
            if (!end)
            {
                Vector3 zeroy = transform.position;
                transform.position = new Vector3(zeroy.x, _initPosition.y, zeroy.z);
            }
            if (end)
            {
                transform.position = Vector3.SmoothDamp(transform.position, TargetPosition.transform.position, ref velocity, 3f);
            }
            if (Vector3.Distance(transform.position, TargetPosition.transform.position) < 0.1f)
            {
                crystal.SetActive(true);
            }
        }
        if(stage_sound==true)
        {
            bool rightTrigger = pinchAnimationAction.action.triggered;
            if(rightTrigger || Input.GetKeyDown(KeyCode.C))
            {
                count += 1;
                if(makeNoise==true)
                {
                    Debug.Log("noise");
                    temp += 1; 
                }
               
            }
            if (count==0)
            {
                tutorial_text.text = "It's too dark here...";
            }else if (count==1)
            {
                tutorial_text.text = "Let's make some soundwave!";
            }else if (count ==2)
            {
                tutorial_text.text = "Please make some noise!";
                temp = 0;
            }
            if (count >=2 && makeNoise==true  && temp==0)
            {
                tutorial_text.text = "Great!";
                temp = 1;
            }else if(temp==2)
            {
                tutorial_text.text = "You can use this skill to help you avoid obstacle!";
            }else if (temp ==3)
            {
                stage_sound = false;
                count = 0;
                stage_left = true;
            }
            
        }
        if(stage_left == true)
        {
            bool rightTrigger = pinchAnimationAction.action.triggered;
            if (rightTrigger || Input.GetKeyDown(KeyCode.C))
            {
                count += 1;
                if(count>2)
                {
                    if(needleft!=true)
                    {
                        count -= 1;
                    }
                }
                if(temp>=1)
                {
                    temp += 1;
                }
            }
            if (count == 1)
            {
                tutorial_text.text = "Now let's try flying!";
            }else if (count ==2 && needleft == true)
            {
                tutorial_text.text = "Oops! That was close!";
            }
            else if (count == 2)
            {
                tutorial_text.text = "";
                move = true;
            }
            else if (count == 3)
            {
                tutorial_text.text = "Let's try fly to the left!";
            }
            else if (count == 4)
            {
                tutorial_text.text = "Raise your right arm as the little man";
                littleman.SetActive(true);
                script.right = true;
                leftRight = true;
            }
            if(temp==1)
            {
                tutorial_text.text = "Great! But it seems too left.";
                script.right = false;
                littleman.SetActive(false);
                leftRight = false;
                flytoright.SetActive(true);
            }
            else if (temp==2)
            {
                tutorial_text.text = "Raise your left arm as the little man";
                littleman.SetActive(true);
                script.left = true;
                stage_right = true;
            }
            if(temp==1024)
            {
                tutorial_text.text = "Fantastic! Now we can fly like a bat!";
                littleman.SetActive(false);
                script.left = false;
                stage_right = false;
            }
            if (move)
            {
                /// Vector3 movement = new Vector3(0, 0, moveSpeed);
                ///rb.velocity = movement;
                timer += Time.deltaTime;

                // Use Mathf.Lerp to smoothly interpolate between 0 and targetSpeed over time
                currentSpeed = Mathf.Lerp(0f, moveSpeed, timer / accelerationDuration);

                // Update the Rigidbody's velocity with the new Z speed
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, currentSpeed);

                if (timer >= accelerationDuration)
                {
                    // Stop further acceleration
                    timer = accelerationDuration;
                }
            }
            else
            {
                ///Vector3 movement = new Vector3(0, 0, 0);
                ///rb.velocity = movement;
                timer += Time.deltaTime;

                // Use Mathf.Lerp to smoothly interpolate between the current speed and 0 over time
                currentSpeed = Mathf.Lerp(rb.velocity.z, 0f, timer / decelerationDuration);

                // Update the Rigidbody's velocity with the new Z speed
                rb.velocity = new Vector3(0, 0, currentSpeed);

                if (timer >= decelerationDuration)
                {
                    // Stop further deceleration
                    timer = decelerationDuration;
                }
            }
            if (leftRight)
            {
                if (Right_hand.transform.position.y - Left_hand.transform.position.y > Flying_threshold) //fly to left
                {
                    HMD.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                }

            }
            if(stage_right)
            {
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
            SoundSource.SetActive(false);
            //StartCoroutine(MoveToZero());
        }
        if(other.gameObject.tag== "TurnLeft")
        {
            move = false;
            needleft = true;
        }
        if(other.gameObject.tag == "LeftOK")
        {
            temp = 1;
        }
        if (other.gameObject.tag == "RightOK")
        {
            temp = 1024;
        }
    }
    private IEnumerator MoveToZero()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;
        Vector3 finalTargetPosition = TargetPosition.transform.position;

        while (elapsedTime < lerpTime)
        {
            // Move towards the target position
            transform.position = Vector3.Lerp(initialPosition, finalTargetPosition, elapsedTime / lerpTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log("here");
        // Ensure the camera reaches the exact target position and rotation
        //transform.position = finalTargetPosition;
        yield return new WaitForSeconds(0.5f);
        crystal.SetActive(true);
        Debug.Log("end");
    }
}
