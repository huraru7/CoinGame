using UnityEngine;

public class Pusher : MonoBehaviour
{
    private Vector3 Initialposition;

    public bool pusherSwitch;
    public float amplitude = 1f;
    public float speed = 1f;

    void Start()
    {
        Initialposition = transform.position;
    }

    void Update()
    {
        if (pusherSwitch)
        {
            float z = Initialposition.z + Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = new Vector3(Initialposition.x, Initialposition.y, z);
        }
    }
}
