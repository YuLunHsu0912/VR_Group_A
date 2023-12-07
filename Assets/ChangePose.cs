using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePose : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    public bool right;
    public bool left;
    void Start()
    {
        animator = GetComponent<Animator>();
        right = false;
        left = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R)||right)
        {
            animator.SetBool("Right", true);
        }
        else
        {
            animator.SetBool("Right", false);
        }
        if (Input.GetKey(KeyCode.E)||left)
        {
            animator.SetBool("Left", true);
        }
        else
        {
            animator.SetBool("Left", false);
        }
    }
}
