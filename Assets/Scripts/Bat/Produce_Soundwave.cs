
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Produce_Soundwave : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject myPrefab;
    public AudioLoudnessDetection detector;
    public float loudnessSensibility = 100;
    public float threshold = 10f;
    public bool KeyboardToProduceSound = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;
        ///Debug.Log("Now loudness="+loudness);
        if(KeyboardToProduceSound)
        {
            if (Input.GetKeyDown(KeyCode.Space))///
            {
                Instantiate(myPrefab, transform.position, Quaternion.identity); //�᭱�[,transform�|�����ܦ��o��onject�p��
            }
        }
       
        else
        {
            if(loudness>threshold)///
            {
                Instantiate(myPrefab, transform.position, Quaternion.identity); //�᭱�[,transform�|�����ܦ��o��onject�p��
            }
        }


    }

}
