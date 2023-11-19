using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continuous_moving : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject HMD;
    public bool move = false;
    // Start is called before the first frame update

    //shake test
    public float shakeDuration = 2f;
    public float shakeMagnitude = 0.05f;
    public float ShakemoveSpeed = 1f;
    private Vector3 originalPosition;
    public Vector3 targetVector = new Vector3(1f, 0f, 0f);
    Vector3 targetPosition;
    void Start()
    {
        move = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            HMD.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!!");
        move = false;
        StartCoroutine(ShakeAndMove());

    }
    private IEnumerator ShakeAndMove()
    {
        // Save the original position of the camera
        originalPosition = transform.position;

        // Shake the camera for a specified duration
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            // Generate a random offset for the camera position
            Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;

            // Apply the offset to the camera position
            transform.position = originalPosition + shakeOffset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset the camera position after shaking
        transform.position = originalPosition;

        // Move the camera to another place
        // Adjust the target position as needed
        targetPosition = originalPosition + targetVector;
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, ShakemoveSpeed * Time.deltaTime);
            yield return null;
        }

        // Ensure the camera reaches the exact target position
        transform.position = targetPosition;
    }
}
