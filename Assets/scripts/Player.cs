using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rb;
    private Vector3 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float x = 0f;
        float z = 0f;

        if (Keyboard.current.aKey.isPressed) x -= 1f;
        if (Keyboard.current.dKey.isPressed) x += 1f;
        if (Keyboard.current.sKey.isPressed) z -= 1f;
        if (Keyboard.current.wKey.isPressed) z += 1f;

        moveInput = new Vector3(x, 0f, z).normalized;
    }

    private void FixedUpdate()
    {
        Vector3 velocity = moveInput * moveSpeed;

        rb.linearVelocity = new Vector3(
            velocity.x,
            rb.linearVelocity.y,
            velocity.z
        );
    }
}