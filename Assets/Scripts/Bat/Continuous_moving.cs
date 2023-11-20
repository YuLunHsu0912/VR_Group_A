using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continuous_moving : MonoBehaviour
{
    public float moveSpeed = 2f;
    public GameObject HMD;
    public bool move = false;
    // Start is called before the first frame update

    //shake test
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 0.05f;
    public float ShakemoveSpeed = 1f;
    private Vector3 originalPosition;
    public Vector3 targetVector = new Vector3(1.5f, 0f, 0f);
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
        Vector3 originalPosition = transform.position;

        // Shake the camera for a specified duration
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            // Generate a random offset for the camera position
            Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;

            // Apply the offset to the camera position
            transform.position = originalPosition + shakeOffset;

            elapsed += Time.deltaTime;

            // Move the camera to another place simultaneously
            Vector3 targetPosition = originalPosition + targetVector;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, ShakemoveSpeed * Time.deltaTime);

            yield return null;
        }

        // Reset the camera position after shaking
        transform.position = originalPosition;

        // Ensure the camera reaches the exact target position
        Vector3 finalTargetPosition = originalPosition + targetVector;
        while (Vector3.Distance(transform.position, finalTargetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalTargetPosition, ShakemoveSpeed * Time.deltaTime);
            yield return null;
        }

        // Ensure the camera reaches the exact target position
        transform.position = finalTargetPosition;
    }
}
