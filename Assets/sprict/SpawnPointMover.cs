using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPointMover : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float leftLimit = -2f;
    public float rightLimit = 2f;

    void Update()
    {
        float input = 0f;
        if (Keyboard.current[Key.A].isPressed) input = -1f;
        if (Keyboard.current[Key.D].isPressed) input = 1f;

        if (input == 0f) return;

        float newX = Mathf.Clamp(
            transform.position.x + input * moveSpeed * Time.deltaTime,
            leftLimit,
            rightLimit
        );
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
