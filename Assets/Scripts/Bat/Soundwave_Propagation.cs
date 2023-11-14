using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundwave_Propagation : MonoBehaviour
{
    // Start is called before the first frame update
    public float max_radius = 10;
    public float scale_speed = 3;
    bool enter_loop;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x <= max_radius)
        {
            float radius = transform.localScale.x + Time.deltaTime * scale_speed;
            transform.localScale = new Vector3(radius, radius, radius);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
