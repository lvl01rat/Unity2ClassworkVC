using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [Header("Door Setup Value")]
    public Transform target;
    public float smoothTime = 0.3f;
    public float yVelocity = 0.0f;
    public bool doorOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    // Update is called once per frame
    void Update()
    {
        if (doorOpen)
        {
            float newY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref yVelocity, smoothTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
