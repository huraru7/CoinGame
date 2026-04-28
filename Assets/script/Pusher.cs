using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pusher : MonoBehaviour
{
    private Vector3 initialPosition;
    private Rigidbody rb;

    public bool isMoving = true;
    public float amplitude = 1f;
    public float speed = 1f;

    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            float z = initialPosition.z + Mathf.Sin(Time.fixedTime * speed) * amplitude;
            rb.MovePosition(new Vector3(initialPosition.x, initialPosition.y, z));
        }
    }
}