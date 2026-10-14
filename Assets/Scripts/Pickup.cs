using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Visuals")]
    public float spinSpeed = 90.0f;
    public float bobHeight = 0.3f;
    public float bobSpeed = 2.0f;

    Vector3 _startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        float newY = _startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add logic for what happens when the player picks up the item
            GameManager.Instance.OnPickupCollected();
            Destroy(gameObject);
        }
    }
}
