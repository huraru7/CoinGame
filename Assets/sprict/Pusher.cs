using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pusher : MonoBehaviour
{
    private Vector3 Initialposition;
    private Rigidbody rb;

    public bool pusherSwitch = true;
    public float amplitude = 1f;
    public float speed = 1f;

    void Start()
    {
        Initialposition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (pusherSwitch)
        {
            float z = Initialposition.z + Mathf.Sin(Time.fixedTime * speed) * amplitude;
            rb.MovePosition(new Vector3(Initialposition.x, Initialposition.y, z));
        }
    }
}