using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPointMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float leftLimit = -2f;
    public float rightLimit = 2f;

    private InputSystem_Actions inputActions;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()  { inputActions.Player.Enable(); }
    void OnDisable() { inputActions.Player.Disable(); }

    void Update()
    {
        float input = inputActions.Player.Move.ReadValue<Vector2>().x;
        if (input == 0f) return;

        float newX = Mathf.Clamp(
            transform.position.x + input * moveSpeed * Time.deltaTime,
            leftLimit,
            rightLimit
        );
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
