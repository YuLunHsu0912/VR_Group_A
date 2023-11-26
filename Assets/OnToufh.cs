using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnToufh : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Hand")
        {
            text.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
